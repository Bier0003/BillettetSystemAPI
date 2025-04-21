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


        [HttpPost("buyTicket")]
        public async Task<IActionResult> BuyTicket([FromBody] buyTicketRequest request)
        {
            try
            {
                var result = await _ticketRepository.BuyTicket(request.EventId, request.ticketIntotal);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("updateTicket")]
        public async Task<IActionResult> UpdateTicket([FromBody] UpdateTicket ticketModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateTicket = await _ticketRepository.UpdateTicket(
                ticketModel.Id_Ticket,
                ticketModel.is_used,
                ticketModel.EventId
                );
        
            return Ok(UpdateTicket);
        }

        [HttpPut("MarkAsUsed")]
        public async Task<IActionResult> MarkAsUsed([FromQuery] Guid id_ticket)
        {
            var ticket = await _context.Tickets.FirstOrDefaultAsync(t => t.Id_Ticket == id_ticket);

            if (ticket == null)
                return NotFound("Ticket not found.");

            if (ticket.is_used)
                return BadRequest("Ticket already used.");

            ticket.is_used = true;
            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();

            return Ok("Ticket marked as used.");
        }


        [HttpGet("TicketPDF")]
        public async Task<IActionResult> TicketPDF(Guid id_ticket)
        {
            return await _ticketRepository.TicketPDF(id_ticket);
          

        }

        [HttpDelete("deleteEvent/{id}")]
        public async Task<IActionResult> RemoveEvent(int id)
        {
            var result = await _eventRepository.RemoveEvent(id);
            return Ok(result);
        }
    }
}

