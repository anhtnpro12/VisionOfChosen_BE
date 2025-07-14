using Microsoft.EntityFrameworkCore;
using VisionOfChosen_BE.DTOs.Scan;
using VisionOfChosen_BE.Infra.Context;
using VisionOfChosen_BE.Infra.Models;

namespace VisionOfChosen_BE.Services
{
    public interface IScanService
    {
        Task<List<ScanDto>> GetAllAsync();
        Task<ScanDto?> GetByIdAsync(string id);
        Task<ScanDto> CreateAsync(ScanCreateDto dto, string actorId);
        Task<ScanDto?> UpdateAsync(string id, ScanUpdateDto dto, string actorId);
        Task<bool> DeleteAsync(string id, string actorId);
    }

    public class ScanService : IScanService
    {
        private readonly VisionOfChosen_Context _context;

        public ScanService(VisionOfChosen_Context context)
        {
            _context = context;
        }

        public async Task<List<ScanDto>> GetAllAsync()
        {
            return await _context.Scans
                .Where(x => !x.deleted)
                .OrderByDescending(x => x.ScanTime)
                .Select(x => ToDto(x))
                .ToListAsync();
        }

        public async Task<ScanDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Scans.FirstOrDefaultAsync(x => x.id == id && !x.deleted);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<ScanDto> CreateAsync(ScanCreateDto dto, string actorId)
        {
            var entity = new Scan
            {
                ScanTime = dto.ScanTime,
                Directory = dto.Directory,
                AddedResources = dto.AddedResources,
                ChangedResources = dto.ChangedResources,
                DestroyedResources = dto.DestroyedResources,
                Status = dto.Status,
                Notes = dto.Notes
            }.Created(actorId);

            _context.Scans.Add(entity);
            await _context.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task<ScanDto?> UpdateAsync(string id, ScanUpdateDto dto, string actorId)
        {
            var entity = await _context.Scans.FirstOrDefaultAsync(x => x.id == id && !x.deleted);
            if (entity == null) return null;

            entity.ScanTime = dto.ScanTime;
            entity.Directory = dto.Directory;
            entity.AddedResources = dto.AddedResources;
            entity.ChangedResources = dto.ChangedResources;
            entity.DestroyedResources = dto.DestroyedResources;
            entity.Status = dto.Status;
            entity.Notes = dto.Notes;
            entity.Modified(actorId);

            _context.Scans.Update(entity);
            await _context.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task<bool> DeleteAsync(string id, string actorId)
        {
            var entity = await _context.Scans.FirstOrDefaultAsync(x => x.id == id && !x.deleted);
            if (entity == null) return false;

            entity.Deleted(actorId);
            _context.Scans.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static ScanDto ToDto(Scan s) => new()
        {
            Id = s.id,
            ScanTime = s.ScanTime,
            Directory = s.Directory,
            AddedResources = s.AddedResources,
            ChangedResources = s.ChangedResources,
            DestroyedResources = s.DestroyedResources,
            Status = s.Status,
            Notes = s.Notes
        };
    }
}
