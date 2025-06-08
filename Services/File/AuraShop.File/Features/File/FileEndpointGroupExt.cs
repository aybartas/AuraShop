using Asp.Versioning.Builder;
using AuraShop.File.Features.File.Delete;
using AuraShop.File.Features.File.Upload;

namespace AuraShop.File.Features.File
{
    public static class FileEndpointGroupExt
    {
        public static void AddFileGroupEndpoints(this WebApplication app, ApiVersionSet apiVersionSet)
        {
            app.MapGroup("api/v{version:apiVersion}/files").WithTags("Files")
                .UploadFile().DeleteFile()
                .WithApiVersionSet(apiVersionSet);
        }
    }
}
