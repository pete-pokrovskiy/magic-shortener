using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;
namespace MagicShortener.API.Infrastructure
{
    /// <summary>
    /// Кастомная прослойка для перехвата исключений
    /// </summary>
    public static class CustomExceptionMiddleware
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
                        // TODO: логирование исключения/уведомление ответственных и т д
                        
                        await context.Response.WriteAsync(JsonConvert.SerializeObject(new {
                            ErrorMessage = contextFeature.Error.ToString()
                        },
                        new JsonSerializerSettings
                        {
                            ContractResolver = new CamelCasePropertyNamesContractResolver()
                        }));
                    }
                });
            });
        }
    }
}
