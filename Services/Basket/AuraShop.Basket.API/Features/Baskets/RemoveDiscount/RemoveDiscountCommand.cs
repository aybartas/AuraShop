using AuraShop.Shared;
using MediatR;

namespace AuraShop.Basket.API.Features.Baskets.RemoveDiscount;

public record RemoveDiscountCommand() : IRequest<ServiceResult>;