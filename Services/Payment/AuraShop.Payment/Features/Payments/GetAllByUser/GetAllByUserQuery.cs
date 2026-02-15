using AuraShop.Shared;
using MediatR;

namespace AuraShop.Payment.Features.Payments.GetAllByUser;

public class GetAllByUserQuery : IRequest<ServiceResult<List<PaymentDto>>>
{
}