using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Application.IntegrationTests
{
    public class AuthenticationControllerTests
    {
        private ApplicationDbContext context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>()
               .UseInMemoryDatabase(Guid.NewGuid().ToString())
               .Options);

        //private Medic medic = new Medic()
        //{
        //    Id = 1,
        //    Name = "Name",
        //    LastName = "Surname",
        //    Email = "test@email.com"
        //};

        [Fact]
        public void When_LoginIn_Expect_JWTToken()
        {
            var webAppFactory = new WebApplicationFactory<Program>();
            var httpClient = webAppFactory.CreateDefaultClient();

            //var response = httpClient.PostAsync("", new HttpContent());
        }
    }
}