using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interface;
using api.Models;
using Ebooking.Data;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly ApplicationDbContext DbContext;
        public CheckoutRepository(ApplicationDbContext applicationDb)
        {
            DbContext = applicationDb;
        }
        public async Task<CheckoutSession> CreateCheckoutSession(CheckoutSession checkoutSession)
        {
            await DbContext.CheckoutSessions.AddAsync(checkoutSession);
            await DbContext.SaveChangesAsync();
            return checkoutSession;
        }

        public async Task<CheckoutSession?> GetCheckoutSession(Guid checkoutSessionID)
        {
            var checkoutSession = await DbContext.CheckoutSessions.FindAsync(checkoutSessionID);
            return checkoutSession;
        }

        public async Task<bool> UpdateCheckoutSession(CheckoutSession checkoutSession)
        {
            var UpdatedCheckout = await DbContext.CheckoutSessions.Where(x => x.Id == checkoutSession.Id).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.Status, CheckoutStatus.Paid).SetProperty(x => x.ExpiryTime, checkoutSession.ExpiryTime));
            return true;
        }
    }
}