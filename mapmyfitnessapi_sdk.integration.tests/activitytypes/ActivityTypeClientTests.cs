using mapmyfitnessapi_sdk.activitytypes;
using NUnit.Framework;

namespace mapmyfitnessapi_sdk.integration.tests.activitytypes
{
    [TestFixture]
    public class ActivityTypeClientTests
    {
        [Test]
        public void Get_GetsAllActivityTypes()
        {
            // Arrange
            var client = new ActivityTypeClient();

            var request = new ActivityTypeApiRequest();
            request
                .WithApiKey(ConfigurationValues.ApiKey)
                .WithAccessToken(ConfigurationValues.AccessToken);

            // Act
            var response = client.Get(request);

            // Assert
            Assert.That(response,Is.Not.Null);
        }
    }
}