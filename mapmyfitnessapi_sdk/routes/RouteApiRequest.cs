namespace mapmyfitnessapi_sdk.routes
{
    public class RouteApiRequest:MapMyFitnessApiRequest
    {
        public int UserId { get; set; }

        public int? RouteId { get; set; }

        public bool DetailedData { get; set; }

        public RouteApiRequest WithUserId(int id)
        {
            UserId = id;

            return this;
        }

        public RouteApiRequest WithRouteId(int id)
        {
            RouteId = id;

            return this;
        }

        public RouteApiRequest WithDetailedData(bool value = true)
        {
            DetailedData = value;

            return this;
        }

       
    }
}