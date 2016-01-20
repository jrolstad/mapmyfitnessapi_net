using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Net.Http.Headers;
using mapmyfitnessapi_sdk.models;
using System.Linq;
using mapmyfitnessapi_sdk.services;
using mapmyfitnessapi_sdk.utilities;

namespace mapmyfitnessapi_sdk.users
{
    public class UserClient
    {
        private readonly MmfHttpClientFactory _httpClientFactory;
        private readonly Uri _baseUrl;

        public UserClient() : this("https://oauth2-api.mapmyapi.com")
        {

        }

        public UserClient(string baseUrl):this(baseUrl,new MmfHttpClientFactory())
        {
            
        }

        public UserClient(string baseUrl, MmfHttpClientFactory httpClientFactory)
        {
            _baseUrl = new Uri(baseUrl);
            _httpClientFactory = httpClientFactory;
        }

        public User GetAuthenticatedUser(UserApiRequest request)
        {
            using (var client = _httpClientFactory.Create(_baseUrl))
			{
				client.BaseAddress = _baseUrl;
				client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync("v7.1/user/self/").Result;
				if (response.IsSuccessStatusCode)
				{
					var userData = response.Content.ReadAsAsync<dynamic>().Result;
					var user = Map(userData);

					return user;
				}

				throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
					response.ReasonPhrase));

			}
        }

        public User Get(UserApiRequest request)
        {
            using (var client = _httpClientFactory.Create(_baseUrl))
            {
                client.BaseAddress = _baseUrl;
                client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = string.Format("v7.1/user/{0}/",request.UserId);
                var response = client.GetAsync(requestUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var userData = response.Content.ReadAsAsync<dynamic>().Result;
                    var user = Map(userData);

                    return user;
                }

                throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
                    response.ReasonPhrase));

            }
        }

        private User Map(dynamic userData)
        {

            var dateJoined = MapDateTime(userData.date_joined);
            var lastLogin = MapDateTime(userData.last_login);
            var birthDate = MapDateTime(userData.birthdate);

            var documentationLink = LinkMapper.MapLink(userData._links.documentation);
            var deactivationLink = LinkMapper.MapLink(userData._links.deactivation);
            var userAchievementLink = LinkMapper.MapLink(userData._links.user_achievements); 
            var friendshipLink = LinkMapper.MapLink(userData._links.friendships); 
            var workoutLink = LinkMapper.MapLink(userData._links.workouts); 
            var selfLink = LinkMapper.MapLink(userData._links.self);

            var imageLinks = LinkMapper.MapLinkCollection(userData._links.image);
            var privacyLinks = LinkMapper.MapLinkCollection(userData._links.privacy);
            var statisticLinks = LinkMapper.MapLinkCollection(userData._links.stats);

            dynamic sharingData = userData.sharing ?? new { twitter = (bool?)null,facebook = (bool?)null };
            dynamic locationData = userData.location ?? new {country = (string)null, region = (string)null, locality = (string)null, address = (string)null };
            dynamic communicationData = userData.communication ?? new {promotions=(bool?)null, newsletter = (bool?)null,system_messages = (string)null };

            return new User
			{ 
				LastName = userData.last_name,
                Weight = userData.weight,
                Height = userData.height,
                Hobbies = userData.hobbies,
                DateJoined = dateJoined,
				Id = userData.id,
				ReceivePromotions = communicationData.promotions,
				ReceiveNewsletter = communicationData.newsletter,
				ReceiveSystemMessages = communicationData.system_messages,
                FirstName = userData.first_name,
                DisplayName = userData.display_name,
                Introduction = userData.introduction,
                DisplayMeasurementSystem = userData.display_measurement_system,
                LastLogin = lastLogin,
                GoalStatement = userData.goal_statement,
                Email = userData.email,
                Country = locationData.country,
                Region = locationData.region,
                Locality = locationData.locality,
                Address = locationData.address,
                UserName = userData.username,
                TwitterSharingEnabled = sharingData.twitter,
                FacebookSharingEnabled = sharingData.facebook,
                LastInitial = userData.last_initial,
                Gender = userData.gender,
                TimeZone = userData.time_zone,
                Birthdate = birthDate,
                ProfileStatement = userData.profile_statement,
                DocumentationLink = documentationLink,
                DeactivationLink = deactivationLink,
                UserAchievementLink = userAchievementLink,
                FriendshipsLink = friendshipLink,
                WorkoutsLink = workoutLink,
                SelfLink = selfLink,
                Images = imageLinks,
                PrivacySettings = privacyLinks,
                Statistics = statisticLinks
			};
        }

        private static DateTime? MapDateTime(dynamic dateValue)
        {
            if (dateValue == null || string.IsNullOrWhiteSpace(dateValue.ToString()))
                return null;

            var date = DateTime.Parse(dateValue.ToString()).ToUniversalTime();

            return date;
        }

    }
}