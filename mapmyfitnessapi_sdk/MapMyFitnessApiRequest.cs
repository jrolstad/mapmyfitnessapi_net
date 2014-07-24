namespace mapmyfitnessapi_sdk
{
    public class MapMyFitnessApiRequest
    {
        public string ApiKey { get; set; }

        public string AccessToken { get; set; }

        public MapMyFitnessApiRequest WithApiKey(string key)
        {
            ApiKey = key;

            return this;
        }

        public MapMyFitnessApiRequest WithAccessToken(string token)
        {
            AccessToken = token;

            return this;
        }
    }
}