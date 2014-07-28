using System;
using System.Net.Http;
using mapmyfitnessapi_sdk.services;
using mapmyfitnessapi_sdk.unit.tests.helpers.fakes;
using mapmyfitnessapi_sdk.users;

namespace mapmyfitnessapi_sdk.unit.tests.helpers
{
    public class TestCompositionRoot
    {
        public static UserClient GetUserClient(HttpClient httpClient)
        {
            var factory = new FakeHttpClientFactory(httpClient);
            var userClient = new UserClient("http://foo",factory);

            return userClient;
        }

        private class FakeHttpClientFactory : MmfHttpClientFactory
        {
            private readonly HttpClient _httpClient;

            public FakeHttpClientFactory(HttpClient httpClient)
            {
                _httpClient = httpClient;
            }

            public override HttpClient Create(Uri baseUri)
            {
                _httpClient.BaseAddress = baseUri;
                return _httpClient;
            }
        }
    }

   
}