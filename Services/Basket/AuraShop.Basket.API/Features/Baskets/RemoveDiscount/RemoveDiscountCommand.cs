using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.Features.Baskets.RemoveDiscount;

public record RemoveDiscountCommand() : IRequest<ServiceResult>;