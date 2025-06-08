using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.File.Features.File.Upload
{
    public static class UploadFileEndpoint
    {
        public static RouteGroupBuilder UploadFile(this RouteGroupBuilder group)
        {
            group.MapPost("/", async (IFormFile file , IMediator mediator) =>
            {
                var result = await mediator.Send(new UploadFileCommand(file));

                return result.ToResult();

            })
                .WithName("UploadFile")
                .MapToApiVersion(1,0)
                .AddEndpointFilter<ValidationFilter<UploadFileCommand>>().DisableAntiforgery();

            return group;
        }
    }
}
