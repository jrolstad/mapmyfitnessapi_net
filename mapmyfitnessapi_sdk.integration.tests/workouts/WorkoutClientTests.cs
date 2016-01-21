using System;
using mapmyfitnessapi_sdk.workouts;
using NUnit.Framework;

namespace mapmyfitnessapi_sdk.integration.tests.workouts
{
    [TestFixture]
    public class WorkoutClientTests
    {
        [Test]
        public void Get_AfterSpecificDate_GetsAllWorkoutsAfterThatDay()
        {
            // Arrange
            var client = new WorkoutClient();

            var request = new WorkoutApiRequest();
            request
                .WithApiKey(ConfigurationValues.ApiKey)
                .WithAccessToken(ConfigurationValues.AccessToken);
            request
                .WithUserId(ConfigurationValues.CurrentUserId)
                .WithStartedAfter(new DateTime(2016, 1, 1));

            // Act
            var response = client.Get(request);

            // Assert
            Assert.That(response,Is.Not.Empty);
        }
    }
}