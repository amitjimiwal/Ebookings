using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.DTO.Events;
using api.Mapper;
using api.Models;
using Ebooking.DTO.Events;
using Ebooking.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ebooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventRepository eventRepository;
        private readonly IBookingRepository BookingRepository;
        public EventsController(IEventRepository repository, IBookingRepository booking, UserManager<ApplicationUser> userManager)
        {
            eventRepository = repository;
            BookingRepository = booking;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents()
        {
            var EventsData = await eventRepository.GetAllEvents();
            if (EventsData == null)
            {
                return NotFound();
            }
            // Mapping the Events to EventDTO
            var EventsResponse = EventsData.Select(eve => eve.CreateDTOFromEvent());
            return Ok(EventsResponse);
        }

        [HttpDelete]
        [Authorize]
        [Route("cancel/{guid}")]
        public async Task<IActionResult> CancelEvent(Guid guid)
        {
            var DeleteBookings = BookingRepository.DeleteAllBookingsForEvent(guid);
            var eventData = await eventRepository.DeleteEvent(guid);
            if (DeleteBookings == true && eventData != null) return Ok("Successfully deleted the event");
            return BadRequest("Error while deleting the Bookings for the event");
        }

        [HttpGet]
        [Route("{guid}")]
        public async Task<IActionResult> GetEvent(Guid guid)
        {
            var eventData = await eventRepository.GetEventById(guid);
            if (eventData == null) return NotFound();
            return Ok(eventData.CreateDTOFromEvent());
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateEvent([FromBody] CreateEventDTO createEventDTO)
        {
            string userID = "";
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newEvent = createEventDTO.CreateEventFromDTO(userID);
            var CreatedEvent = await eventRepository.CreateEvent(newEvent);
            if (CreatedEvent == null)
            {
                return StatusCode(500, "Error while creating the event");
            }
            return Ok(CreatedEvent.CreateDTOFromEvent());
        }

    [Authorize]
    [HttpGet("userinfo")]
    public IActionResult GetUserInfo()
    {
        // Get the email (which you've stored in the JwtRegisteredClaimNames.Email claim)
        var email = HttpContext.User.FindFirstValue(JwtRegisteredClaimNames.Email);

        // Get the username (which you've stored in the JwtRegisteredClaimNames.GivenName claim)
        var username = User.FindFirstValue(JwtRegisteredClaimNames.GivenName);

        // You can return this information or use it as needed
        return Ok(new { Email = email, Username = username });
    }
    }
}