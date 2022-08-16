using System.Net;

namespace BuberDinner.Application.Common.Exceptions;

public interface IServiceException
{
    HttpStatusCode StatusCode { get; }

    string ErrorMessage { get; }
}