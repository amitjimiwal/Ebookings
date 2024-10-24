using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Mapper
{
    public static class PaymentMapper
    {
        public static PaymentInformation CreatePaymentSession(this CheckoutSession checkoutSession)
        {
            return new PaymentInformation
            {
                CheckoutSessionId = checkoutSession.Id,
                PaymentStatus = PaymentStatus.Pending,
                PaymentMethod = "CARD",
                AmountToBePaid = checkoutSession.FinalAmount,
                Currency = "INR",
                CreatedAt = DateTime.Now,
                ExpiryTime = DateTime.Now.AddMinutes(30),
            };
        }
    }
}