using NLog;
using ILogger = NLog.ILogger;
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
        this._logger = LogManager.GetCurrentClassLogger();
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
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error occurred check the (Error Details) for more details.");
            HandleException(context);
            //await context.Response.WriteAsync($"Error occured ,{ex.Message}");
        }
    }
    #endregion
    #region Private Methods :
    private static void HandleException(HttpContext httpContext)
    {
        httpContext.Response.Redirect($"{httpContext.Request.PathBase}/Home/Error_404");
        //if(httpContext.Response.StatusCode==(int)HttpStatusCode.NotFound)
        //{
        //    httpContext.Response.Redirect("Home/Error_404");
        //}
        //httpContext.Response.ContentType = "application/json";
        //httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
    }
    #endregion

}
