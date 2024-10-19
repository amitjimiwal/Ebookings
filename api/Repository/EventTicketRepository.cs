using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Events;
using api.Interface;
using api.Models;
using Ebooking.Data;
using Ebooking.Models;

namespace api.Repository
{
    public class EventTicketRepository : IEventTicketRepository
    {
        private readonly ApplicationDbContext DbContext;
        public EventTicketRepository(ApplicationDbContext applicationDbContext)
        {
            DbContext = applicationDbContext;
        }
        public async Task<List<TicketTypes>> CreateTicketForEvent(List<CreateTicketDTO> ticket, Guid eventId)
        {
            // if (events == null || !DbContext.Events.Any(e => e.Id == events.Id))
            // {
            //     throw new ArgumentException("Invalid event provided.");
            // }
            List<TicketTypes> ticketTypes = new List<TicketTypes>();
            foreach (var item in ticket)
            {
                var ticketItem = new TicketTypes
                {
                    Id = Guid.NewGuid(),
                    EventId = eventId,
                    TotalTickets = item.TotalTickets,
                    TicketPrice = item.TicketPrice,
                    AvailableTickets = item.AvailableTickets,
                    DisplayName = item.DisplayName
                };
                ticketTypes.Add(ticketItem);
            }
            return ticketTypes;
        }
    }
}