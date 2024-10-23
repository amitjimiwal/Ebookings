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
        public static CouponDTO CreateDTOForCoupon(this CouponCode couponCode)
        {
            return new CouponDTO()
            {
                Code = couponCode.Code,
                EventId = couponCode.EventId,
                DiscountPercentage = couponCode.DiscountPercentage,
                MaxUsage = couponCode.MaxUsage,
                CurrentUsage = couponCode.CurrentUsage,
                Expiry = couponCode.Expiry,
            };
        }
    }
}