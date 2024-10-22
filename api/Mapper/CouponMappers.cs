using api.DTO.Checkout;
using api.Models;

namespace api.Mapper
{
    public static class CouponMapper
    {
        public static CouponCode CreateCouponFromDTO(this CreateCouponDTO createCouponDTO)
        {
            return new CouponCode()
            {
                Code = createCouponDTO.Code,
                EventId = createCouponDTO.EventId,
                DiscountPercentage = createCouponDTO.DiscountPercentage,
                MaxUsage = createCouponDTO.MaxUsage,
                CurrentUsage = createCouponDTO.MaxUsage,
                Expiry = createCouponDTO.Expiry
            };
        }
    }
}