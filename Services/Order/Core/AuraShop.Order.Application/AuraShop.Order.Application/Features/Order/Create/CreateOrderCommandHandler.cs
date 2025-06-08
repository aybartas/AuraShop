using AuraShop.Order.Application.Contracts;
using AuraShop.Order.Application.UnitOfWork;
using AuraShop.Order.Domain.Entities;
using AuraShop.Shared;
using AuraShop.Shared.Services;
using MediatR;

namespace AuraShop.Order.Application.Features.Order.Create;

public class CreateOrderCommandHandler(
    IUnitOfWork unitOfWork,
    IGenericRepository<Domain.Entities.Order> orderRepository,
    IGenericRepository<Address> addressRepository,
    IIdentityService identityService
) : IRequestHandler<CreateOrderCommand, ServiceResult<CreateOrderCommandResponse>>
{
    public async Task<ServiceResult<CreateOrderCommandResponse>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        Address? userAddress = null;

        if (request.SaveShippingAddress)
        {
            userAddress = new Address
            {
                UserId = identityService.UserId,
                Street = request.ShippingOrderAddress.Street,
                City = request.ShippingOrderAddress.City,
                State = request.ShippingOrderAddress.State,
                ZipCode = request.ShippingOrderAddress.ZipCode,
                Country = request.ShippingOrderAddress.Country
            };

            await addressRepository.AddAsync(userAddress);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }

        var orderAddress = new OrderAddress
        {
            UserAddressId = userAddress?.Id,
            Street = request.ShippingOrderAddress.Street,
            City = request.ShippingOrderAddress.City,
            State = request.ShippingOrderAddress.State,
            ZipCode = request.ShippingOrderAddress.ZipCode,
            Country = request.ShippingOrderAddress.Country,
        };

        var order = Domain.Entities.Order.CreateInitialOrder(identityService.UserId, orderAddress);

        if (!string.IsNullOrEmpty(request.CouponCode))
        {
            var discountRate = 0; // TODO: Call discount service
            order.ApplyDiscount(request.CouponCode, discountRate);
        }

        foreach (var item in request.Items)
        {
            order.AddItem(item.ProductId, item.ProductName, item.UnitPrice, item.Quantity);
        }

        await orderRepository.AddAsync(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        order.SetPaid("test-payment");

        orderRepository.Update(order);
        await unitOfWork.SaveChangesAsync(cancellationToken);


        return ServiceResult<CreateOrderCommandResponse>.SuccessAsCreated(new CreateOrderCommandResponse(order.OrderNumber), $"/api/orders/{order.OrderNumber}");
    }
}