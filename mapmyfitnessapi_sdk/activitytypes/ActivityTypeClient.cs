using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using mapmyfitnessapi_sdk.extensions;
using mapmyfitnessapi_sdk.models;
using mapmyfitnessapi_sdk.services;
using mapmyfitnessapi_sdk.users;
using mapmyfitnessapi_sdk.utilities;

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
                AssignParents(items);
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

                var nextLink = LinkMapper.MapLink(activityTypeData._links.next);

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
            var selfLink = LinkMapper.MapLink(activityTypeData._links.self);
            var id = int.Parse(selfLink.Id);

            var iconLink = LinkMapper.MapLink(activityTypeData._links.icon_url);
            var rootLink = LinkMapper.MapLink(activityTypeData._links.root);
            var parentLink = LinkMapper.MapLink(activityTypeData._links.parent);

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


        private void AssignParents(List<ActivityType> activityTypes)
        {
            var activityTypesById = activityTypes.ToDictionaryExplicit(at => at.Id);

            foreach (var type in activityTypes)
            {
                if (type.ParentLink != null)
                {
                    var parent = activityTypesById.Value(int.Parse(type.ParentLink.Id));
                    type.Parent = parent;
                }
                if(type.RootLink!=null)
                {
                    var root = activityTypesById.Value(int.Parse(type.RootLink.Id));
                    type.Root = root;
                }
            }
        }
    }
}