using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Trakx.Utils.Apis;

namespace Trakx.YouSign.ApiClient
{
    internal abstract class AuthorisedClient
    {
#nullable disable
        public YouSignApiConfiguration Configuration { get; protected set; }
        public ICredentialsProvider CredentialsProvider { get; protected set; }
#nullable restore

        private string? _baseUrl;
        protected string BaseUrl => _baseUrl ??= Configuration!.BaseUrl;

        protected AuthorisedClient(ClientConfigurator clientConfigurator)
        {
            Configuration = clientConfigurator.Configuration;
            CredentialsProvider = clientConfigurator.GetCredentialsProvider(this.GetType());
        }

        protected Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();
            CredentialsProvider.AddCredentials(msg);
            return Task.FromResult(msg);
        }
    }
}
