using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using TitanGate.WebSiteStore.Entities.Exceptions;

namespace TitanGate.WebSiteStore.Api.Middleware
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
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var code = HttpStatusCode.InternalServerError;
            string result = "Oops";
            if (ex is WebSiteStoreNotFoundException)
            {
                code = HttpStatusCode.NotFound;
                result = JsonSerializer.Serialize(new { error = ex.Message });
            }
            else if (ex is WebSiteStoreException)
            {
                result = JsonSerializer.Serialize(new { error = ex.Message });
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
