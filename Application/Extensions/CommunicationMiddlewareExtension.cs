﻿using Application.Middlewares;
using Microsoft.AspNetCore.Builder;
namespace Application.Extensions
{
    public static class CommunicationMiddlewareExtension
    {
        public static IApplicationBuilder
        UseCommunicationMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CommunicationMiddleware>();
        }
    }
}
