﻿using MyWeapon.Shared.ResultDtos;
using MyWeapon.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System.Net;
using Serilog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;

namespace MyWeapon.Common.Middlewares
{
    /// <summary>
    /// Log and convert authorization failures to <see cref="ErrorResultDto"/> with generic message.
    /// </summary>
    public class FailedAuthorizationWrapperHandler : IAuthorizationMiddlewareResultHandler
    {
        private readonly AuthorizationMiddlewareResultHandler _defaultHandler;
        private readonly IStringLocalizer _localizer;
        private readonly ILogger _logger;

        public FailedAuthorizationWrapperHandler(IStringLocalizerFactory factory,
            ILogger logger)
        {
            _defaultHandler = new AuthorizationMiddlewareResultHandler();
            _localizer = factory.Create(typeof(CommonResource));
            _logger = logger;
        }

        public async Task HandleAsync(
            RequestDelegate requestDelegate,
            HttpContext httpContext,
            AuthorizationPolicy authorizationPolicy,
            PolicyAuthorizationResult policyAuthorizationResult)
        {
            if (policyAuthorizationResult.Challenged)
            {
                _logger.Warning($"Unauthenticated access to url {httpContext.Request.Path.Value}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                httpContext.Response.ContentType = "application/json";

                var result = JsonCamelCaseSerializer.Serialize(new ErrorResultDto(_localizer["Unauthorized"]));

                await httpContext.Response.WriteAsync(result);
                return;
            }

            if (policyAuthorizationResult.Forbidden)
            {
                _logger.Warning($"Unauthorized access by user {httpContext.User.Identity?.Name}, to url {httpContext.Request.GetEncodedUrl()}");

                httpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                httpContext.Response.ContentType = "application/json";

                var result = JsonCamelCaseSerializer.Serialize(new ErrorResultDto(_localizer["Forbidden"]));

                await httpContext.Response.WriteAsync(result);
                return;
            }

            // Fallback to the default implementation.
            await _defaultHandler.HandleAsync(requestDelegate, httpContext, authorizationPolicy, policyAuthorizationResult);
        }
    }
}
