using System;
using Microsoft.Extensions.DependencyInjection;
using NRetry.Options;

namespace NRetry.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddNRetry(this IServiceCollection services)
        {
            services.AddNRetry(option=> { });
        }
        public static void AddNRetry(this IServiceCollection services,Action<RetryOption> actionOptions)
        {
            RetryOption retryOption = new RetryOption();
            actionOptions.Invoke(retryOption);
            services.AddSingleton(retryOption);
        }
    }
}
