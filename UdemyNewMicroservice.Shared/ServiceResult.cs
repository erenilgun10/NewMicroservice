using Microsoft.AspNetCore.Mvc;
using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace UdemyNewMicroservice.Shared;

public class ServiceResult
{
    [JsonIgnore]
    public HttpStatusCode Status { get; set; }
    public ProblemDetails? Fail { get; set; }

    [JsonIgnore] public bool IsSuccess => Fail is null;

    [JsonIgnore] public bool IsFail => !IsSuccess;

    // Static factory method to create a successful ServiceResult with NoContent status
    public static ServiceResult SuccessAsNoContent()
    {
        return new ServiceResult
        {
            Status = HttpStatusCode.NoContent
        };
    }

    // Static factory method to create a ServiceResult representing a Not Found error
    public static ServiceResult ErrorAsNotFound()
    {
        return new ServiceResult
        {
            Status = HttpStatusCode.NotFound,
            // Provide a default ProblemDetails object for Not Found errors
            Fail = new ProblemDetails
            {
                Title = "Not Found",
                Detail = "The requested resource could not be found.",
                Status = (int)HttpStatusCode.NotFound
            }
        };
    }
    public static ServiceResult ErrorFromProblemDetails(ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
        {
            return new ServiceResult
            {
                Fail = new ProblemDetails()
                {
                    Title = "An error occurred",
                },
                Status = exception.StatusCode,
            };

        }
        ProblemDetails? problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        return new ServiceResult
        {
            Fail = problemDetails,
            Status = exception.StatusCode
        };
    }

    // 400, 404, 500 gibi durumlarda kullanılacak hata bilgilerini içeren ServiceResult oluşturmak için statik factory method
    public static ServiceResult Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        return new ServiceResult
        {
            Fail = problemDetails,
            Status = statusCode
        };
    }

    // 400, 404, 500 gibi durumlarda kullanılacak hata bilgilerini içeren ServiceResult oluşturmak için statik factory method
    public static ServiceResult Error(string title, string description, HttpStatusCode statusCode)
    {
        return new ServiceResult
        {
            Status = statusCode,
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = description,
                Status = statusCode.GetHashCode() // (int)statusCode 
            },
        };
    }

    // 400, 404, 500 gibi durumlarda kullanılacak hata bilgilerini içeren ServiceResult oluşturmak için statik factory method
    public static ServiceResult Error(string title, HttpStatusCode statusCode)
    {
        return new ServiceResult
        {
            Status = statusCode,
            Fail = new ProblemDetails
            {
                Title = title,
                Status = statusCode.GetHashCode() // (int)statusCode 
            },
        };
    }

    // 400 => BadRequest => Validation hataları için kullanılacak ServiceResult oluşturmak için statik factory method
    public static ServiceResult ErrorFromValidation(IDictionary<string, object?> errors)
    {
        return new ServiceResult
        {
            Fail = new ProblemDetails
            {
                Title = "Validation Error occurred",
                Detail = "Please check the errors property for more details.",
                Extensions = errors,
                Status = HttpStatusCode.BadRequest.GetHashCode()
            },
        };
    }


}


public class ServiceResult<T> : ServiceResult
{

    public T? Data { get; set; }
    [JsonIgnore] public string? UrlAsCreated { get; set; }

    // 200
    public static ServiceResult<T> SuccessAsOk(T data)
    {
        return new ServiceResult<T>
        {
            Status = HttpStatusCode.OK,
            Data = data
        };
    }

    // 201 => Created => Response body de oluşturulan kaynağın bilgisi dönülür
    public static ServiceResult<T> SuccessAsCreated(T data, string url)
    {
        return new ServiceResult<T>
        {
            Status = HttpStatusCode.Created,
            Data = data,
            UrlAsCreated = url
        };
    }

    // 204 => NoContent => Response body de herhangi bir veri dönülmez
    public new static ServiceResult<T> ErrorFromProblemDetails(ApiException exception)
    {
        if (string.IsNullOrEmpty(exception.Content))
        {
            return new ServiceResult<T>
            {
                Fail = new ProblemDetails()
                {
                    Title = "An error occurred",
                },
                Status = exception.StatusCode,
            };

        }
        ProblemDetails? problemDetails = JsonSerializer.Deserialize<ProblemDetails>(exception.Content, new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        });

        return new ServiceResult<T>
        {
            Fail = problemDetails,
            Status = exception.StatusCode
        };









    }

    // 400, 404, 500 gibi durumlarda kullanılacak hata bilgilerini içeren ServiceResult oluşturmak için statik factory method
    public new static ServiceResult<T> Error(ProblemDetails problemDetails, HttpStatusCode statusCode)
    {
        return new ServiceResult<T>
        {
            Fail = problemDetails,
            Status = statusCode
        };
    }

    // 400, 404, 500 gibi durumlarda kullanılacak hata bilgilerini içeren ServiceResult oluşturmak için statik factory method
    public new static ServiceResult<T> Error(string title, string description, HttpStatusCode statusCode)
    {
        return new ServiceResult<T>
        {
            Status = statusCode,
            Fail = new ProblemDetails
            {
                Title = title,
                Detail = description,
                Status = statusCode.GetHashCode() // (int)statusCode 
            },
        };
    }

    // 400, 404, 500 gibi durumlarda kullanılacak hata bilgilerini içeren ServiceResult oluşturmak için statik factory method
    public new static ServiceResult<T> Error(string title, HttpStatusCode statusCode)
    {
        return new ServiceResult<T>
        {
            Status = statusCode,
            Fail = new ProblemDetails
            {
                Title = title,
                Status = statusCode.GetHashCode() // (int)statusCode 
            },
        };
    }

    // 400 => BadRequest => Validation hataları için kullanılacak ServiceResult oluşturmak için statik factory method
    public new static ServiceResult<T> ErrorFromValidation(IDictionary<string, object?> errors)
    {
        return new ServiceResult<T>
        {
            Fail = new ProblemDetails
            {
                Title = "Validation Error occurred",
                Detail = "Please check the errors property for more details.",
                Extensions = errors,
                Status = HttpStatusCode.BadRequest.GetHashCode()
            },
        };
    }



}
