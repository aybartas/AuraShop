using AuraShop.Shared;
using MediatR;

namespace AuraShop.Payment.Features.Payments.GetAllByUser;

public record GetAllByUserQuery : IRequest<ServiceResult<List<GetAllByUserResult>>>
{
}