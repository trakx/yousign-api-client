using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Trakx.YouSign.ApiClient
{
    internal abstract class AuthorisedClient
    {
#nullable disable
        public ClientConfigurator Configurator { get; protected set; }
#nullable restore

        private string? _baseUrl;
        protected string BaseUrl => _baseUrl ??= Configurator.Configuration!.BaseUrl;

        protected AuthorisedClient(ClientConfigurator configurator)
        {
            Configurator = configurator;
        }

        protected async Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken cancellationToken)
        {
            var msg = new HttpRequestMessage();
            await Configurator.CredentialsProvider.AddCredentialsAsync(msg);
            return msg;
        }
    }
}
