using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WebSwIT.BusinessLogicLayer.Helpers;
using WebSwIT.Shared;
using WebSwIT.Shared.Exceptions;

namespace WebSwIT.PresentationLayer.Middlewares
{
    public class ExceptionMiddleware
    {
        private RequestDelegate _next;
        private ILogger _logger;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ExceptionMiddleware>();
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next.Invoke(context);

                string body = await new StreamReader(context.Request.Body, Encoding.UTF8).ReadToEndAsync();
                string url = context.Request.GetDisplayUrl();
            }
            catch (ServerException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                LogHelper.WriteLogToFile($"Date\n{DateTime.Now}");
                LogHelper.WriteLogToFile($"Message\n{ex.Message}");
                LogHelper.WriteLogToFile($"StackTrace\n{ex.StackTrace}");

                context.Response.ContentType = Constants.CookieParams.JSON_TYPE;
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync("Sorry, an error occurred on the server. Please try later!");
            }

        }

        private static async Task HandleExceptionAsync(HttpContext context, ServerException exception)
        {
            string result = JsonConvert.SerializeObject(new List<string>(exception.ErrorMessages));
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)exception.Code;
            await context.Response.WriteAsync(result);
        }
    }
}
