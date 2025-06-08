using AuraShop.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.FileProviders;

namespace AuraShop.File.Features.File.Delete;

public record DeleteFileCommand(string Filename) : IRequest<ServiceResult<DeleteFileCommandResponse>>;

public record DeleteFileCommandResponse(bool IsDeleted);

public class DeleteFileCommandValidator : AbstractValidator<DeleteFileCommand>
{
    public DeleteFileCommandValidator()
    {
        RuleFor(x => x.Filename)
            .NotEmpty().WithMessage("File name is required");
    }
}
public class DeleteFileCommandHandler(IFileProvider fileProvider) : IRequestHandler<DeleteFileCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteFileCommand request, CancellationToken cancellationToken)
    {
        var fileInfo = fileProvider.GetFileInfo(Path.Combine("files", request.Filename));

        if (!fileInfo.Exists)
            return await Task.FromResult(ServiceResult.ErrorAsNotFound());

        System.IO.File.Delete(fileInfo.PhysicalPath!);

        return await Task.FromResult(ServiceResult.SuccessAsNoContent());
    }
}