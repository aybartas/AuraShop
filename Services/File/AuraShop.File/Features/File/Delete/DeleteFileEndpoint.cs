using AuraShop.Shared.Extensions;
using AuraShop.Shared.Filters;
using MediatR;

namespace AuraShop.File.Features.File.Delete
{
    public static class DeleteFileEndpoint
    {
        public static RouteGroupBuilder DeleteFile(this RouteGroupBuilder group)
        {
            group.MapDelete("/{fileName}", async (string fileName, IMediator mediator) =>
                {
                    var result = await mediator.Send(new DeleteFileCommand(fileName));

                    return result.ToResult();

                })
                .WithName("DeleteFile")
                .MapToApiVersion(1, 0)
                .AddEndpointFilter<ValidationFilter<DeleteFileCommand>>();

            return group;
        }
    }
}
