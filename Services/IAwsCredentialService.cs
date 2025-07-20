using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using VisionOfChosen_BE.DTOs.Setting;
using VisionOfChosen_BE.Infra.Context;
using VisionOfChosen_BE.Infra.Models;

namespace VisionOfChosen_BE.Services
{
    public interface IAwsCredentialService
    {
        Task<AwsCredentialDto> GetAsync(string userId);
        Task<AwsCredentialDto> CreateOrUpdateAsync(AwsCredentialCreateDto dto, string userId);
    }

    public class AwsCredentialService : IAwsCredentialService
    {
        private readonly VisionOfChosen_Context _context;
        private readonly IMapper _mapper;

        public AwsCredentialService(VisionOfChosen_Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AwsCredentialDto> GetAsync(string userId)
        {
            var entities = await _context.AwsCredentials
                .FirstOrDefaultAsync(x => x.UserId == userId);

            return _mapper.Map<AwsCredentialDto>(entities);
        }

        public async Task<AwsCredentialDto> CreateOrUpdateAsync(AwsCredentialCreateDto dto, string userId)
        {
            var entity = await _context.AwsCredentials
                .FirstOrDefaultAsync(x => x.UserId == userId);

            if (entity == null)
            {
                entity = _mapper.Map<AwsCredential>(dto).Created(userId);
                entity.UserId = userId;
                _context.AwsCredentials.Add(entity);
            }
            else
            {
                _mapper.Map(dto, entity);
                entity.Modified(userId);
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<AwsCredentialDto>(entity);
        }

    }
}
