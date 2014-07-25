using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Threading.Tasks;
using mapmyfitnessapi_sdk.models;

namespace mapmyfitnessapi_sdk.users
{
    public class UserApi
    {
        private readonly Uri _baseUrl;

        public UserApi() : this("https://oauth2-api.mapmyapi.com")
        {

        }

        public UserApi(string baseUrl)
        {
            _baseUrl = new Uri(baseUrl);
        }

        public User GetAuthenticatedUser(UserApiRequest request)
        {
			using (var client = new HttpClient())
			{
				client.BaseAddress = _baseUrl;
				client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
				client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				var response = client.GetAsync("v7.0/user/self/").Result;
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

        public User GetUser(UserApiRequest request)
        {
			throw new NotImplementedException();
        }

        private User Map(dynamic userData)
        {
			return new User
			{ 
				LastName = userData.last_name,
				Id = userData.id,
				ReceivePromotions = userData.communication.promotions,
				ReceiveNewsletter = userData.communication.newsletter,
				ReceiveSystemMessages = userData.communication.system_messages
			};
        }
    }
}