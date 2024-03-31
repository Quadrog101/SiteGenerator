using System.Net;

namespace SiteGenerator.Domain.Exceptions;

public abstract class BaseException(string msg) : Exception(msg)
{
    public abstract HttpStatusCode StatusCode { get; }
}