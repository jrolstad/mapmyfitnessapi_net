using System;
using System.Linq;
using System.Net.Http;
using mapmyfitnessapi_sdk.models;
using mapmyfitnessapi_sdk.unit.tests.helpers;
using mapmyfitnessapi_sdk.unit.tests.helpers.fakes;
using mapmyfitnessapi_sdk.users;
using NUnit.Framework;

namespace mapmyfitnessapi_sdk.unit.tests.users
{
	[TestFixture]
	public class UserApiTests
	{
		[Test]
		public void GetAuthenticatedUser_WhenCalled_GetsTheCurrentUser ()
		{
			// Arrange
		    var request = new UserApiRequest();
			request
				.WithAccessToken ("someToken")
				.WithApiKey ("someKey");

		    var responseJson = TestFileReader.ReadFile("mapmyfitnessapi_sdk.unit.tests.users.userData.json");

		    var clientFactory = new FakeHttpClientFactory()
                .WithJsonResponse(responseJson);
            
		    var api = new UserClient("http://foo", clientFactory);

			// Act
			var result = api.GetAuthenticatedUser(request);

			// Assert
            AssertRequestHeaders(clientFactory.LastClient,"someKey","someToken");
		    AssertUserDataIsFromJson(result);
		}

	    [Test]
        public void GetUser_WhenCalled_GetsTheUser()
        {
            // Arrange
            var request = new UserApiRequest();
            request
                .WithUserId(502434)
                .WithAccessToken("someToken")
                .WithApiKey("someKey");

            var responseJson = TestFileReader.ReadFile("mapmyfitnessapi_sdk.unit.tests.users.userData.json");
            var clientFactory = new FakeHttpClientFactory()
                .WithJsonResponse(responseJson);

            var api = new UserClient("http://foo", clientFactory);

            // Act
            var result = api.Get(request);

            // Assert
            AssertRequestHeaders(clientFactory.LastClient, "someKey", "someToken");
            AssertUserDataIsFromJson(result);
        }

        private static void AssertRequestHeaders(HttpClient client, string apiKey, string accessToken)
        {
            Assert.That(client.DefaultRequestHeaders.GetValues("Api-Key").First(), Is.EqualTo(apiKey));
            Assert.That(client.DefaultRequestHeaders.GetValues("Authorization").First(), Is.EqualTo(string.Format("Bearer {0}", accessToken)));
        }

        private static void AssertUserDataIsFromJson(User result)
        {
            Assert.That(result.Id, Is.EqualTo(502434));
            Assert.That(result.LastName, Is.EqualTo("Rolstad"));
            Assert.That(result.Weight, Is.EqualTo(83.00740371));
            Assert.That(result.ReceivePromotions, Is.EqualTo(true));
            Assert.That(result.ReceiveNewsletter, Is.EqualTo(true));
            Assert.That(result.ReceiveSystemMessages, Is.EqualTo(false));
            Assert.That(result.Height, Is.EqualTo(1.8796));
            Assert.That(result.Hobbies, Is.EqualTo("I feel like running"));
            Assert.That(result.DateJoined, Is.EqualTo(new DateTime(2008, 5, 6, 18, 39, 7)));
            Assert.That(result.FirstName, Is.EqualTo("Josh"));
            Assert.That(result.DisplayName, Is.EqualTo("Josh Rolstad"));
            Assert.That(result.Introduction, Is.EqualTo("what I'm about"));
            Assert.That(result.DisplayMeasurementSystem, Is.EqualTo("imperial"));
            Assert.That(result.LastLogin, Is.EqualTo(new DateTime(2014, 2, 22, 2, 39, 26)));
            Assert.That(result.GoalStatement, Is.Null);
            Assert.That(result.Email, Is.EqualTo("jrolstad@gmail.com"));
            Assert.That(result.Country, Is.EqualTo("US"));
            Assert.That(result.Region, Is.EqualTo("WA"));
            Assert.That(result.Locality, Is.EqualTo("Seattle"));
            Assert.That(result.Address, Is.EqualTo("1918 8th Ave"));
            Assert.That(result.UserName, Is.EqualTo("jrolstad"));
            Assert.That(result.TwitterSharingEnabled, Is.False);
            Assert.That(result.FacebookSharingEnabled, Is.False);
            Assert.That(result.LastInitial, Is.EqualTo("R."));
            Assert.That(result.Gender, Is.EqualTo("M"));
            Assert.That(result.TimeZone, Is.EqualTo(@"America/Los_Angeles"));
            Assert.That(result.Birthdate, Is.EqualTo(new DateTime(1977, 3, 30, 8, 0, 0)));
            Assert.That(result.ProfileStatement, Is.EqualTo("what I do"));
        }
	}
}

