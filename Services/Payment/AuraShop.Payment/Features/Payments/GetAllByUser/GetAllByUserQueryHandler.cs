using AuraShop.Payment.Database;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace AuraShop.Payment.Features.Payments.GetAllByUser;

public class GetAllByUserQueryHandler(PaymentDbContext context, IIdentityService identityService) : IRequestHandler<GetAllByUserQuery, ServiceResult<List<GetAllByUserResult>>>
{
    public async Task<ServiceResult<List<GetAllByUserResult>>> Handle(GetAllByUserQuery request, CancellationToken cancellationToken)
    {
        var userId = identityService.UserId.Value;

        var payments = await context.Payments
            .Where(p => p.UserId == userId)
            .OrderByDescending(p => p.CreateDate)
            .Select(p => new GetAllByUserResult
            {
                Id = p.Id,
                PaymentReferenceId = p.PaymentReferenceId,
                OrderNumber = p.OrderNumber,
                Amount = p.Amount,
                Status = p.Status,
                CreatedAt = p.CreateDate
            })
            .ToListAsync(cancellationToken);

        return ServiceResult<List<GetAllByUserResult>>.SuccessAsOk(payments);
    }
}