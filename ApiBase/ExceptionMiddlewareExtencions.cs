using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace ApiBase
{
    public static class ExceptionMiddlewareExtencions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        string message = $"Error: {contextFeature.Error.Message}";
                        if (contextFeature.Error.InnerException != null)
                        {
                            message = $"{contextFeature.Error.Message} ------->> {contextFeature.Error.InnerException!.Message}";
                        }
                        //error message response
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = message
                        }.ToString()!);
                    }
                });
            });
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = null!;

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

    }
}
