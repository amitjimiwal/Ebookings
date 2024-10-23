using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interface;
using api.Models;
using Ebooking.Data;

namespace api.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext DbContext;
        public PaymentRepository(ApplicationDbContext applicationDb)
        {
            DbContext = applicationDb;
        }
        public async Task<PaymentInformation> CreatePaymentInformation(PaymentInformation paymentInformation)
        {
            await DbContext.PaymentInformation.AddAsync(paymentInformation);
            await DbContext.SaveChangesAsync();
            return paymentInformation;
        }
    }
}