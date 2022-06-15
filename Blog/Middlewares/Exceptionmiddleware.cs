using System.Net;
//using Microsoft.AspNetCore.Http;
//using NLog;
//using System.IO;
//using System.Threading.Tasks;
//using ILogger = NLog.ILogger;
namespace Blog.Middlewares;

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
        //this._logger = LogManager.GetCurrentClassLogger();
    }
    #endregion
    #region Methods :
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body);
            context.Request.Body.Position = 0;
            await _next(context);
        }
        catch (System.Exception ex)
        {
            // _logger.Error(ex, "Error occured check the (Error Details) for more details.");
            HandleException(context);
            await context.Response.WriteAsync($"Error occured ,{ex.Message}");
        }
    }
    #endregion
    #region Private Methods :
    private void HandleException(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json";
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
    #endregion

}
