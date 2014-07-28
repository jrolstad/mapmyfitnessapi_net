using System;
using System.Collections.Generic;
using mapmyfitnessapi_sdk.models;
using mapmyfitnessapi_sdk.services;

namespace mapmyfitnessapi_sdk.routes
{
    public class RouteClient
    {
         private readonly MmfHttpClientFactory _httpClientFactory;
        private readonly Uri _baseUrl;

        public RouteClient() : this("https://oauth2-api.mapmyapi.com")
        {

        }

        public RouteClient(string baseUrl):this(baseUrl,new MmfHttpClientFactory())
        {
            
        }

        public RouteClient(string baseUrl, MmfHttpClientFactory httpClientFactory)
        {
            _baseUrl = new Uri(baseUrl);
            _httpClientFactory = httpClientFactory;
        }

        public List<Route> Get(RouteApiRequest request)
        {
            throw new NotImplementedException();
        }
    }
}