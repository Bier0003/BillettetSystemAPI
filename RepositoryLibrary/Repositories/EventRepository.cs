using BillettetSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary.Models;

namespace BillettetSystemAPI.Repositories   
{
    public class EventRepository : IEvent
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public async Task<Event> CreateEvent(string event_title, string event_description, DateTime create_at, int categoryId)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {

                    var newEvent = new Event
                    {
                        Event_Title = event_title,
                        Event_Description = event_description,
                        Create_At = create_at,
                        Categories = new Category()
                    };

                   
                        var category = await _context.Categories.FindAsync(categoryId);
                        if (category != null)
                        {
                        newEvent.Categories = new Category() { Id_Category = categoryId };
                        }
                        else
                        {
                            throw new Exception($"Category with ID {categoryId} not found.");
                        }
                    

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
        }

        public async Task<List<Event>> GetAllEvents()
        {
            var events = await _context.Events
                .Include(e => e.Categories) 
                .ToListAsync();

            if (events == null || !events.Any())
            {
                throw new Exception("No events found");
            }

            return events.Select(e => new Event
            {
                Id_event = e.Id_event,
                Event_Title = e.Event_Title,
                Event_Description = e.Event_Description,
                Create_At = e.Create_At,
                Categories = e.Categories
            }).ToList();
        }

        public async Task<Event> GetEventById(int Id_event)
        {
            var events = await _context.Events
                .Include(e => e.Categories)
                .FirstOrDefaultAsync(e => e.Id_event == Id_event);

            if (events == null)
            {
                throw new Exception("Event not found");
            }

            return new Event
            {
                Id_event = events.Id_event,
                Event_Title = events.Event_Title,
                Event_Description = events.Event_Description,
                Create_At = events.Create_At,
                Categories = events.Categories
            };
        }

        public async Task<bool> RemoveEvent(int Id_event)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var eventToRemove = await _context.Events
                    .Include(e => e.Categories)
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
                throw new Exception(e.Message);
            }
        }

        public async Task<Event> UpdateEvent(int Id_event, string event_title, string event_description, DateTime create_at, List<int> categoryIds)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var eventToUpdate = await _context.Events
                        .Include(e => e.Categories)
                        .FirstOrDefaultAsync(e => e.CategoryId == Id_category);

                    if (eventToUpdate == null)
                    {
                        throw new Exception("Event not found");
                    }

                    // Update event fields
                    eventToUpdate.Event_Title = event_title;
                    eventToUpdate.Event_Description = event_description;
                    eventToUpdate.Create_At = create_at;

                    // Clear existing categories and add new ones
                    eventToUpdate.Categories.Clear();
                    foreach (var categoryId in categoryIds)
                    {
                        var category = await _context.Categories.FindAsync(categoryId);
                        if (category != null)
                        {
                            eventToUpdate.Categories.Add(category); // Associate new category
                        }
                        else
                        {
                            throw new Exception($"Category with ID {categoryId} not found.");
                        }
                    }

                    await _context.SaveChangesAsync(); // Save changes to DB
                    await transaction.CommitAsync(); // Commit transaction

                    return eventToUpdate; 
                }
                catch (Exception e)
                {
                    await transaction.RollbackAsync(); // Rollback on error
                    throw new Exception(e.Message);
                }
            }
        }
    }
}
