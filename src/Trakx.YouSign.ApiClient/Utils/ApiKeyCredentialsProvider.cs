using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Serilog;
using Trakx.Utils.Apis;
using Trakx.Utils.DateTimeHelpers;

namespace Trakx.YouSign.ApiClient.Utils
{
    public interface ICopperCredentialsProvider : ICredentialsProvider { };
    public class ApiKeyCredentialsProvider : ICopperCredentialsProvider, IDisposable
    {
        internal const string ApiKeyHeader = "ApiKey";
        internal const string ApiSignatureHeader = "X-Signature";
        internal const string ApiTimestampHeader = "X-Timestamp";

        private readonly YouSignApiConfiguration _configuration;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly CancellationTokenSource _tokenSource;

        private static readonly ILogger Logger = Log.Logger.ForContext<ApiKeyCredentialsProvider>();

        public ApiKeyCredentialsProvider(IOptions<YouSignApiConfiguration> configuration,
            IDateTimeProvider dateTimeProvider)
        {
            _configuration = configuration.Value;
            _dateTimeProvider = dateTimeProvider;

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