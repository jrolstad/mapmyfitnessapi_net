using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace mapmyfitnessapi_sdk.unit.tests.helpers.fakes
{
    public class FakeHttpMessageHandler : HttpMessageHandler
    {
        private readonly HttpResponseMessage _response;

        public FakeHttpMessageHandler(string content, HttpStatusCode httpStatusCode, string mediaType)
        {
            var httpContent = CreateHttpContent(content, mediaType);
            _response = CreateResponseMessage(httpStatusCode, httpContent);
        }

        private static HttpResponseMessage CreateResponseMessage(HttpStatusCode httpStatusCode, HttpContent httpContent)
        {
            var response = new HttpResponseMessage()
            {
                StatusCode = httpStatusCode,
                Content = httpContent,
            };

            return response;
        }

        private static HttpContent CreateHttpContent(string content, string mediaType)
        {
            var memStream = new MemoryStream();

            var sw = new StreamWriter(memStream);
            sw.Write(content);
            sw.Flush();
            memStream.Position = 0;

            var httpContent = new StreamContent(memStream);
            httpContent.Headers.ContentType = new MediaTypeWithQualityHeaderValue(mediaType);

            return httpContent;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<HttpResponseMessage>();

            tcs.SetResult(_response);

            return tcs.Task;
        }
    }
}