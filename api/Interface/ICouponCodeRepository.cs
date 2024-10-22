using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;

namespace api.Interface
{
    public interface ICouponCodeRepository
    {
        Task<CouponCode?> GetCouponCodeById(string Name, Guid EventId);

        Task CreateCouponForEvent(CouponCode couponCode);
    }
}