using AuraShop.Shared;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.FileProviders;

namespace AuraShop.File.Features.File.Upload;

public record UploadFileCommandResponse(string Url);
public record UploadFileCommand(IFormFile File) : IRequest<ServiceResult<UploadFileCommandResponse>>;

public class UploadFileCommandValidator : AbstractValidator<UploadFileCommand>
{
    public UploadFileCommandValidator()
    {
        RuleFor(x => x.File).NotNull().WithMessage("File is required");
    }
}

public class UploadFileCommandHandler(IFileProvider fileProvider): IRequestHandler<UploadFileCommand, ServiceResult<UploadFileCommandResponse>>
{
    public async Task<ServiceResult<UploadFileCommandResponse>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var newFileName = $"{request.File.FileName}_{Guid.NewGuid()}{Path.GetExtension(request.File.FileName)}";
        var uploadPath = Path.Combine(fileProvider.GetFileInfo("files").PhysicalPath!,newFileName);

        await using var stream = new FileStream(uploadPath, FileMode.Create);

        await request.File.CopyToAsync(stream, cancellationToken);

        var filePath = $"files/{newFileName}";

        var response = new UploadFileCommandResponse(filePath);

        return ServiceResult<UploadFileCommandResponse>.SuccessAsCreated(response, filePath);

    }
}
