using System.Security.Cryptography.X509Certificates;
using mapmyfitnessapi_sdk.usergears;
using NUnit.Framework;

namespace mapmyfitnessapi_sdk.integration.tests.usergears
{
    [TestFixture]
    public class UserGearClientTests
    {
        [Test]
        public void Get_GetsAllUserGear()
        {
            // Arrange
            var client = new UserGearClient();

            var request = new UserGearApiRequest();
            request
                .WithApiKey(ConfigurationValues.ApiKey)
                .WithAccessToken(ConfigurationValues.AccessToken);

            // Act
            var response = client.Get(request);

            // Assert
            Assert.That(response,Is.Not.Empty);
        }

        [Test]
        public void Get_WithUserGearId_GetsTheSpecificGear()
        {
            // Arrange
            var client = new UserGearClient();

            var request = new UserGearApiRequest();
            request
                .WithUserGearId(ConfigurationValues.UserGearId)
                .WithApiKey(ConfigurationValues.ApiKey)
                .WithAccessToken(ConfigurationValues.AccessToken);

            // Act
            var response = client.GetById(request);

            // Assert
            Assert.That(response, Is.Not.Null);
        }
    }
}