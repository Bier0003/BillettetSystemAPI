using BillettetSystemAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using ModelLibrary;
using ModelBilletterSystem;



namespace BillettetSystemAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEvent _eventRepository;
        private readonly ICategory _categoryRepository;
        private readonly ApplicationDbContext _context;

        public EventController(IEvent eventRepository, ICategory categoryRepository, ApplicationDbContext context)
        {
            _eventRepository = eventRepository;
            _categoryRepository = categoryRepository;
            _context = context;
        }

        [HttpGet("getAllEvents")]
        public async Task<IActionResult> GetAllEvents()
        {
            var result = await _eventRepository.GetAllEvents();
            return Ok(result);
        }

        [HttpGet("getEventById/{id}")]
        public async Task<IActionResult> GetEventById(int id)
        {
            var result = await _eventRepository.GetEventById(id);
            return Ok(result);
        }

        [HttpPost("createEvent")]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEvent eventModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (eventModel.CategoryId == 0)
                return BadRequest("CategoryId is required.");

            var createdEvent = await _eventRepository.CreateEvent(
                eventModel.Event_Title,
                eventModel.Event_Description,
                eventModel.Create_At,
                eventModel.Ticket_Amount,
                eventModel.Image,
                eventModel.CategoryId
            );

            return Ok(createdEvent);
        }

        [HttpPut("updateEvent")]
        public async Task<IActionResult> UpdateEvent([FromBody] Event eventModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedEvent = await _eventRepository.UpdateEvent(
                eventModel.Id_event,
                eventModel.Event_Title,
                eventModel.Event_Description,
                eventModel.Create_At,
                eventModel.Ticket_Amount,
                eventModel.Image,
                eventModel.CategoryId
            );

            return Ok(updatedEvent);
        }

        [HttpDelete("deleteEvent/{id}")]
        public async Task<IActionResult> RemoveEvent(int id)
        {
            var result = await _eventRepository.RemoveEvent(id);
            return Ok(result);
        }
    }
}
