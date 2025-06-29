using AuraShop.Auth.Dtos;
using AuraShop.Shared;
using AuraShop.Shared.Extensions;
using MediatR;

namespace AuraShop.Auth.Features.GetProfile
{
    public record GetProfileRequest : IRequest<ServiceResult<UserDto>>;

    public static class GetProfileEndpointExt
    {
        public static RouteGroupBuilder AddGetProfileEndpoint(this RouteGroupBuilder builder)
        {
            builder.MapGet("/profile", async (IMediator mediator) =>
            {
                var result = await mediator.Send(new GetProfileRequest());
                return result.ToResult();

            }).RequireAuthorization();

            return builder;
        }
    }
}
