using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface ICheckoutRepository
    {
        Task<CheckoutSession> CreateCheckoutSession(CheckoutSession checkoutSession);

        Task<CheckoutSession?> GetCheckoutSession(Guid checkoutSessionID);
        Task<bool> UpdateCheckoutSession(CheckoutSession checkoutSession);
    }
}