using EventBookingApp.Contexts;
using EventBookingApp.Exceptions;
using EventBookingApp.Interfaces;
using EventBookingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace EventBookingApp.Repositories
{
    public class EventRepository : IRepository<int, Event>
    {
        private readonly EventContext _context;
        private readonly ILogger<EventRepository> _logger;

        public EventRepository(EventContext eventContext, ILogger<EventRepository> logger)
        {
            _context = eventContext;
            _logger = logger;
        }

        public async Task<Event> Add(Event entity)
        {
            try
            {
                _context.Events.Add(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding event");
                throw new CouldNotAddException("Event");
            }
        }

        public async Task<Event> Delete(int key)
        {
            var eventEntity = await Get(key);
            if (eventEntity == null)
            {
                throw new NotFoundException("Event");
            }
            _context.Events.Remove(eventEntity);
            await _context.SaveChangesAsync();
            return eventEntity;
        }

        public async Task<Event> Get(int key)
        {
            var eventEntity = await _context.Events.FirstOrDefaultAsync(e => e.Id == key);
            if (eventEntity == null)
            {
                throw new NotFoundException("Event");
            }
            return eventEntity;
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            var events = await _context.Events.ToListAsync();
            if (events.Count == 0)
            {
                throw new NotFoundException("Event");
            }
            return events;
        }

        public async Task<Event> Update(int key, Event entity)
        {
            var eventEntity = await Get(key);
            if (eventEntity == null)
            {
                throw new NotFoundException("Event");
            }
            eventEntity.Name = entity.Name;
            eventEntity.Location = entity.Location;
            eventEntity.Date = entity.Date;
            eventEntity.Description = entity.Description;
            await _context.SaveChangesAsync();
            return eventEntity;
        }

    }
}
