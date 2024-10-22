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
    public class CouponCodeRepository : ICouponCodeRepository
    {
        private readonly ApplicationDbContext DbContext;
        public CouponCodeRepository(ApplicationDbContext applicationDb)
        {
            DbContext = applicationDb;
        }

        public async Task CreateCouponForEvent(CouponCode couponCode)
        {
            await DbContext.CouponCodes.AddAsync(couponCode);
            await DbContext.SaveChangesAsync();
        }

        public async Task<CouponCode?> GetCouponCodeById(string Name, Guid EventId)
        {
            var couponData = await DbContext.CouponCodes.FirstOrDefaultAsync(coupon => coupon.EventId == EventId && coupon.Code == Name);
            return couponData;
        }
    }
}