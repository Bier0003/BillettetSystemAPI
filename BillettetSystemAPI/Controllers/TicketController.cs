using BillettetSystemAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelBilletterSystem;
using ModelLibrary;

namespace BillettetSystemAPI.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly IEvent _eventRepository;
        private readonly ICategory _categoryRepository;
        private readonly ITicket _ticketRepository;
        private readonly ApplicationDbContext _context;

        public TicketController(IEvent eventRepository, ICategory categoryRepository, ITicket ticketRepository, ApplicationDbContext context)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _ticketRepository = ticketRepository;
            _context = context;
        }

        [HttpGet("getAllTickets")]
        public async Task<IActionResult> GetAllTickets()
        {
            var result = await _ticketRepository.GetAllTickets();
            return Ok(result);
        }

        [HttpGet("getTicketById")]
        public async Task<IActionResult> GetTicketById(Guid Id_ticket)
        {
            var result = await _ticketRepository.GetTicketById(Id_ticket);
            return Ok(result);
        }

        [HttpPost("createTicket")]
        public async Task<IActionResult> CreateTicket([FromBody] CreateTicket ticketModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTicket = await _ticketRepository.CreateTicket(
                ticketModel.Ticket_Price,
                ticketModel.is_used,
                ticketModel.EventId


            );

            return Ok(createdTicket);
        }

        [HttpPost("buyTicket")]
        public async Task<Ticket> BuyTicket([FromBody] buyTicket ticketModel)
        {
            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(t => t.Ticket_Price == t.Ticket_Price && t.is_used == false);

            if (ticket == null)
            {
                throw new InvalidOperationException("Ticket not available.");
            }

            ticket.is_used = true;
            await _context.SaveChangesAsync();

            return ticket;
        }

        [HttpPut("updateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicket ticketModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTicket = await _ticketRepository.UpdateTicket(
                ticketModel.Id_Ticket,
                ticketModel.Ticket_Price,
                ticketModel.is_used,
                ticketModel.EventId

            );

            return Ok(updatedTicket);
        }

        [HttpDelete("deleteEvent/{id}")]
        public async Task<IActionResult> RemoveEvent(int id)
        {
            var result = await _eventRepository.RemoveEvent(id);
            return Ok(result);
        }
    }
}

