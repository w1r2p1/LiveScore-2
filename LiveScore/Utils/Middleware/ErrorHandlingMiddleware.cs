using LiveScore.Exceptions;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LiveScore.Utils.Middleware
{
    /// <summary>
    /// Excetion handling middleware that propagates error codes in JSON format to client with coresponding HTTP status code.
    /// </summary>
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        /// <summary>
        /// Constructor that receives request delegate.
        /// </summary>
        /// <param name="next">Request delegate</param>
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// This method invokes next middleware in request pipeline and handles unpredicted exceptions.
        /// </summary>
        /// <param name="context">Http context</param>
        /// <returns><see cref="System.Threading.Tasks.Task"/></returns>
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
