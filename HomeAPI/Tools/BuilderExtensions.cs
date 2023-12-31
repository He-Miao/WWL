﻿using AspNetCoreRateLimit;
using Common.Helpers;

namespace HomeAPI.Tools
{
    /// <summary>
    /// Builder扩展
    /// </summary>
    public static class BuilderExtensions
    {
        public static ServiceProvider serviceProvider { get; set; }
        public static WebApplicationBuilder AddServiceProvider(this WebApplicationBuilder builder)
        {
            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceProvider = builder.Services.BuildServiceProvider();
            return builder;
        }
        public static WebApplicationBuilder AddRateLimit(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<IpRateLimitOptions>(AppSettingsHelper.GetSection("IpRateLimiting"));
            builder.Services.Configure<IpRateLimitPolicies>(AppSettingsHelper.GetSection("IpRateLimitPolicies"));
            builder.Services.AddInMemoryRateLimiting();
            builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            return builder;
        }
    }
}
