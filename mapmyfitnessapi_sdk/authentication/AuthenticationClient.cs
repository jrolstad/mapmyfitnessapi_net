using System;
using System.Collections.Generic;
using System.Net.Http;
using mapmyfitnessapi_sdk.models;
using mapmyfitnessapi_sdk.services;

namespace mapmyfitnessapi_sdk.authentication
{
    public class AuthenticationClient
    {
        private readonly MmfHttpClientFactory _httpClientFactory;
        private readonly Uri _baseUrl;

        public AuthenticationClient() : this("https://oauth2-api.mapmyapi.com")
        {

        }

        public AuthenticationClient(string baseUrl):this(baseUrl,new MmfHttpClientFactory())
        {
            
        }

        public AuthenticationClient(string baseUrl, MmfHttpClientFactory httpClientFactory)
        {
            _baseUrl = new Uri(baseUrl);
            _httpClientFactory = httpClientFactory;
        }

        public AuthenticationDetail GetAccessToken(AuthenticationApiRequest request)
        {
            using (var client = _httpClientFactory.Create(_baseUrl))
            {
                client.BaseAddress = _baseUrl;
                client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);

                var requestContent = CreateRequest(request);
                var response = client.PostAsync("v7.0/oauth2/access_token/", requestContent).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseData = response.Content.ReadAsAsync<dynamic>().Result;
                    var authorizationData = Map(responseData);

                    return authorizationData;
                }

                throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
                    response.ReasonPhrase));

            }
        }

        private static FormUrlEncodedContent CreateRequest(AuthenticationApiRequest request)
        {
            var requestData = new Dictionary<string, string>
            {
                {"client_id", request.ClientId},
                {"client_secret", request.ClientSecretKey},
                {"grant_type", "authorization_code"},
                {"code", request.AuthorizationCode}
            };
            var content = new FormUrlEncodedContent(requestData);
            return content;
        }

        private AuthenticationDetail Map(dynamic responseData)
        {
            var detail = new AuthenticationDetail
            {
                AccessToken = responseData.access_token,
                Scope = responseData.scope,
                ExpiresIn = responseData.expires_in,
                RefreshToken = responseData.refresh_token
            };

            return detail;
        }
    }
}