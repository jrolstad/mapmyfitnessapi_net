using System.Security.Cryptography.X509Certificates;
using mapmyfitnessapi_sdk.users;
using NUnit.Framework;

namespace mapmyfitnessapi_sdk.integration.tests.users
{
    [TestFixture]
    public class UserClientTests
    {
        [Test]
        public void GetAuthenticatedUser_ValidCredentials_ReturnsMe()
        {
            // Arrange
            var client = new UserClient();

            var request = new UserApiRequest();
            request
                .WithApiKey(ConfigurationValues.ApiKey)
                .WithAccessToken(ConfigurationValues.AccessToken);

            // Act
            var response = client.GetAuthenticatedUser(request);

            // Assert
            Assert.That(response.UserName,Is.Not.Null);
        }

        [Test]
        public void GetUser_ValidCredentials_ReturnsSpecifiedUser()
        {
            // Arrange
            var client = new UserClient();

            var request = new UserApiRequest();
            request
                .WithUserId(ConfigurationValues.OtherUserId)
                .WithApiKey(ConfigurationValues.ApiKey)
                .WithAccessToken(ConfigurationValues.AccessToken);

            // Act
            var response = client.GetAuthenticatedUser(request);

            // Assert
            Assert.That(response.UserName, Is.Not.Null);
        }
    }
}