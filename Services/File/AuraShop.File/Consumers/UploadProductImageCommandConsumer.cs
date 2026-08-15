using AuraShop.Bus.Commands;
using AuraShop.Bus.Events;
using MassTransit;
using Microsoft.Extensions.FileProviders;

namespace AuraShop.File.Consumers
{
    public class UploadProductImageCommandConsumer(IServiceScopeFactory scopeFactory) : IConsumer<UploadProductImageCommand>
    {
        public async Task Consume(ConsumeContext<UploadProductImageCommand> context)
        {
            using var scope = scopeFactory.CreateScope();

            var fileProvider = scope.ServiceProvider.GetRequiredService<IFileProvider>();

            var command = context.Message;

            var newFileName = $"{command.Filename}_{Guid.NewGuid()}{Path.GetExtension(command.Filename)}";
            var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!, newFileName);

            await System.IO.File.WriteAllBytesAsync(uploadPath, command.ImageData);

            var publishEndpoint = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

            await publishEndpoint.Publish(new ProductImageUploadedEvent
            {
                ProductId = command.ProductId,
                ImageUrl = $"files/{newFileName}"
            });

        }
    }
}
