using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Trakx.YouSign.ApiClient.Tests.Properties;
using Xunit;
using Xunit.Abstractions;

namespace Trakx.YouSign.ApiClient.Tests.Integration
{
    public sealed class ProceduresClientTests : YouSignTestBase
    {

        public ProceduresClientTests(ITestOutputHelper output) 
            : base(output)
        {
        }

        [Fact]
        public async Task ProceduresClientTest_should_create_a_new_memberid_to_be_signed()
        {
            var fileName = $"{Guid.NewGuid()}.pdf";
            var fileContent = Convert.ToBase64String(Resources.TestPdf);
            var file = await FilesClient.FilesAsync(new FileInput
            {
                Name = fileName,
                Content = fileContent
            });

            var firstName = MockCreator.GetRandomString(10);
            var lastName = MockCreator.GetRandomString(10);
            var procedureName = "My first procedure";
            var procedureDescription = "Awesome! Here is the description of my first procedure";
            var email = MockCreator.GetRandomString(10) + "@trakx.io";
            var procedure = new ProcedureInput
            {
                Name = procedureName,
                Description = procedureDescription,
                Members = new List<MemberInput>
                {
                    new ()
                    {
                        Firstname = firstName,
                        Lastname = lastName,
                        Email = email,
                        Phone = "+5551999999998",
                        // OperationLevel = MemberInputOperationLevel.Advanced,
                        FileObjects = new List<FileObjectInput>
                        {
                            new ()
                            {
                                File = file.Result.Id,
                                Page = 1,
                                Position = "230,499,464,589",
                                Mention = "Read and approved",
                                Mention2 = $"Signed by {firstName} {lastName}"
                            }
                        }
                    }
                }
            };
            var procedureResult = await ProceduresClient.ProceduresAsync(procedure);
            procedureResult.Result.Id.Should().StartWith("/procedures/");
            procedureResult.Result.Name.Should().Be(procedureName);
            procedureResult.Result.Description.Should().Be(procedureDescription);
            procedureResult.Result.Members.Should().NotBeEmpty();
            procedureResult.Result.Members[0].Firstname.Should().Be(firstName);
            procedureResult.Result.Members[0].Lastname.Should().Be(lastName);
            procedureResult.Result.Members[0].Email.Should().Be(email);

        }

    }
}
