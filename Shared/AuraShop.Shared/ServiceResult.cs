using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using Refit;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace AuraShop.Shared
{
    public class ServiceResult
    {
        [JsonIgnore]
        public HttpStatusCode Status { get; set; }
        public ProblemDetails? ProblemDetails { get; set; }
        public static ServiceResult SuccessAsNoContent()
        {
            return new ServiceResult()
            {
                Status = HttpStatusCode.NoContent
            };
        }
        public static ServiceResult ErrorAsNotFound(string detail = "The requested resource was not found")
        {
            return new ServiceResult()
            {
                Status = HttpStatusCode.NotFound,
                ProblemDetails = new ProblemDetails
                {
                    Type = "Not found",
                    Detail = detail,
                }
            };
        }
        public static ServiceResult Error(ApiException exception)
        {

            if (string.IsNullOrEmpty(exception.Content))
            {
                return new ServiceResult()
                {
                    ProblemDetails = new ProblemDetails
                    {
                        Title = exception.Message,
                    },
                    Status = exception.StatusCode
                };
            }

            var problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            return new ServiceResult
            {
                Status = exception.StatusCode,
                ProblemDetails = problemDetails,
            };
        }
        public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode status)
        {
            return new ServiceResult()
            {
                ProblemDetails = problemDetails,
                Status = status
            };
        }
        public static ServiceResult Error(IDictionary<string, object> errors)
        {
            return new ServiceResult()
            {
                Status = HttpStatusCode.BadRequest,
                ProblemDetails = new ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = (int?)HttpStatusCode.BadRequest,
                },
            };
        }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public T? Data { get; set; }

        [JsonIgnore]
        public string? UrlAsCreated { get; set; }

        public static ServiceResult<T> SuccessAsOk(T data)
        {
            return new ServiceResult<T>()
            {
                Status = HttpStatusCode.OK,
                Data = data
            };
        }
        public static ServiceResult<T> SuccessAsCreated(T data, string url)
        {
            return new ServiceResult<T>()
            {
                Status = HttpStatusCode.Created,
                Data = data,
                UrlAsCreated = url
            };
        }
        public new static ServiceResult<T> Error(ProblemDetails problemDetails,HttpStatusCode status)
        {
            return new ServiceResult<T>()
            {
                ProblemDetails = problemDetails,
                Status = status
            };
        }

        public static ServiceResult<T> ErrorAsNotFound(string detail = "The requested resource was not found")
        {
            return new ServiceResult<T>()
            {
                Status = HttpStatusCode.NotFound,
                ProblemDetails = new ProblemDetails
                {
                    Type = "Not found",
                    Detail = detail,
                }
            };
        }
        public new static ServiceResult<T> Error(string title, string detail, HttpStatusCode status)
        {
            return new ServiceResult<T>
            {
                Status = status,
                ProblemDetails = new ProblemDetails
                {
                    Title = title,
                    Status = (int?)status,
                    Detail = detail,
                },
            };
        }
        public new static ServiceResult<T> Error(IDictionary<string,object> errors)
        {
            return new ServiceResult<T>()
            {
                Status = HttpStatusCode.BadRequest,
                ProblemDetails = new ProblemDetails
                {
                    Title = "Validation errors occured",
                    Detail = "Please check the errors property for more details",
                    Extensions = errors,
                    Status = (int?)HttpStatusCode.BadRequest,
                },
            };
        }
    }
}
