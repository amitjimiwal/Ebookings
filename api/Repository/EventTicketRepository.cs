using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Events;
using api.Interface;
using api.Models;
using Ebooking.Data;
using Ebooking.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<TicketTypes?> GetTicketTypebyID(Guid TicketTypeID)
        {
            var ticket = await DbContext.TicketTypes.FirstOrDefaultAsync(ticket => ticket.Id == TicketTypeID);
            return ticket;
        }

        public async Task<TicketTypes> UpdateTicketCount(Guid ticketTypeID, int count)
        {
            var ticket = await DbContext.TicketTypes.FirstOrDefaultAsync(ticket => ticket.Id == ticketTypeID);
            if (ticket == null)
            {
                throw new ArgumentException("Invalid ticketID provided.");
            }
            ticket.AvailableTickets -= count;
            await DbContext.SaveChangesAsync();
            return ticket;
        }

        public async Task<TicketTypes> UpdateTicketData(TicketTypes ticketType)
        {
            var TicketData = await DbContext.TicketTypes.Where(x => x.Id == ticketType.Id).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.AvailableTickets, ticketType.AvailableTickets).SetProperty(x => x.TotalTickets, ticketType.TotalTickets).SetProperty(x => x.TicketPrice, ticketType.TicketPrice).SetProperty(x => x.DisplayName, ticketType.DisplayName));
            return ticketType;
        }
    }
}