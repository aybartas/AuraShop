using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.ApplyDiscount;

public record ApplyCouponCommand(string CouponCode) : IRequest<ServiceResult>;