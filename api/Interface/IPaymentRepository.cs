using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface IPaymentRepository
    {
        Task<PaymentInformation> CreatePaymentInformation(PaymentInformation paymentInformation);

        Task<PaymentInformation?> GetPaymentInformation(Guid paymentInformationID);

        Task<bool> UpdatePayment(PaymentInformation paymentInformation);
    }
}