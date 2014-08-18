using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using mapmyfitnessapi_sdk.models;
using mapmyfitnessapi_sdk.services;

namespace mapmyfitnessapi_sdk.workouts
{
    public class WorkoutClient
    {
        private readonly Uri _baseUrl;
        private MmfHttpClientFactory _httpClientFactory;

        public WorkoutClient() : this("https://oauth2-api.mapmyapi.com")
        {

        }

        public WorkoutClient(string baseUrl):this(baseUrl,new MmfHttpClientFactory())
        {
            
        }

        public WorkoutClient(string baseUrl, MmfHttpClientFactory httpClientFactory)
        {
            _baseUrl = new Uri(baseUrl);
            _httpClientFactory = httpClientFactory;
        }

        public List<Workout> Get(WorkoutApiRequest request)
        {
            using (var client = _httpClientFactory.Create(_baseUrl))
            {
                client.BaseAddress = _baseUrl;
                client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = MapRequestToUrlParameters(request);

                if (request.WorkoutId.HasValue)
                {
                    var singleWorkout = GetWorkout(client, requestUri);
                    return singleWorkout;
                }

                var workouts = GetWorkouts(client, requestUri);

                return workouts;
            }
        }

        private static string MapRequestToUrlParameters(WorkoutApiRequest request)
        {
            if (request.WorkoutId.HasValue)
            {
                var workoutRequestUri = string.Format("v7.0/workout/{0}/", request.WorkoutId);

                if (request.TimeSeries.HasValue && request.TimeSeries == true)
                    workoutRequestUri += "?field_set=time_series";

                return workoutRequestUri;
            }

            var requestUri = string.Format("v7.0/workout/?user={0}", request.UserId);

            if (request.ActivityType.HasValue)
                requestUri += string.Format("&activity_type={0}", request.ActivityType);

            if (request.StartedAfter.HasValue)
                requestUri += string.Format("&started_after={0:u}", request.StartedAfter);

            if (request.StartedBefore.HasValue)
                requestUri += string.Format("&started_before={0:u}", request.StartedAfter);

          

            return requestUri;
        }

        private List<Workout> GetWorkouts(HttpClient client, string requestUri)
        {
            var response = client.GetAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var workoutData = response.Content.ReadAsAsync<dynamic>().Result;

                var workouts = Map(workoutData);

                var nextLink = MapLink(workoutData._links.next);

                if (nextLink != null)
                {
                    var nextWorkouts = GetWorkouts(client, nextLink.Href);
                    workouts.AddRange(nextWorkouts);
                }

                return workouts;
            }

            

            throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
                response.ReasonPhrase));
        }

        private List<Workout> GetWorkout(HttpClient client, string requestUri)
        {
            var response = client.GetAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var workoutData = response.Content.ReadAsAsync<dynamic>().Result;

                var workout = MapWorkout(workoutData);

                return new List<Workout>{workout};
            }

            throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
                response.ReasonPhrase));
        }

        private List<Workout> Map(dynamic workoutData)
        {
            var workouts = new List<Workout>();

            foreach (var item in workoutData._embedded.workouts)
            {
                var workout = MapWorkout(item);
                workouts.Add(workout);
            }

            return workouts;
        }

        private Workout MapWorkout(dynamic item)
        {
        
            var selfLink = MapLink(item._links.self);
            var workoutId = Int32.Parse(selfLink.Id);

            try
            {
                var rawData = item.ToString();

                var startDateTime = MapDateTime(item.start_datetime);
                var updatedDateTime = MapDateTime(item.updated_datetime);
                var createdDateTime = MapDateTime(item.created_datetime);

                var routeLink = MapLink(item._links.route);
                var routeId = routeLink != null ? Int32.Parse(routeLink.Id):null;

                var activityTypeLink = MapLink(item._links.activity_type);
                var activityTypeId = Int32.Parse(activityTypeLink.Id);

                var userLink = MapLink(item._links.user);
                var userId = Int32.Parse(userLink.Id);

                var privacyLink = MapLink(item._links.privacy);
                var privacyId = Int32.Parse(privacyLink.Id);


                var workout = new Workout
                {
                    StartDateTime = startDateTime,
                    UpdatedDateTime = updatedDateTime,
                    CreatedDateTime = createdDateTime,
                    Notes = item.notes,
                    ReferenceKey = item.reference_key,
                    StartLocaleTimezone = item.start_locale_timezone,
                    HasTimeSeries = item.has_time_series,
                    IsVerified = item.is_verified,
                    ActiveTime = item.aggregates.active_time_total,
                    Distance = item.aggregates.distance_total,
                    MaxSpeed = item.aggregates.speed_max,
                    MinSpeed = item.aggregates.speed_min,
                    AverageSpeed = item.aggregates.speed_avg,
                    ElapsedTime = item.aggregates.elapsed_time_total,
                    MetabolicEnergy = item.aggregates.metabolic_energy_total,
                    Source = item.source,
                    Name = item.name,
                    Id = workoutId,
                    SelfLink = selfLink,
                    Route = routeId,
                    RouteLink = routeLink,
                    ActivityType = activityTypeId,
                    ActivityTypeLink = activityTypeLink,
                    User = userId,
                    UserLink = userLink,
                    Privacy = privacyId,
                    PrivacyLink = privacyLink,
                    RawData = rawData
                };

                return workout;
            }
            catch (Exception exception)
            {
                var message = string.Format("Unable to map workout {0}", workoutId);

                throw new ApplicationException(message, exception);
            }
           
        }

        private static DateTime? MapDateTime(dynamic dateValue)
        {
            if (dateValue == null || string.IsNullOrWhiteSpace(dateValue.ToString()))
                return null;

            var date = DateTime.Parse(dateValue.ToString()).ToUniversalTime();

            return date;
        }

        private static List<Link> MapLinkCollection(dynamic linkData)
        {
            if(linkData == null)
                return new List<Link>();

            var links = new List<Link>();
            foreach (var linkItem in linkData)
            {
                var link = new Link
                {
                    Href = linkItem.href,
                    Id = linkItem.id,
                    Name = linkItem.name
                };
                links.Add(link);
            }
            return links;
        }

        private static Link MapLink(dynamic linkData)
        {
           
            List<Link> links = MapLinkCollection(linkData);
            var link = links.FirstOrDefault();

            return link;
        }
    }
}