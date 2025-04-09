using ModelBilletterSystem;
using BillettetSystemAPI.Interfaces;


namespace BillettetSystemAPI.Repositories
{

    public class TicketRepository : ITicket
    {
        private readonly ApplicationDbContext _context;
        public TicketRepository(ApplicationDbContext Context)
        {
            _context = Context;
        }
        public Task BuyTicket(decimal ticket_Price, bool is_used)
        {
            throw new NotImplementedException();
        }
        public Task<Ticket> CreateTicket(decimal ticket_price, bool is_used, int eventId)
        {
            
           throw new NotImplementedException();
        }

        public Task<List<Ticket>> GetAllTickets()
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> GetTicketById(Guid Id_ticket)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveTicket(Guid Id_ticket)
        {
            throw new NotImplementedException();
        }

        public Task<Ticket> UpdateTicket(Guid Id_ticket, decimal ticket_price, bool is_used, int eventId)
        {
            throw new NotImplementedException();
        }
    }
}
