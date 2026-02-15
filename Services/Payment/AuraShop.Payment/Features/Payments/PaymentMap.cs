using AutoMapper;

namespace AuraShop.Payment.Features.Payments;

public class PaymentMap : Profile
{
    public PaymentMap()
    {
        CreateMap<Payment, PaymentDto>().ReverseMap();
    }
}