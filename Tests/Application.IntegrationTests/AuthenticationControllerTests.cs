using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Application.IntegrationTests
{
    public class AuthenticationControllerTests
    {
        [Fact]
        public void When_LoginIn_Expect_JWTToken()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            var response = httpClient.PostAsync("", new HttpContent());
        }
    }
}