namespace mapmyfitnessapi_sdk.authentication
{
    public class AuthenticationApiRequest:MapMyFitnessApiRequest
    {
        public string ClientId { get; set; }

        public string ClientSecretKey { get; set; }

        public string AuthorizationCode { get; set; }
    }
}