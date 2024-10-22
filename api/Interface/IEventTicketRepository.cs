using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Events;
using api.Models;
using Ebooking.Models;

namespace api.Interface
{
    public interface IEventTicketRepository
    {
        Task<List<TicketTypes>> CreateTicketForEvent(List<CreateTicketDTO> ticket, Guid eventId);

        Task<TicketTypes?> GetTicketTypebyID(Guid TicketTypeID);
    }
}