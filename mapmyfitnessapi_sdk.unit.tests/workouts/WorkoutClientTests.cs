using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using mapmyfitnessapi_sdk.models;
using mapmyfitnessapi_sdk.unit.tests.helpers;
using mapmyfitnessapi_sdk.unit.tests.helpers.fakes;
using mapmyfitnessapi_sdk.users;
using mapmyfitnessapi_sdk.workouts;
using NUnit.Framework;

namespace mapmyfitnessapi_sdk.unit.tests.workouts
{
    [TestFixture]
    public class WorkoutClientTests
    {

        [Test]
        public void Get_WithOnlyUserId_GetsAllWorkoutsForTheUser()
        {
            // Arrange
            var request = new WorkoutApiRequest();
            request
                .WithUserId(1256)
                .WithAccessToken("someToken")
                .WithApiKey("someKey");

            var responseJson = TestFileReader.ReadFile("mapmyfitnessapi_sdk.unit.tests.workouts.workoutData.json");
            var httpClient = FakeHttpClient.WithJsonResponse(responseJson);

            var api = TestCompositionRoot.GetWorkoutClient(httpClient);

            // Act
            var result = api.Get(request);

            // Assert
            AssertRequestHeaders(httpClient, "someKey", "someToken");

            Assert.That(result.Count,Is.EqualTo(7));

            var firstWorkout = result.First();
            AssertWorkoutData(firstWorkout);
        }


        private static void AssertRequestHeaders(HttpClient client, string apiKey, string accessToken)
        {
            Assert.That(client.DefaultRequestHeaders.GetValues("Api-Key").First(), Is.EqualTo(apiKey));
            Assert.That(client.DefaultRequestHeaders.GetValues("Authorization").First(), Is.EqualTo(string.Format("Bearer {0}", accessToken)));
        }

        private static void AssertWorkoutData(Workout firstWorkout)
        {
            Assert.That(firstWorkout.StartDateTime, Is.EqualTo(new DateTime(2014, 7, 22, 13, 21, 40)));
            Assert.That(firstWorkout.UpdatedDateTime, Is.EqualTo(new DateTime(2014, 7, 22, 15, 1, 43)));
            Assert.That(firstWorkout.CreatedDateTime, Is.EqualTo(new DateTime(2014, 7, 22, 16, 1, 43)));
            Assert.That(firstWorkout.Name, Is.EqualTo(@"Run / Jog on July 22, 2014"));
            Assert.That(firstWorkout.Notes, Is.EqualTo("something interesting"));
            Assert.That(firstWorkout.ReferenceKey, Is.EqualTo("774969701"));
            Assert.That(firstWorkout.StartLocaleTimezone, Is.EqualTo("America/Los_Angeles"));
            Assert.That(firstWorkout.Source, Is.EqualTo("Unknown Garmin Device(006-B1345-00)"));
            Assert.That(firstWorkout.HasTimeSeries, Is.EqualTo(true));
            Assert.That(firstWorkout.IsVerified, Is.EqualTo(true));
            Assert.That(firstWorkout.ActiveTime, Is.EqualTo(3390));
            Assert.That(firstWorkout.Distance, Is.EqualTo(11341.99668096));
            Assert.That(firstWorkout.MaxSpeed, Is.EqualTo(5.293981792));
            Assert.That(firstWorkout.MinSpeed, Is.EqualTo(0.3000000502));
            Assert.That(firstWorkout.AverageSpeed, Is.EqualTo(3.3460005216));
            Assert.That(firstWorkout.ElapsedTime, Is.EqualTo(3720.0));
            Assert.That(firstWorkout.MetabolicEnergy, Is.EqualTo(4309520.0));

            Assert.That(firstWorkout.Id, Is.EqualTo(657440399));
            Assert.That(firstWorkout.SelfLink.Id, Is.EqualTo("657440399"));
            Assert.That(firstWorkout.SelfLink.Href, Is.EqualTo(@"/v7.0/workout/657440399/"));

            Assert.That(firstWorkout.Route, Is.EqualTo(480631396));
            Assert.That(firstWorkout.RouteLink.Id, Is.EqualTo("480631396"));
            Assert.That(firstWorkout.RouteLink.Href, Is.EqualTo(@"/v7.0/route/480631396/"));

            Assert.That(firstWorkout.ActivityType, Is.EqualTo(16));
            Assert.That(firstWorkout.ActivityTypeLink.Id, Is.EqualTo("16"));
            Assert.That(firstWorkout.ActivityTypeLink.Href, Is.EqualTo(@"/v7.0/activity_type/16/"));

            Assert.That(firstWorkout.User, Is.EqualTo(502434));
            Assert.That(firstWorkout.UserLink.Id, Is.EqualTo("502434"));
            Assert.That(firstWorkout.UserLink.Href, Is.EqualTo(@"/v7.0/user/502434/"));

            Assert.That(firstWorkout.Privacy, Is.EqualTo(1));
            Assert.That(firstWorkout.PrivacyLink.Id, Is.EqualTo("1"));
            Assert.That(firstWorkout.PrivacyLink.Href, Is.EqualTo(@"/v7.0/privacy_option/1/"));
        }
    }
}