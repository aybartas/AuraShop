using AuraShop.Payment.Database;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Payment.Features.Payments.GetAllByUser;

public class GetAllByUserQueryHandler(PaymentDbContext context, IIdentityService identityService, IMapper mapper) : IRequestHandler<GetAllByUserQuery, ServiceResult<List<PaymentDto>>>
{
    public async Task<ServiceResult<List<PaymentDto>>> Handle(GetAllByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.UserId.Value;

        var payments = await context.Payments
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreateDate)
            .ToListAsync(cancellationToken);

        var mappedPayments = mapper.Map<List<PaymentDto>>(payments);

        return ServiceResult<List<PaymentDto>>.SuccessAsOk(mappedPayments);
    }
}