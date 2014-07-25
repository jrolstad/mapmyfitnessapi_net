using mapmyfitnessapi_sdk.services;
using mapmyfitnessapi_sdk.unit.tests.helpers;
using mapmyfitnessapi_sdk.unit.tests.helpers.fakes;
using NUnit.Framework;
using System;
using mapmyfitnessapi_sdk.users;

namespace mapmyfitnessapi_sdk.unit.tests
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
			Assert.That (result.Id, Is.Not.EqualTo (0));
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
            var result = api.GetAuthenticatedUser(request);

            // Assert
            Assert.That(result.Id, Is.Not.EqualTo(0));
        }
	}
}

