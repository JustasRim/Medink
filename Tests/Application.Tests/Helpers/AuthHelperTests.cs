using Microsoft.Extensions.Configuration;
using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Helpers.Tests
{
    public class AuthHelperTests
    {
        private IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                new Dictionary<string, string> 
                {
                    {"Secrets:JwtIssuer", "www.isuer.com"},
                    {"Secrets:JwtAudience", "www.isuer.com"},
                    {"Secrets:JwtSecret", "secretsecretsecretsecretsecretsecretsecretsecretsecretsecrets"},
                }).Build();
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.

        [Fact]
        public void When_GivenUser_Expect_TokenDto()
        {
            AuthHelper helper = new AuthHelper(configuration);
            var user = new User()
            {
                Email = "test@test.com",
                Role = Role.Patient,
                SaltedHash = "123123123"
            };

            var token = helper.GetAuthToken(user);
            Assert.NotNull(token);
        }

        [Fact]
        public void When_UserIsEmpty_Expect_Exception()
        {
            AuthHelper helper = new AuthHelper(configuration);

            Assert.Throws<ArgumentNullException>(() => helper.GetAuthToken(new User()));
        }

        [Fact]
        public void When_UserIsNull_Expect_Exception()
        {
            AuthHelper helper = new AuthHelper(configuration);

            Assert.Throws<NullReferenceException>(() => helper.GetAuthToken(null));
        }
    }
}