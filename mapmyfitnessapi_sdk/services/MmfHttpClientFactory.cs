using System;
using System.Net.Http;

namespace mapmyfitnessapi_sdk.services
{
    public class MmfHttpClientFactory
    {
        public virtual HttpClient Create(Uri baseUri)
        {
            return new HttpClient {BaseAddress = baseUri};
        }
    }
}