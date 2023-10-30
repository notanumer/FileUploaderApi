using System.Net;

namespace FileUploader.Infrastructure.Abstractions.Exceptions;

public abstract class ApiException : Exception
{
    public abstract HttpStatusCode Code { get; }

    public ApiException(string message, Exception? inner = null) : base(message, inner)
    {

    }
}
