using System.Net;
using System.Net.Http;

namespace mapmyfitnessapi_sdk.unit.tests.helpers.fakes
{
    public class FakeHttpClient:HttpClient
    {
        public FakeHttpClient(string content, HttpStatusCode httpStatusCode, string mediaType)
            : base(new FakeHttpMessageHandler(content,httpStatusCode,mediaType))
        {
            
        }

        public static FakeHttpClient WithJsonResponse(string content, HttpStatusCode httpStatusCode = HttpStatusCode.OK)
        {
            const string mediaType = @"application/json";
            return new FakeHttpClient(content,httpStatusCode,mediaType);
        }

    }
}