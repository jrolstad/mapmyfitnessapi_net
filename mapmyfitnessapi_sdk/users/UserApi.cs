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

        public UserApi():this("https://oauth2-api.mapmyapi.com")
        {
            
        }

        public UserApi(string baseUrl)
        {
            _baseUrl = new Uri(baseUrl);
        }

        public User GetAuthenticatedUser(UserApiRequest request)
        {
            throw new NotImplementedException();
        }

        public User GetUser(UserApiRequest request)
        {
            var task = GetUserAsync(request);
            task.Wait();

            return task.Result;
        }

        private async Task<User> GetUserAsync(UserApiRequest request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseUrl;
                client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = await client.GetAsync("v7.0/user/self/");

                if (response.IsSuccessStatusCode)
                {
                    var user = await response.Content.ReadAsAsync<dynamic>();

                    return user;
                }

                throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}",response.StatusCode,response.ReasonPhrase));

            }
        }
    }
}