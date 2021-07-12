using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Trakx.YouSign.ApiClient.Utils;
using Trakx.Utils.Apis;

namespace Trakx.YouSign.ApiClient
{
    internal class ClientConfigurator
    {
        private readonly IServiceProvider _provider;

        public ClientConfigurator(IServiceProvider provider)
        {
            _provider = provider;
            Configuration = provider.GetService<IOptions<YouSignApiConfiguration>>()!.Value;
        }

        public YouSignApiConfiguration Configuration { get; }

        public ICredentialsProvider GetCredentialsProvider(System.Type clientType)
        {
            return _provider.GetRequiredService<ICredentialsProvider>();
        }
    }
}