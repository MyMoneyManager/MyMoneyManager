using MyMoneyManager.API.Helpers;
using MyMoneyManager.Service.Exceptions;

namespace MyMoneyManager.API.Middlewares;

public class ExceptionHandlerMiddleWare
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExceptionHandlerMiddleWare> logger;

    public ExceptionHandlerMiddleWare(RequestDelegate next, ILogger<ExceptionHandlerMiddleWare> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (CustomException ex)
        {
            this.logger.LogError(ex.Message);
            context.Response.StatusCode = ex.StatusCode;
            await context.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message,
            });
        }
        catch (Exception ex)
        {
            this.logger.LogError($"{ex}\n\n");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response
            {
                StatusCode = 500,
                Message = ex.Message,
            });
        }
    }
}
