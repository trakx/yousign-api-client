using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Trakx.Utils.Testing;
using Xunit.Abstractions;

namespace Trakx.YouSign.ApiClient.Tests.Integration
{

    public class YouSignBaseTestBase
    {

        protected ServiceProvider ServiceProvider { get; init; }
        
        protected ILogger Logger { get; init; }

        protected IFilesClient FilesClient { get; init; }
        
        protected IProceduresClient ProceduresClient { get; init; }

        protected IUsersClient UsersClient { get; init; }

        protected IOrganizationsClient OrganizationsClient { get; init; }

        protected MockCreator MockCreator { get; init; }

        public YouSignBaseTestBase(ITestOutputHelper output)
        {
            var secrets = new Secrets();
            var config = new YouSignApiConfiguration
            {
                BaseUrl = "https://staging-api.yousign.com",
                ApiKey = secrets.ApiKey
            };
            Logger = new LoggerConfiguration().WriteTo.TestOutput(output).CreateLogger()
                .ForContext(MethodBase.GetCurrentMethod()!.DeclaringType);
            IServiceCollection services = new ServiceCollection();
            services.AddTrakxExchangeApiClient(config);
            ServiceProvider = services.BuildServiceProvider();
            FilesClient = ServiceProvider.GetRequiredService<IFilesClient>();
            ProceduresClient = ServiceProvider.GetRequiredService<IProceduresClient>();
            OrganizationsClient = ServiceProvider.GetRequiredService<IOrganizationsClient>();
            UsersClient = ServiceProvider.GetRequiredService<IUsersClient>();
            MockCreator = new MockCreator(output);
        }

    }
}
