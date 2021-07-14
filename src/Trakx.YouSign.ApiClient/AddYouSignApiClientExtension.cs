using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Serilog;
using Trakx.Utils.Apis;
using Trakx.Utils.DateTimeHelpers;

namespace Trakx.YouSign.ApiClient
{
    public static partial class AddYouSignApiClientExtension
    {
        public static IServiceCollection AddTrakxYouSignApiClient(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            var config = configuration.GetSection(nameof(YouSignApiConfiguration))
                .Get<YouSignApiConfiguration>()!;
            services.Configure<YouSignApiConfiguration>(configuration.GetSection(nameof(YouSignApiConfiguration)));
            AddCommonDependencies(services, config);
            return services;
        }

        public static IServiceCollection AddTrakxYouSignApiClient(
            this IServiceCollection services, YouSignApiConfiguration configuration)
        {
            AddCommonDependencies(services, configuration);

            return services;
        }

        private static void AddCommonDependencies(IServiceCollection services, 
            YouSignApiConfiguration configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<ICredentialsProvider, YouSignCredentialsProvider>();
            services.AddSingleton<ClientConfigurator>();
            services.AddSingleton(configuration);
            services.AddClients(configuration);
        }

        private static void LogFailure(ILogger logger, DelegateResult<HttpResponseMessage> result, TimeSpan timeSpan, int retryCount, Context context)
        {
            if (result.Exception != null)
            {
                logger.Warning(result.Exception, "An exception occurred on retry {RetryAttempt} for {PolicyKey}. Retrying in {SleepDuration}ms.",
                    retryCount, context.PolicyKey, timeSpan.TotalMilliseconds);
            }
            else
            {
                logger.Warning("A non success code {StatusCode} with reason {Reason} and content {Content} was received on retry {RetryAttempt} for {PolicyKey}. Retrying in {SleepDuration}ms.",
                    (int)result.Result.StatusCode, result.Result.ReasonPhrase,
                    result.Result.Content.ReadAsStringAsync().GetAwaiter().GetResult(),
                    retryCount, context.PolicyKey, timeSpan.TotalMilliseconds);
            }
        }
    }
}
