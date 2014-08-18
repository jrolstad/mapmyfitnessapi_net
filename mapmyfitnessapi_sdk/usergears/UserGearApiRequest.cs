using mapmyfitnessapi_sdk.users;

namespace mapmyfitnessapi_sdk.usergears
{
    public class UserGearApiRequest : MapMyFitnessApiRequest
    {
        public int UserGearId { get; set; }

        public UserGearApiRequest WithUserGearId(int id)
        {
            UserGearId = id;

            return this;
        }
    }
}