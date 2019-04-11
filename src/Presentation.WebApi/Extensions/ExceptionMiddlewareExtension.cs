namespace BasketService.Presentation.WebApi.Extensions
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Net;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using BasketService.Infrastructure.CrossCutting.Exceptions;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Diagnostics;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Newtonsoft.Json;
    using BasketService.Infrastructure.CrossCutting.Logging;

    [ExcludeFromCodeCoverage]
    public static class ExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseDetailedExceptionHandler(this IApplicationBuilder app, ILog logger)
        {
            return app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    try
                    {
                        await Invoke(context, logger, true).ConfigureAwait(true);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error handling Exception", ex);
                    }
                });
            });
        }
        
        public static IApplicationBuilder UseExceptionHandler(this IApplicationBuilder app, ILog logger)
        {
            return app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    try
                    {
                        await Invoke(context, logger, true).ConfigureAwait(false);
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Error handling Exception", ex);
                    }
                });
            });
        }

        private static async Task Invoke(HttpContext context, ILog logger, bool detailedException)
        {
            var contextFeature = context.Features.Get<IExceptionHandlerPathFeature>();

            SetContentTypeAndStatusCode(context, contextFeature);

            if (contextFeature != null && contextFeature.Error != null)
            {
                ExceptionHandler.HandleException(contextFeature.Error, logger);

                await context.Response.WriteAsync(
                    JsonConvert.SerializeObject(
                        CreateErrorDetail(contextFeature.Error, detailedException))).ConfigureAwait(false);
            }
        }

        private static void SetContentTypeAndStatusCode(HttpContext context, IExceptionHandlerPathFeature contextFeature)
        {
            context.Response.ContentType = new MediaTypeHeaderValue("application/json").ToString();
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            if (contextFeature != null && contextFeature.Error != null)
            {
                SetResponseStatusCodeForException(context, contextFeature.Error);
            }
        }

        private static void SetResponseStatusCodeForException(HttpContext context, Exception exception)
        {
            if (ExceptionHandler.IsApiBaseException(exception))
            {
                SetResponseStatusCode(context, exception as ApiExceptionBase);
            }
            else
            {
                SetResponseStatusCode(context, exception);
            }
        }

        private static void SetResponseStatusCode(HttpContext context, ApiExceptionBase exception)
        {
            context.Response.StatusCode = (int)exception.HttpCode;
        }

        private static void SetResponseStatusCode(HttpContext context, Exception exception)
        {
            var exceptionType = exception.GetType();

            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else if (exceptionType == typeof(ArgumentException))
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
        }

        private static ErrorDetail CreateErrorDetail(Exception exception, bool detailedException)
        {
            if (ExceptionHandler.IsApiBaseException(exception))
            {
                return CreateErrorDetail(exception as ApiExceptionBase, detailedException);
            }

            return new ErrorDetail(exception, (int)ErrorCode.None, detailedException);
        }

        private static ErrorDetail CreateErrorDetail(ApiExceptionBase exception, bool detailedException)
        {
            return new ErrorDetail(exception, exception.Code, detailedException);
        }
    }
}