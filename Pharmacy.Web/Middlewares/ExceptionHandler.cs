using System.Net;

namespace Pharmacy.Web.Middlewares;

public class ExceptionHandler
{
    private readonly RequestDelegate _next;

    public ExceptionHandler(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next.Invoke(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionMessageAsync(httpContext.Response, ex);
        }
    }

    private async Task HandleExceptionMessageAsync(HttpResponse httpResponse, Exception ex)
    {
        httpResponse.ContentType = "application/json";
        httpResponse.StatusCode = (int)HttpStatusCode.InternalServerError;

        var message = new
        {
            StatusCode = httpResponse.StatusCode,
            ErrorMessage = ex.Message,
            InnerExceptionMessage = ex.InnerException?.Message
        };

        await httpResponse.WriteAsJsonAsync(message);
    }
}
