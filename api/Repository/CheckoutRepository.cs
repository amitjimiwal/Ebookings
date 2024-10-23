using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interface;
using api.Models;
using Ebooking.Data;
using Microsoft.AspNetCore.Authentication;

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
    }
}