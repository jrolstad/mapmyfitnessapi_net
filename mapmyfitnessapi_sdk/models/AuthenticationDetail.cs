namespace mapmyfitnessapi_sdk.models
{
    public class AuthenticationDetail
    {
        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string Scope { get; set; }

        public int ExpiresIn { get; set; }

    }
}