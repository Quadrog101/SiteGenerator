using System.Net;

namespace SiteGenerator.Domain.Exceptions;

public class EntityNotFoundException(string msg) : BaseException(msg)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.NotFound;
}