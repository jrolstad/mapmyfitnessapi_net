namespace mapmyfitnessapi_sdk.users
{
    public class UserApiRequest:MapMyFitnessApiRequest
    {
        public int UserId { get; set; }

        public UserApiRequest WithUserId(string id)
        {
            UserId = id;

            return this;
        }
    }
}