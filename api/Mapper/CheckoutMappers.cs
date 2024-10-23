using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.DTO.Checkout;
using api.Models;

namespace api.Mapper
{
    public static class CheckoutMappers
    {
        public static CheckoutSession CreateCheckoutFromDTO(this CreateCheckoutSessionDTO createCheckoutSessionDTO, string appUserID)
        {
            return new CheckoutSession()
            {
                Name = createCheckoutSessionDTO.Name,
                Email = createCheckoutSessionDTO.Email,
                PhoneNumber = createCheckoutSessionDTO.PhoneNumber,
                EventId = createCheckoutSessionDTO.EventId,
                TotalTicketsPurchased = createCheckoutSessionDTO.TotalTickets,
                TotalPrice = createCheckoutSessionDTO.TotalPrice,
                CouponCode = createCheckoutSessionDTO.CouponCode,
                DiscountAmount = createCheckoutSessionDTO.DiscountAmount,
                FinalAmount = createCheckoutSessionDTO.FinalAmount,
                AppUserID = appUserID,
                Status = CheckoutStatus.Pending,
                CreatedAt = DateTime.Now,
                ExpiryTime = DateTime.Now.AddMinutes(30),
                Tickets = createCheckoutSessionDTO.Tickets.Select(ticket => new Tickets()
                {
                    TicketTypeId = ticket.TicketId,
                    Quantity = ticket.TicketQuantity,
                    TotalPrice = ticket.SingleticketPrice * ticket.TicketQuantity,
                    TicketName = ticket.TicketDisplayName
                }).ToList()
            };
        }
    }
}