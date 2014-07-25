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

        public string FakeContentType { get; set; }

        public HttpClient LastClient { get; set; }

        public override HttpClient Create(Uri baseUri)
        {
            var httpMessageHandler = new FakeHttpMessageHandler(FakeContent,FakeStatusCode, FakeContentType);
            var client = new HttpClient(httpMessageHandler);

            LastClient = client;

            return client;
        }

        public FakeHttpClientFactory WithJsonResponse(string content, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            FakeContent = content;
            FakeStatusCode = statusCode;
            FakeContentType = "application/json";

            return this;
        }

        
    }
}