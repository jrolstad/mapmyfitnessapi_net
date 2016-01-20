namespace mapmyfitnessapi_sdk.activitytypes
{
    public class ActivityTypeApiRequest : MapMyFitnessApiRequest
    {
        public int ActivityTypeId { get; set; }

        public ActivityTypeApiRequest WithActivityTypeId(int id)
        {
            ActivityTypeId = id;

            return this;
        }
    }
}