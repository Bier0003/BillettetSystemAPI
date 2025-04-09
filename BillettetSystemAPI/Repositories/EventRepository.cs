using BillettetSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelBilletterSystem;

namespace BillettetSystemAPI.Repositories   
{
    public class EventRepository : IEvent
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public async Task<Event> CreateEvent(string Event_Title, string Event_Description, DateTime Create_at, int Ticket_amount, string? Image, int CategoryId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var category = await _context.Category.FindAsync(CategoryId);
                if (category == null)
                {
                    throw new Exception($"Category with ID {CategoryId} not found.");
                }


                var newEvent = new Event
                {
                    Event_Title = Event_Title,
                    Event_Description = Event_Description,
                    Create_At = Create_at,
                    CategoryId = CategoryId,
                    Ticket_Amount = Ticket_amount, 
                    Category = category
                };

                _context.Events.Add(newEvent);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return newEvent;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

        public async Task<List<Event>> GetAllEvents()
        {
            var events = await _context.Events
                .Include(e => e.Category) 
                .ToListAsync();

            if (events == null || !events.Any())
            {
                throw new Exception("No events found.");
            }

            return events;
        }

        public async Task<Event> GetEventById(int Id_event)
        {
            var eventItem = await _context.Events
                .Include(e => e.Category)
                .FirstOrDefaultAsync(e => e.Id_event == Id_event);

            if (eventItem == null)
            {
                throw new Exception("Event not found");
            }

            return eventItem;
        }

        public async Task<bool> RemoveEvent(int Id_event)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var eventToRemove = await _context.Events
                    .FirstOrDefaultAsync(e => e.Id_event == Id_event);

                if (eventToRemove == null)
                {
                    throw new Exception("Event not found");
                }

                _context.Events.Remove(eventToRemove);
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return true;
            }
            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception($"Failed to remove event: {e.Message}");
            }
        }

        public async Task<Event> UpdateEvent(int Id_Event, string Event_Title, string Event_Description, DateTime Create_at, int Ticket_amount, string? Image, int CategoryId)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var eventToUpdate = await _context.Events
                    .Include(e => e.Category)
                    .FirstOrDefaultAsync(e => e.Id_event == Id_Event);

                if (eventToUpdate == null)
                {
                    throw new Exception("Event not found");
                }

                var category = await _context.Category.FindAsync(CategoryId);
                if (category == null)
                {
                    throw new Exception($"Category with ID {CategoryId} not found.");
                }

                // Update fields
                eventToUpdate.Event_Title = Event_Title;
                eventToUpdate.Event_Description = Event_Description;
                eventToUpdate.Create_At = Create_at;
                eventToUpdate.CategoryId = CategoryId;
                eventToUpdate.Ticket_Amount = Ticket_amount; 
                eventToUpdate.Category = category;

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                return eventToUpdate;
            }

            catch (Exception e)
            {
                await transaction.RollbackAsync();
                throw new Exception(e.Message);
            }
        }

    }
}
