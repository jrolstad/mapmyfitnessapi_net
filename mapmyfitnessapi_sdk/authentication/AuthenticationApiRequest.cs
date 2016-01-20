namespace mapmyfitnessapi_sdk.authentication
{
    public class AuthenticationApiRequest:MapMyFitnessApiRequest
    {
        public string ClientId { get; set; }

        public string ClientSecretKey { get; set; }

        public string AuthorizationCode { get; set; }

        public AuthenticationApiRequest WithClientId(string value)
        {
            this.ClientId = value;
            return this;
        }

        public AuthenticationApiRequest WithClientSecretKey(string value)
        {
            this.ClientSecretKey = value;
            return this;
        }

        public AuthenticationApiRequest WithAuthorizationCode(string value)
        {
            this.AuthorizationCode = value;
            return this;
        }
    }
}