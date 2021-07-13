using System;
using System.Threading.Tasks;
using FluentAssertions;
using Trakx.YouSign.ApiClient.Tests.Properties;
using Xunit;
using Xunit.Abstractions;

namespace Trakx.YouSign.ApiClient.Tests.Integration
{
    public sealed class FilesClientTests : YouSignBaseTestBase
    {

        public FilesClientTests(ITestOutputHelper output) 
            : base(output)
        {
        }

        [Fact]
        public async Task FilesAsync_should_retrieve_current_organizations()
        {
            var fileName = $"{Guid.NewGuid()}.pdf";
            var fileContent = Convert.ToBase64String(Resources.TestPdf);
            var file = await FilesClient.FilesAsync(new FileInput
            {
                Name = fileName,
                Content = fileContent
            });
            file.Result.Id.Should().StartWith("/files/");
            file.Result.ContentType.Should().Be("application/pdf");
            file.Result.Name.Should().Be(fileName);
        }

    }
}
