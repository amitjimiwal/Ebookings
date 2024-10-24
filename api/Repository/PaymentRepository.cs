using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interface;
using api.Models;
using Ebooking.Data;
using Microsoft.EntityFrameworkCore;

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

        public async Task<PaymentInformation?> GetPaymentInformation(Guid paymentInformationID)
        {
            var paymentInformation = await DbContext.PaymentInformation.FindAsync(paymentInformationID);
            return paymentInformation;
        }

        public async Task<bool> UpdatePayment(PaymentInformation payment)
        {
            var updatedPayment = await DbContext.PaymentInformation.Where(x => x.Id == payment.Id).ExecuteUpdateAsync(setters => setters.SetProperty(x => x.PaymentStatus, PaymentStatus.Completed).SetProperty(x => x.TransactionId, payment.TransactionId).SetProperty(x => x.ExpiryTime, payment.ExpiryTime));
            return true;
        }
    }
}