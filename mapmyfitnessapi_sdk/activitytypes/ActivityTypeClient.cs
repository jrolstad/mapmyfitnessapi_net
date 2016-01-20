using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using mapmyfitnessapi_sdk.models;
using mapmyfitnessapi_sdk.services;
using mapmyfitnessapi_sdk.users;

namespace mapmyfitnessapi_sdk.activitytypes
{
    public class ActivityTypeClient
    {
        private readonly Uri _baseUrl;
        private MmfHttpClientFactory _httpClientFactory;

        public ActivityTypeClient() : this("https://oauth2-api.mapmyapi.com")
        {

        }

        public ActivityTypeClient(string baseUrl):this(baseUrl,new MmfHttpClientFactory())
        {

        }

        public ActivityTypeClient(string baseUrl, MmfHttpClientFactory httpClientFactory)
        {
            _baseUrl = new Uri(baseUrl);
            _httpClientFactory = httpClientFactory;
        }

        public List<ActivityType> Get(ActivityTypeApiRequest request)
        {
            using (var client = _httpClientFactory.Create(_baseUrl))
            {
                client.BaseAddress = _baseUrl;
                client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = string.Format("v7.1/activity_type/");

                var items = GetActivityTypeCollection(client, requestUri);

                return items;
            }
        }

        private List<ActivityType> GetActivityTypeCollection(HttpClient client, string requestUri)
        {
            var response = client.GetAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var activityTypeData = response.Content.ReadAsAsync<dynamic>().Result;

                var items = MapCollection(activityTypeData);

                var nextLink = MapLink(activityTypeData._links.next);

                if (nextLink != null)
                {
                    var nextItems = GetActivityTypeCollection(client, nextLink.Href);
                    items.AddRange(nextItems);
                }

                return items;
            }



            throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
                response.ReasonPhrase));
        }

        private List<ActivityType> MapCollection(dynamic activityTypeData)
        {
            var activityTypes = new List<ActivityType>();

            foreach (var item in activityTypeData._embedded.activity_types)
            {
                var activityType = MapSingle(item);
                activityTypes.Add(activityType);
            }

            return activityTypes;
        }

        private ActivityType MapSingle(dynamic activityTypeData)
        {
            var selfLink = MapLink(activityTypeData._links.self);
            var id = int.Parse(selfLink.Id);

            var iconLink = MapLink(activityTypeData._links.icon_url);
            var rootLink = MapLink(activityTypeData._links.root);
            var parentLink = MapLink(activityTypeData._links.parent);

            var template = MapTemplate(activityTypeData);
           
            var rawData = activityTypeData.ToString();

            var activityType = new ActivityType
            {
                Id = id,
                SelfLink = selfLink,
                Name = activityTypeData.name,
                Mets = activityTypeData.mets,
                ShortName = activityTypeData.short_name,
                HasChildren = activityTypeData.has_children,
                ImportOnly = activityTypeData.import_only,
                LocationAware = activityTypeData.location_aware,
                IconUrl = iconLink.Href,
                RootLink = rootLink,
                ParentLink = parentLink,
                Template = template,
                RawData = rawData
            };

            return activityType;
        }

        private static ActivityTypeTemplate MapTemplate(dynamic activityTypeData)
        {
            if (activityTypeData.template == null)
                return null;

            var template = new ActivityTypeTemplate
            {
                ActivitiesAre = activityTypeData.template.activities_are,
                UserActionPast = activityTypeData.template.user_action_past,
                UserActionPresent = activityTypeData.template.user_action_present
            };
            return template;
        }

        private static List<Link> MapLinkCollection(dynamic linkData)
        {
            if (linkData == null)
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