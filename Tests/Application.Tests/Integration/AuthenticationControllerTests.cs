using api;
using api.Application.Common.Interfaces;
using Domain.Dtos;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Application.Tests.Integration
{
    public class AuthenticationControllerTests
    {
        private readonly HttpClient _client;

        public AuthenticationControllerTests()
        {
            var root = new InMemoryDatabaseRoot();
            _client = new WebApplicationFactory<Program>()
               .WithWebHostBuilder(builder =>
               {
                   builder.ConfigureServices(services =>
                   {
                       services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

                       services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("test", root));
                       services.AddScoped<IApplicationDbContext>(q => q.GetRequiredService<ApplicationDbContext>());
                   });
               })
               .CreateClient();
        }

        [Fact]
        public async void When_SignIn_Expect_JWTToken()
        {
            var response = await _client.PostAsJsonAsync("api/v1/Authentication/sign-up",
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
        }

        [Theory]
        [MemberData(nameof(GetRegisterUserDtoDataGenerator))]
        public async void When_SignIn_Expect_SuccsessfullLogin(RegisterUserDto dto)
        {
            var response = await _client.PostAsJsonAsync("api/v1/Authentication/sign-up", dto);
            var registeResponse = await response.Content.ReadFromJsonAsync<TokenDto>();

            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(registeResponse);
            Assert.True(registeResponse?.Token?.Length > 30);
        }

        [Fact]
        public async void When_SignInAsAdmin_Expect_BadRequest()
        {
            var registerDto = new RegisterUserDto()
            {
                Name = "Butthead",
                LastName = "123",
                Email = "butthead@gmail.com",
                Password = "1231232",
                Role = Domain.Enums.Role.Admin
            };

            var response = await _client.PostAsJsonAsync("api/v1/Authentication/sign-up", registerDto);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        [Theory]
        [MemberData(nameof(GetLoginUserDtoDataGenerator))]
        public async void When_UserIsNotLoggedIn_Expect_Unothorized(LoginUserDto dto)
        {
            var response = await _client.PostAsJsonAsync("api/v1/Authentication/login", dto);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.Unauthorized);
        }

        [Theory]
        [MemberData(nameof(GetLoginUserDtoDataGenerator))]
        public async void When_LoginDataOk_Expect_Success(LoginUserDto dto)
        {
            var reg = new RegisterUserDto
            {
                Email = dto.Email,
                Password = dto.Password,
                LastName = "asd",
                Name = "dsa",
                Role = Domain.Enums.Role.Patient
            }; 

            var signUpResponse = await _client.PostAsJsonAsync("api/v1/Authentication/sign-up", reg);
            var response = await _client.PostAsJsonAsync("api/v1/Authentication/login", dto);
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async void When_UserIsNull_Excpect_BadRequest()
        {
            var response = await _client.PostAsJsonAsync("api/v1/Authentication/login", new LoginUserDto());
            Assert.True(response.StatusCode == System.Net.HttpStatusCode.BadRequest);
        }

        public static IEnumerable<object[]> GetRegisterUserDtoDataGenerator()
        {
            yield return new object[]
            {
                new RegisterUserDto
                {
                    Name = "Butthead",
                    LastName = "123",
                    Email = "butthead@gmail.com",
                    Password = "1231232",
                    Role = Domain.Enums.Role.Patient
                }
            };

            yield return new object[]
            {
                new RegisterUserDto
                {
                    Name = "AsdD@asdasdBautasdasdthead",
                    LastName = "123",
                    Email = "asdasdasdasdasdasdasdasdasbutthead@gmail.com",
                    Password = "1231232",
                    Role = Domain.Enums.Role.Medic
                }
            };

            yield return new object[]
            {
                new RegisterUserDto
                {
                    Name = "AsdD@dasdasdasdasdasd",
                    LastName = "dasdasdasdSADASdasdasdasdasasda",
                    Email = "asdasdasdasdasdasdasdasdasbutthead@gmail.com",
                    Password = "1231232",
                    Role = Domain.Enums.Role.Patient
                }
            };
        }

        public static IEnumerable<object[]> GetLoginUserDtoDataGenerator()
        {
            yield return new object[]
            {
                new LoginUserDto
                {
                    Email = "butthead@gmail.com",
                    Password = "1231232"
                }
            };

            yield return new object[]
            {
                new LoginUserDto
                {
                    Email = "asdasdasdasdasdasdasdasdasbutthead@gmail.com",
                    Password = "1231232"
                }
            };

            yield return new object[]
            {
                new LoginUserDto
                {
                    Email = "asdasdasdasdasdasdasdasdasbutthead@gmail.com",
                    Password = "1231232"
                }
            };
        }
    }
}