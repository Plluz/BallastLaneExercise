using Microsoft.AspNetCore.Mvc.Filters;

public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<GlobalExceptionFilterAttribute> _logger;

    public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger)
    {
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public override void OnException(ExceptionContext context)
    {
        _logger.LogError($"Exception caught by GlobalExceptionFilter in {context.ActionDescriptor.DisplayName}: {context.Exception}");

        context.HttpContext.Response.StatusCode = 500;
        context.Result = new JsonResult("Internal Server Error");
    }
}