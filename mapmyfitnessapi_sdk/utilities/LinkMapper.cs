using System.Collections.Generic;
using System.Linq;
using mapmyfitnessapi_sdk.models;

namespace mapmyfitnessapi_sdk.utilities
{
    public static class LinkMapper
    {
        public static List<Link> MapLinkCollection(dynamic linkData)
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

        public static Link MapLink(dynamic linkData)
        {
            List<Link> links = MapLinkCollection(linkData);
            var link = links.FirstOrDefault();

            return link;
        }
    }
}