using AuraShop.Order.Application.Contracts;
using AuraShop.Order.Application.Dtos;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using AutoMapper;
using MediatR;

namespace AuraShop.Order.Application.Features.Order.GetUserOrders;

public class GetUserOrdersQuery : IRequest<ServiceResult<GetUserOrdersQueryResponse>>
;

public class GetUserOrdersQueryResponse
{
    public List<OrderDto> Orders { get; set; }
}

public class GetUserOrdersQueryHandler(IOrderRepository orderRepository, IMapper mapper, IIdentityService identityService) : IRequestHandler<GetUserOrdersQuery, ServiceResult<GetUserOrdersQueryResponse>>
{
    public async Task<ServiceResult<GetUserOrdersQueryResponse>> Handle(GetUserOrdersQuery request, CancellationToken cancellationToken)
    {
        var userOrders = await orderRepository.GetOrdersByUserId(identityService.UserId.Value);

        var orders = mapper.Map<List<OrderDto>>(userOrders);

        return ServiceResult<GetUserOrdersQueryResponse>.SuccessAsOk(new GetUserOrdersQueryResponse()
        {
            Orders = orders
        });
    }
}