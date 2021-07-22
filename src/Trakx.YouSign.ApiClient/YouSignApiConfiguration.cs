using System.ComponentModel.DataAnnotations;
using Trakx.Utils.Attributes;

namespace Trakx.YouSign.ApiClient
{
    public record YouSignApiConfiguration
    {
#nullable disable
        [Required]
        public string BaseUrl { get; init; }

        [Required]
        public string WebAppUrl { get; init; }

        public string CallbackUrl { get; init; }

        public int? InitialRetryDelayInMilliseconds { get; init; }

        public int? MaxRetryCount { get; init; }

        [SecretEnvironmentVariable]
        public string ApiKey { get; init; }

#nullable restore
    }
}
