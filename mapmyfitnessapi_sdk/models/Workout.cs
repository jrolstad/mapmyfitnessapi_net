using System;

namespace mapmyfitnessapi_sdk.models
{
    public class Workout
    {
        public DateTime StartDateTime { get; set; }

        public string Name { get; set; }

        public DateTime UpdatedDateTime { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string Notes { get; set; }

        public string ReferenceKey { get; set; }

        public string StartLocaleTimezone { get; set; }

        public string Source { get; set; }

        public bool HasTimeSeries { get; set; }

        public bool IsVerified { get; set; }

        public double? ActiveTime { get; set; }

        public double? Distance { get; set; }

        public double? Steps { get; set; }

        public double? AverageSpeed { get; set; }

        public double? MaxSpeed { get; set; }

        public double? MinSpeed { get; set; }

        public double? ElapsedTime { get; set; }

        public double? MetabolicEnergy { get; set; }

        public int Id { get; set; }

        public int? Route { get; set; }

        public int ActivityType { get; set; }

        public int User { get; set; }

        public int Privacy { get; set; }

        public int? UserGear { get; set; }

        public Link UserGearLink { get; set; }

        public Link SelfLink { get; set; }

        public Link RouteLink { get; set; }

        public Link ActivityTypeLink { get; set; }

        public Link UserLink { get; set; }

        public Link PrivacyLink { get; set; }

        public string RawData { get; set; }

    }
}