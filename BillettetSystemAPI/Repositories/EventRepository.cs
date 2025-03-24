using BillettetSystemAPI.Interfaces;
using Microsoft.EntityFrameworkCore;
using ModelBilletterSystem.Models;
using System.Timers;

namespace BillettetSystemAPI.Repositories
{
    public class EventRepository : IEvent
    {
        private readonly ApplicationDbContext _context;
        public EventRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public async Task<Event> CreateEvent(string event_title, string event_description, DateTime create_at, List<int> categoryIds)
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
                        Categories = new List<Category>()  
                    };

                  
                    foreach (var categoryId in categoryIds)
                    {
                        var category = await _context.Categories.FindAsync(categoryId);
                        if (category != null)
                        {
                           
                            category.EventId = newEvent.Id_event;  
                            newEvent.Categories.Add(category);  
                        }
                        else
                        {
                         
                            throw new Exception($"Category with ID {categoryId} not found.");
                        }
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
            throw new NotImplementedException();
        }

        public async Task<Event> GetEventById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveEvent(int id)
        {
            throw new NotImplementedException();
        }


        public async Task<Event> UpdateEvent(int id, string event_title, string event_description, DateTime create_at, List<int> categoryId)
        {
            throw new NotImplementedException();
        }
    }
}
