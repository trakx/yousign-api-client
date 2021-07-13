using System;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Polly;
using Serilog;
using Trakx.YouSign.ApiClient.Utils;
using Trakx.Utils.Apis;
using Trakx.Utils.DateTimeHelpers;

namespace Trakx.YouSign.ApiClient
{
    public static partial class AddYouSignApiClientExtension
    {
        public static IServiceCollection AddTrakxExchangeApiClient(
            this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();
            var apiConfig = configuration.GetSection(nameof(YouSignApiConfiguration))
                .Get<YouSignApiConfiguration>()!;
            services.Configure<YouSignApiConfiguration>(configuration.GetSection(nameof(YouSignApiConfiguration)));

            AddCommonDependencies(services, apiConfig);

            return services;
        }

        public static IServiceCollection AddTrakxExchangeApiClient(
            this IServiceCollection services, YouSignApiConfiguration configuration)
        {
            var options = Options.Create(configuration);
            services.AddSingleton(options);
            AddCommonDependencies(services, configuration);

            return services;
        }

        private static void AddCommonDependencies(IServiceCollection services, YouSignApiConfiguration configuration)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            services.AddSingleton<ICredentialsProvider, ApiKeyCredentialsProvider>();
            services.AddSingleton(s => new ClientConfigurator(s));
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
                    result.Result.Content?.ReadAsStringAsync().GetAwaiter().GetResult(),
                    retryCount, context.PolicyKey, timeSpan.TotalMilliseconds);
            }
        }
    }
}
