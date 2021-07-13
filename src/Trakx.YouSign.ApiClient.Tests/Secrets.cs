using Trakx.Utils.Attributes;
using Trakx.Utils.Testing;

namespace Trakx.YouSign.ApiClient.Tests
{
    public record Secrets : SecretsBase
    {
#nullable disable
        [SecretEnvironmentVariable(nameof(YouSignApiConfiguration), nameof(YouSignApiConfiguration.ApiKey))]
        public string ApiKey { get; init; }
#nullable restore
    }
}
