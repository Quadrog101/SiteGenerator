using System.Net;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SiteGenerator.Domain.Exceptions;

namespace SiteGenerator.Web.Filters;

public class GlobalExceptionFilters(ILogger<GlobalExceptionFilters> logger) : IExceptionFilter
{
    private readonly ILogger _logger = logger;


    public void OnException(ExceptionContext context)
    {
        if (context.ExceptionHandled) return;
        var exception = context.Exception;

        _logger.LogError($"GlobalExceptionFilter: Error in {context.ActionDescriptor.DisplayName}. {exception.Message}. Stack Trace: {exception.StackTrace}");
        switch (exception)
        {
            case null:
                return;
            case BaseException bex:
                context.Result = new ObjectResult(bex.Message) { StatusCode = (int)bex.StatusCode };
                break;
            default:
                context.Result = new ObjectResult(exception.Message) { StatusCode = (int)HttpStatusCode.InternalServerError };
                break;
        }
    }
}