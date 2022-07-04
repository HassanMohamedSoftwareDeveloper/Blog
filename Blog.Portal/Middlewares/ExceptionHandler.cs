using NLog;
using System.Net;
using ILogger = NLog.ILogger;
namespace Blog.Portal.Middlewares;

public class ExceptionHandler
{
    #region PROPS :
    private readonly ILogger _logger;
    private readonly RequestDelegate _next;
    #endregion
    #region CTORS :
    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
        _logger = LogManager.GetCurrentClassLogger();
    }
    #endregion
    #region Methods :
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            _logger.Info("Begin request");
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body);
            context.Request.Body.Position = 0;
            await _next(context);
            if (context.Response.StatusCode == (int)HttpStatusCode.NotFound)
            {
                context.Response.Redirect($"{context.Request.PathBase}/Error404");
            }
            else if (context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
            {
                context.Response.Redirect($"{context.Request.PathBase}/Error500");
            }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred check the (Error Details) for more details.");
            HandleException(context);
        }
    }
    #endregion
    #region Private Methods :
    private static void HandleException(HttpContext httpContext)
    {
        httpContext.Response.Redirect($"{httpContext.Request.PathBase}/Error500");
        //httpContext.Response.ContentType = "application/json";
        //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
    #endregion
}
