using AuraShop.Shared;
using MediatR;

namespace AuraShop.Discount.Features.Coupons.Create
{
    public record CreateCouponCommand(decimal Rate,string Code, DateTime ExpirationDate) : IRequest<ServiceResult<CreateCouponResponse>>;
}
