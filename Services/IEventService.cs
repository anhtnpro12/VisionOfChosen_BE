using Microsoft.EntityFrameworkCore;
using VisionOfChosen_BE.DTOs.Event;
using VisionOfChosen_BE.Infra.Context;
using VisionOfChosen_BE.Infra.Models;

namespace VisionOfChosen_BE.Services
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllAsync();
        Task<EventDto?> GetByIdAsync(string id);
        Task<EventDto> CreateAsync(EventCreateDto dto, string actorId);
        Task<EventDto?> UpdateAsync(string id, EventUpdateDto dto, string actorId);
        Task<bool> DeleteAsync(string id, string actorId);
    }

    public class EventService : IEventService
    {
        private readonly VisionOfChosen_Context _context;

        public EventService(VisionOfChosen_Context context)
        {
            _context = context;
        }

        public async Task<List<EventDto>> GetAllAsync()
        {
            return await _context.Events
                .Where(x => !x.deleted)
                .OrderByDescending(x => x.Time)
                .Select(x => ToDto(x))
                .ToListAsync();
        }

        public async Task<EventDto?> GetByIdAsync(string id)
        {
            var entity = await _context.Events.FirstOrDefaultAsync(x => x.id == id && !x.deleted);
            return entity == null ? null : ToDto(entity);
        }

        public async Task<EventDto> CreateAsync(EventCreateDto dto, string actorId)
        {
            var entity = new Event
            {
                UserId = dto.UserId,
                Changer = dto.Changer,
                Service = dto.Service,
                Name = dto.Name,
                Status = dto.Status,
                Time = dto.Time
            }.Created(actorId);

            _context.Events.Add(entity);
            await _context.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task<EventDto?> UpdateAsync(string id, EventUpdateDto dto, string actorId)
        {
            var entity = await _context.Events.FirstOrDefaultAsync(x => x.id == id && !x.deleted);
            if (entity == null) return null;

            entity.Changer = dto.Changer;
            entity.Service = dto.Service;
            entity.Name = dto.Name;
            entity.Status = dto.Status;
            entity.Time = dto.Time;
            entity.Modified(actorId);

            _context.Events.Update(entity);
            await _context.SaveChangesAsync();

            return ToDto(entity);
        }

        public async Task<bool> DeleteAsync(string id, string actorId)
        {
            var entity = await _context.Events.FirstOrDefaultAsync(x => x.id == id && !x.deleted);
            if (entity == null) return false;

            entity.Deleted(actorId);
            _context.Events.Update(entity);
            await _context.SaveChangesAsync();

            return true;
        }

        private static EventDto ToDto(Event e) => new()
        {
            Id = e.id,
            UserId = e.UserId,
            Changer = e.Changer,
            Service = e.Service,
            Name = e.Name,
            Status = e.Status,
            Time = e.Time
        };
    }
}
