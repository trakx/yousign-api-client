using Trakx.Utils.Apis;

namespace Trakx.YouSign.ApiClient
{
    public class ClientConfigurator
    {

        public YouSignApiConfiguration Configuration { get; init; }

        public ICredentialsProvider CredentialsProvider { get; init; }

        public ClientConfigurator(YouSignApiConfiguration configuration, 
            ICredentialsProvider credentialsProvider)
        {
            Configuration = configuration;
            CredentialsProvider = credentialsProvider;
        }

    }
}
