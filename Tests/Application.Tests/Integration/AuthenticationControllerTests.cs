using api;
using api.Application.Common.Interfaces;
using Domain.Dtos;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Json;
using System.Xml.Linq;

namespace Application.Tests.Integration
{
    public class AuthenticationControllerTests
    {
        private readonly WebApplicationFactory<Program> _clientFactory;

        public AuthenticationControllerTests()
        {
            _clientFactory = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureServices(services =>
                   {
                       services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                       services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
                       services.AddScoped<IApplicationDbContext>(q => q.GetRequiredService<ApplicationDbContext>());
                   });
               });
        }

        [Fact]
        public async void When_SignIn_Expect_JWTToken()
        {
            var client = _clientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("api/v1/Authentication/sign-up",
                new RegisterUserDto()
                {
                    Name = "Butthead",
                    LastName = "123",
                    Email = "butthead@gmail.com",
                    Password = "1231232",
                    Role = Domain.Enums.Role.Patient
                });

            var registeResponse = await response.Content.ReadFromJsonAsync<TokenDto>();
            Assert.NotNull(registeResponse);
            Assert.True(registeResponse?.Token?.Length > 30);
            Assert.True(registeResponse?.Expires > DateTime.Now);   
        }
    }
}