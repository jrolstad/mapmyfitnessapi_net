using System;
using System.Net;
using System.Net.Http;
using mapmyfitnessapi_sdk.services;

namespace mapmyfitnessapi_sdk.unit.tests.helpers.fakes
{
    public class FakeHttpClientFactory:MmfHttpClientFactory
    {
        private string FakeContent { get; set; }

        private HttpStatusCode FakeStatusCode { get; set; }

        public HttpClient LastClient { get; set; }

        public override HttpClient Create(Uri baseUri)
        {
            var httpMessageHandler = FakeHttpJsonMessageHandler.GetHttpMessageHandler(FakeContent,FakeStatusCode);
            
            LastClient = new HttpClient(httpMessageHandler);

            return LastClient;
        }

        public FakeHttpClientFactory WithJsonResponse(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            FakeContent = content;
            FakeStatusCode = statusCode;

            return this;
        }
    }
}