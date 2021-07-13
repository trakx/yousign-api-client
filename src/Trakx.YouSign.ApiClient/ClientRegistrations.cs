using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;
using Serilog;


namespace Trakx.YouSign.ApiClient
{
    public static partial class AddYouSignApiClientExtension
    {
        private static void AddClients(this IServiceCollection services, YouSignApiConfiguration configuration)
        {
            var delay = Backoff.DecorrelatedJitterBackoffV2(
                medianFirstRetryDelay: TimeSpan.FromMilliseconds(configuration.InitialRetryDelayInMilliseconds ?? 100), 
                retryCount: configuration.MaxRetryCount ?? 10, fastFirst: true);
                                    
            services.AddHttpClient<IFilesClient, FilesClient>("Trakx.YouSign.ApiClient.FilesClient")
                .AddPolicyHandler((s, request) => 
                    Policy<HttpResponseMessage>
                    .Handle<ApiException>()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    .Or<HttpRequestException>()
                    .OrTransientHttpStatusCode()
                    .WaitAndRetryAsync(delay,
                        onRetry: (result, timeSpan, retryCount, context) =>
                        {
                            var logger = Log.Logger.ForContext<FilesClient>();
                            LogFailure(logger, result, timeSpan, retryCount, context);
                        })
                    .WithPolicyKey("Trakx.YouSign.ApiClient.FilesClient"));

                                
            services.AddHttpClient<IProceduresClient, ProceduresClient>("Trakx.YouSign.ApiClient.ProceduresClient")
                .AddPolicyHandler((s, request) => 
                    Policy<HttpResponseMessage>
                    .Handle<ApiException>()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    .Or<HttpRequestException>()
                    .OrTransientHttpStatusCode()
                    .WaitAndRetryAsync(delay,
                        onRetry: (result, timeSpan, retryCount, context) =>
                        {
                            var logger = Log.Logger.ForContext<ProceduresClient>();
                            LogFailure(logger, result, timeSpan, retryCount, context);
                        })
                    .WithPolicyKey("Trakx.YouSign.ApiClient.ProceduresClient"));

                                
            services.AddHttpClient<IOrganizationsClient, OrganizationsClient>("Trakx.YouSign.ApiClient.OrganizationsClient")
                .AddPolicyHandler((s, request) => 
                    Policy<HttpResponseMessage>
                    .Handle<ApiException>()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    .Or<HttpRequestException>()
                    .OrTransientHttpStatusCode()
                    .WaitAndRetryAsync(delay,
                        onRetry: (result, timeSpan, retryCount, context) =>
                        {
                            var logger = Log.Logger.ForContext<OrganizationsClient>();
                            LogFailure(logger, result, timeSpan, retryCount, context);
                        })
                    .WithPolicyKey("Trakx.YouSign.ApiClient.OrganizationsClient"));

                                
            services.AddHttpClient<IUsersClient, UsersClient>("Trakx.YouSign.ApiClient.UsersClient")
                .AddPolicyHandler((s, request) => 
                    Policy<HttpResponseMessage>
                    .Handle<ApiException>()
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Forbidden)
                    .Or<HttpRequestException>()
                    .OrTransientHttpStatusCode()
                    .WaitAndRetryAsync(delay,
                        onRetry: (result, timeSpan, retryCount, context) =>
                        {
                            var logger = Log.Logger.ForContext<UsersClient>();
                            LogFailure(logger, result, timeSpan, retryCount, context);
                        })
                    .WithPolicyKey("Trakx.YouSign.ApiClient.UsersClient"));

        }
    }
}
