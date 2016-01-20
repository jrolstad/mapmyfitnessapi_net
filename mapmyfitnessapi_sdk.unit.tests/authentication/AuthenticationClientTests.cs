using mapmyfitnessapi_sdk.authentication;
using NUnit.Framework;

namespace mapmyfitnessapi_sdk.unit.tests.authentication
{
    [TestFixture]
    public class AuthenticationClientTests
    {
        [Test]
        public void GetAccessToken_ValidCredentials_ReturnsAccessToken()
        {
            // Arrange
            var client = new AuthenticationClient();

            var request = new AuthenticationApiRequest()
                .WithClientId("some client id")
                .WithClientSecretKey("some client secret key")
                .WithAuthorizationCode("some authorization code");

            // Act
            var response = client.GetAccessToken(request);

            // Assert
            Assert.That(response.AccessToken,Is.Not.Null);
            Assert.That(response.RefreshToken,Is.Not.Null);
            Assert.That(response.ExpiresIn,Is.GreaterThan(0));
            Assert.That(response.Scope,Is.Not.Null);
        }
    }
}