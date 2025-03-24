using ModelBilletterSystem.Models;
using BillettetSystemAPI.Interfaces;


namespace BillettetSystemAPI.Repositories
{

    public class TiketRepository : ITicket
    {
        private readonly ApplicationDbContext _context;
        public TiketRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }

        public Task<Ticket> CreateTicket(int ticket_amount, int ticket_price, bool ability, bool is_used, int eventId)
        {
            throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTickets()
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicketById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveTicket(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> UpdateTicket(int id, int ticket_amount, int ticket_price, bool ability, bool is_used, int eventId)
        {
            throw new NotImplementedException();
        }
    }

}
