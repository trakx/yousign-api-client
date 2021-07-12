using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Trakx.YouSign.ApiClient.Tests.Integration
{
    public sealed class UsersClientTests : YouSignBaseTestBase
    {

        public UsersClientTests(ITestOutputHelper output) 
            : base(output)
        {
        }

        [Fact]
        public async Task UsersClientTest()
        {
            var users = await UsersClient.UsersAsync();
            users.Result.Should().NotBeEmpty();
        }

    }
}
