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
			var request = new UserApiRequest ();
			request
				.WithAccessToken ("321a93d2329879543cb67c47c03e55f3c8e752b9")
				.WithApiKey ("wpma9vemz3pfu8tyq3y85va29dx23ff6");

			var api = new UserApi ();

			// Act
			var result = api.GetAuthenticatedUser(request);

			// Assert
			Assert.That (result.Id, Is.Not.EqualTo (0));
		}
	}
}

