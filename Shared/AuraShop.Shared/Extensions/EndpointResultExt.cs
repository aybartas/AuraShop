using System.Net;
using Microsoft.AspNetCore.Http;

namespace AuraShop.Shared.Extensions
{
    public static class EndpointResultExt
    {
        public static IResult ToResult<T>(this ServiceResult<T> result)
        {
            return result.Status switch
            {
                HttpStatusCode.OK => Results.Ok(result.Data),
                HttpStatusCode.Created => Results.Created(result.UrlAsCreated,result.Data),
                HttpStatusCode.NotFound => Results.NotFound(result.ProblemDetails),
                HttpStatusCode.BadRequest => Results.Problem(result.ProblemDetails),
                _ => Results.Problem(result?.ProblemDetails)
            };
        }
        public static IResult ToResult(this ServiceResult result)
        {
            return result.Status switch
            {
                HttpStatusCode.NoContent => Results.NoContent(),
                HttpStatusCode.NotFound => Results.NotFound(result.ProblemDetails),
                _ => Results.Problem(result?.ProblemDetails)
            };
        }
    }
}
