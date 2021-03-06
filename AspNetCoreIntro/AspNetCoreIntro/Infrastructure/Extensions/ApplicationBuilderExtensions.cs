﻿namespace AspNetCoreIntro.Infrastructure.Extensions
{
    using AspNetCoreIntro.Handlers;
    using AspNetCoreIntro.Middlewere;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Linq;
    using System.Reflection;

    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseDatabaseMigration(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<DatabaseMigrationMiddlewere>();
        }

        public static IApplicationBuilder UseHtmlContentType(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<HtmlContentTypeMiddlewere>();
        }

        public static IApplicationBuilder UseRequestHandlers(this IApplicationBuilder builder)
        {
            var handlers = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Where(t => t.IsClass && typeof(IHandler).IsAssignableFrom(t))
                .Select(Activator.CreateInstance)
                .Cast<IHandler>()
                .OrderBy(h => h.Order);

            foreach (var handler in handlers)
            {
                builder.MapWhen(handler.Condition, app =>
                {
                    app.Run(handler.RequestHandler);
                });
            }

            return builder;
        }

        public static void UseNotFoundHandler(this IApplicationBuilder builder)
        {
            builder.Run(async (context) =>
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("404 Page was not found :/");
            });
        }
    }
}
