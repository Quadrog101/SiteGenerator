using System.Net;

namespace SiteGenerator.Domain.Exceptions;

public class BusinessException(string message) : BaseException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}