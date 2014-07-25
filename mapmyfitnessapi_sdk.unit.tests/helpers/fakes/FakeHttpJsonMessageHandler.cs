using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace mapmyfitnessapi_sdk.unit.tests.helpers.fakes
{
    public class FakeHttpJsonMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public static HttpMessageHandler GetHttpMessageHandler(string content, HttpStatusCode httpStatusCode, string mediaType = "application/json")
        {
            var memStream = new MemoryStream();

            var sw = new StreamWriter(memStream);
            sw.Write(content);
            sw.Flush();
            memStream.Position = 0;

            var httpContent = new StreamContent(memStream);
            httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue(mediaType);

            var response = new HttpResponseMessage()
            {
                StatusCode = httpStatusCode,
                Content = httpContent,
            };


            var messageHandler = new FakeHttpJsonMessageHandler(response);
          
            return messageHandler;
        }

        public FakeHttpJsonMessageHandler(HttpResponseMessage response)
        {
            _response = response;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<HttpResponseMessage>();

            tcs.SetResult(_response);

            return tcs.Task;
        }
    }
}