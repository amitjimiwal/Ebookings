using System.Text.Json;
using api.DTO.Events;
using api.Interface;
using api.Mapper;
using api.Models;
using Ebooking.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stockapi.Extensions;

namespace Ebooking.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEventRepository eventRepository;
        private readonly IBookingRepository BookingRepository;
        private readonly IImageUploadService imageUploadService;

        private readonly IEventTicketRepository eventTicketRepository;
        public EventsController(IEventRepository repository, IBookingRepository booking, UserManager<ApplicationUser> userManager, IImageUploadService imageUploadService, IEventTicketRepository eventTicket)
        {
            eventTicketRepository = eventTicket;
            this.imageUploadService = imageUploadService;
            eventRepository = repository;
            BookingRepository = booking;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetEvents([FromQuery] QueryParamsDTO queryParams)
        {
            var EventsData = await eventRepository.GetAllEvents(queryParams);
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!TotalTicketsValidation(createEventDTO.Venue.Capacity, createEventDTO.TicketTypes))
            {
                return BadRequest("Total tickets should be less than or equal to venue capacity");
            }
            //get the username from claims
            string UserName = User.GetUserName();
            var appUser = await userManager.FindByNameAsync(UserName);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }
            //create new event
            var newEvent = createEventDTO.CreateEventFromDTO(appUser.Id);
            var ticketTypes = await eventTicketRepository.CreateTicketForEvent(createEventDTO.TicketTypes, newEvent.Id);
            newEvent.TicketTypes = ticketTypes;
            var CreatedEvent = await eventRepository.CreateEvent(newEvent);
            if (CreatedEvent == null)
            {
                return StatusCode(500, "Error while creating the event");
            }
            return Ok(new
            {
                message = "Successfully created the event",
                eventId = CreatedEvent.Id
            });
        }

        private bool TotalTicketsValidation(int venueCapacity, List<CreateTicketDTO> ticketTypes)
        {
            int totalTickets = 0;
            foreach (var ticket in ticketTypes)
            {
                totalTickets += ticket.TotalTickets;
            }
            return totalTickets <= venueCapacity;
        }

        [HttpPost("uploadImage/{eventId}")]
        [Authorize]
        public async Task<IActionResult> UploadImage([FromForm] List<IFormFile> image, Guid eventId)
        {

            //get the username from claims
            string UserName = User.GetUserName();
            var appUser = await userManager.FindByNameAsync(UserName);
            if (appUser == null)
            {
                return BadRequest("User not found");
            }

            //find the event
            var eventData = await eventRepository.GetEventById(eventId);
            if (eventData == null)
            {
                return NotFound("Event not found");
            }

            //validate if the correct user is trying to upload the image
            if (eventData.ApplicationUserID != appUser.Id)
            {
                return Unauthorized("You are not authorized to upload images for this event");
            }

            //upload the image
            await imageUploadService.UploadImageAsync(image, eventId);
            return Ok("Successfully uploaded all the images");
        }

        [HttpGet("Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await eventRepository.GetEventCategories();
            if (categories == null)
            {
                return NotFound();
            }
            return Ok(categories.Select(cat => new
            {
                cat.Id,
                cat.Name
            }));
        }
    }
}