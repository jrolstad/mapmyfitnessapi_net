using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using mapmyfitnessapi_sdk.models;
using mapmyfitnessapi_sdk.services;

namespace mapmyfitnessapi_sdk.usergears
{
    public class UserGearClient
    {
        private readonly MmfHttpClientFactory _httpClientFactory;
        private readonly Uri _baseUrl;

        public UserGearClient() : this("https://oauth2-api.mapmyapi.com")
        {

        }

        public UserGearClient(string baseUrl)
            : this(baseUrl, new MmfHttpClientFactory())
        {
            
        }

        public UserGearClient(string baseUrl, MmfHttpClientFactory httpClientFactory)
        {
            _baseUrl = new Uri(baseUrl);
            _httpClientFactory = httpClientFactory;
        }

        public List<UserGear> Get(UserGearApiRequest request)
        {
            using (var client = _httpClientFactory.Create(_baseUrl))
            {
                client.BaseAddress = _baseUrl;
                client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = string.Format("/api/0.1/usergear/");

                var items = GetUserGearCollection(client, requestUri);

                return items;
            }
        }

        private List<UserGear> GetUserGearCollection(HttpClient client, string requestUri)
        {
            var response = client.GetAsync(requestUri).Result;
            if (response.IsSuccessStatusCode)
            {
                var userGearData = response.Content.ReadAsAsync<dynamic>().Result;

                var items = MapCollection(userGearData);

                var nextLink = MapLink(userGearData._links.next);

                if (nextLink != null)
                {
                    var nextItems = GetUserGearCollection(client, nextLink.Href);
                    items.AddRange(nextItems);
                }

                return items;
            }



            throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
                response.ReasonPhrase));
        }

        public UserGear GetById(UserGearApiRequest request)
        {
            using (var client = _httpClientFactory.Create(_baseUrl))
            {
                client.BaseAddress = _baseUrl;
                client.DefaultRequestHeaders.Add("Api-Key", request.ApiKey);
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", request.AccessToken));
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var requestUri = string.Format("/api/0.1/usergear/{0}/", request.UserGearId);
                var response = client.GetAsync(requestUri).Result;
                if (response.IsSuccessStatusCode)
                {
                    var userData = response.Content.ReadAsAsync<dynamic>().Result;
                    var user = MapSingle(userData);

                    return user;
                }

                throw new HttpRequestException(string.Format("Http Status:{0}| Reason:{1}", response.StatusCode,
                    response.ReasonPhrase));

            }
        }

        private List<UserGear> MapCollection(dynamic userGearData)
        {
            var workouts = new List<UserGear>();

            foreach (var item in userGearData._embedded.usergear)
            {
                var workout = MapSingle(item);
                workouts.Add(workout);
            }

            return workouts;
        }

        private UserGear MapSingle(dynamic userGearData)
        {
            var selfLink = MapLink(userGearData._links.self);
            var id = selfLink.Id;

            var purchaseDate = MapDateTime(userGearData.purchase_date);

            var gear = MapGear(userGearData.gear);
            var rawData = userGearData.ToString();

            var userGear = new UserGear
            {
                Id = id,
                SelfLink = selfLink,
                Name = userGearData.name,
                InitialDistance = userGearData.initial_distance,
                TargetDistance = userGearData.target_distance,
                PurchaseDate = purchaseDate,
                CurrentDistance = userGearData.current_distance,
                Retired = userGearData.retired,
                Gear = gear,
                RawJson = rawData
            };

            return userGear;
        }

        private Gear MapGear(dynamic gearData)
        {
            if (gearData == null)
                return null;

            var gear = new Gear
            {
                StyleNumber = gearData.style_number,
                Color = gearData.color,
                ProductUrl = gearData.product_url,
                AgeGroup = gearData.age_group,
                Size = gearData.size,
                Sku = gearData.sku,
                Source = gearData.source,
                Department = gearData.department,
                Price = gearData.price,
                Available = gearData.available,
                Category = gearData.category,
                Description = gearData.description,
                Brand = gearData.brand,
                PurchaseUrl = gearData.purchase_url,
                MidLevelProductType = gearData.mid_level_product_type,
                PhotoUrl = gearData.photo_url,
                DetailPhotoUrl = gearData.detail_photo_url,
                ProductType = gearData.product_type,
                Gender = gearData.gender,
                Upc = gearData.upc,
                ThumbnailUrl = gearData.thumbnail_url,
                StyleId = gearData.styleid,
                Model = gearData.model,
                Msrp = gearData.msrp
            };

            return gear;
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

        private static DateTime? MapDateTime(dynamic dateValue)
        {
            if (dateValue == null || string.IsNullOrWhiteSpace(dateValue.ToString()))
                return null;

            var date = DateTime.Parse(dateValue.ToString()).ToUniversalTime();

            return date;
        }
    }
}