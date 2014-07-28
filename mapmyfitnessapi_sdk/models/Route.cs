using System;

namespace mapmyfitnessapi_sdk.models
{
    public class Route
    {
        public int Id { get; set; }
        public string Href { get; set; }
        public string Name { get; set; }
        
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string DataSource { get; set; }
        public string Description { get; set; }

        public DateTime UpdateDateTime { get; set; }
        public DateTime CreateDateTime { get; set; }

        public string StartPointType { get; set; }
        public string StartingLocationType { get; set; }
        public Point StartingLocationCoordinates{ get; set; }

        public double Distance { get; set; }
        public double? MaximumElevation { get; set; }
        public double? MinimumElevation { get; set; }
        public double? TotalAscent { get; set; }
        public double? TotalDescent { get; set; }

        public int User { get; set; }
        public Link UserLink { get; set; }

        public int Privacy { get; set; }
        public Link PrivacyLink { get; set; }

        public Link SelfLink { get; set; }

        public int ActivityType { get; set; }
        public Link ActivityTypeLink { get; set; }

        public string ThumbnailImageUrl { get; set; }
        public Link ThumbnailLink { get; set; }

        public Link AlternateLink { get; set; }
       
    }

    public class Point
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Distance { get; set; }

        public double Elevation { get; set; }
    }
}