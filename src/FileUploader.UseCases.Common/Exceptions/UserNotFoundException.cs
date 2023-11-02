using FileUploader.Infrastructure.Abstractions.Exceptions;
using System.Net;

namespace FileUploader.UseCases.Common.Exceptions;

public class UserNotFoundException : ApiException
{
    public UserNotFoundException(string message, Exception? inner = null) : base(message, inner)
    {
    }

    public override HttpStatusCode Code => HttpStatusCode.NotFound; 
}
