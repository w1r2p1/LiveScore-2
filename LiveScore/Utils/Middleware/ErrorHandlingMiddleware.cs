using LiveScore.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LiveScore.Utils.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);

                if (context.Response.StatusCode > 400)
                {
                    throw new ClientRequestException();
                }
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = (int)HttpStatusCode.InternalServerError;
            var result = ErrorCode.InternalServerError;

            if (exception is ClientRequestException)
            {
                result = ErrorCode.ClientError;
                code = context.Response.StatusCode;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(new { error = result }));
        }
    }
}
