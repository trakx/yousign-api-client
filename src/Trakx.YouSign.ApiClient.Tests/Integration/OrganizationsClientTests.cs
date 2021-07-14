using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Trakx.YouSign.ApiClient.Tests.Integration
{
    public sealed class OrganizationsClientTests : YouSignTestBase
    {

        public OrganizationsClientTests(ITestOutputHelper output) 
            : base(output)
        {
        }

        [Fact]
        public async Task OrganizationsAsync_should_retrieve_current_organizations()
        {
            var organizations = await OrganizationsClient.OrganizationsAsync();
            organizations.Result.Should().NotBeEmpty();
        }

    }
}
