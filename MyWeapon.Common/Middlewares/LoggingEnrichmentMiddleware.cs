using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace MyWeapon.Common.Middlewares
{
    public class LoggingEnrichmentMiddleware
    {
        private readonly RequestDelegate _next;

        public LoggingEnrichmentMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Extract UserId and TenantId from the context (for example, from claims)
            var userId = context.User?.FindFirst("UserId")?.Value;
            var tenantId = context.User?.FindFirst("TenantId")?.Value;

            // Push these properties to the log context so they appear in all logs for this request
            using (LogContext.PushProperty("UserId", userId))
            using (LogContext.PushProperty("TenantId", tenantId))
            {
                await _next(context);
            }
        }
    }
}
