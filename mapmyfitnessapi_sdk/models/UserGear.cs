using System;

namespace mapmyfitnessapi_sdk.models
{
    public class UserGear
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double? InitialDistance { get; set; }

        public double? TargetDistance { get; set; }

        public DateTime? PurchaseDate { get; set; }

        public double? CurrentDistance { get; set; }

        public bool Retired { get; set; }

        public Link SelfLink { get; set; }

        public Link UserLink { get; set; }

        public Gear Gear { get; set; }

        public string RawJson { get; set; }
    }
}