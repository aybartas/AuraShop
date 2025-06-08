using AuraShop.Shared;
using MediatR;

namespace AuraShop.Catalog.Features.Product.Delete;

public class DeleteProductCommandHandler(IProductService productService)
    : IRequestHandler<DeleteProductCommand, ServiceResult>
{
    private readonly IProductService _productService = productService;

    public async Task<ServiceResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var hasProduct =  await _productService.GetProductByIdAsync(request.Id);

        if (hasProduct == null)
            return ServiceResult.ErrorAsNotFound();

        await _productService.DeleteProductAsync(request.Id);

        return ServiceResult.SuccessAsNoContent();
    }
}