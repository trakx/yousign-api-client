using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Serilog;
using Trakx.Utils.Apis;

namespace Trakx.YouSign.ApiClient
{
    public class YouSignCredentialsProvider : ICredentialsProvider, IDisposable
    {

        private readonly YouSignApiConfiguration _configuration;
        private readonly CancellationTokenSource _tokenSource;

        private static readonly ILogger Logger = Log.Logger.ForContext<YouSignCredentialsProvider>();

        public YouSignCredentialsProvider(YouSignApiConfiguration configuration)
        {
            _configuration = configuration;
            _tokenSource = new CancellationTokenSource();
        }


        #region Implementation of ICredentialsProvider

        /// <inheritdoc />
        public void AddCredentials(HttpRequestMessage msg)
        {
            msg.Headers.Add("Authorization", $"Bearer {_configuration.ApiKey}");
            Logger.Verbose("Headers added");
        }

        public Task AddCredentialsAsync(HttpRequestMessage msg)
        {
            AddCredentials(msg);
            return Task.CompletedTask;
        }

        #endregion

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing)
            {
                return;
            }
            _tokenSource.Cancel();
            _tokenSource.Dispose();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}