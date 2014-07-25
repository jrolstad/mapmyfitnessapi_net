using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using mapmyfitnessapi_sdk.models;

namespace mapmyfitnessapi_sdk.workouts
{
    public class WorkoutClient
    {
        private readonly Uri _baseUrl;

        public WorkoutClient() : this("https://oauth2-api.mapmyapi.com")
        {

        }

        public WorkoutClient(string baseUrl)
        {
            _baseUrl = new Uri(baseUrl);
        }

        public List<Workout> Get(WorkoutApiRequest request)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = _baseUrl;
                client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = string.Format("v7.0/workout/?user={0}", request.UserId);

                if (request.ActivityType.HasValue)
                    requestUri += string.Format("&activity_type={0}", request.ActivityType);

                if (request.StartedAfter.HasValue)
                    requestUri += string.Format("&started_after={0:u}", request.StartedAfter);

                if (request.StartedBefore.HasValue)
                    requestUri += string.Format("&started_before={0:u}", request.StartedAfter);

                var response = client.GetAsync(requestUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var workoutData = response.Content.ReadAsAsync<dynamic>().Result;
                    var workouts = Map(workoutData);

                    return workouts;
                }

                throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
                    response.ReasonPhrase));

            }
        }

        private List<Workout> Map(dynamic workoutData)
        {
            var workouts = new List<Workout>();

            foreach (var item in workoutData._embedded)
            {
                var workout = MapWorkout(item);
                workouts.Add(workout);
            }

            return workouts;
        }

        private Workout MapWorkout(dynamic item)
        {
            var startDateTime = MapDateTime(item.start_datetime);
            var selfLink = MapLink(item._links.self);

            var workout = new Workout
            {
               StartDateTime = startDateTime,
               Name = item.name,
               Id = selfLink.Id,
               SelfLink = selfLink
            };

            return workout;
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