using System.Collections.Generic;

namespace mapmyfitnessapi_sdk.models
{
    public class ActivityType
    {
        public int Id { get; set; }

        public int Mets { get; set; }

        public List<MetsSpeed> MetsSpeed { get; set; } 

        public string Name { get; set; }

        public string ShortName { get; set; }

        public bool? HasChildren { get; set; }

        public bool? ImportOnly { get; set; }

        public bool? LocationAware { get; set; }

        public string IconUrl { get; set; }

        public Link RootLink { get; set; }

        public Link ParentLink { get; set; }

        public Link SelfLink { get; set; }

        public ActivityTypeTemplate Template { get; set; }

        public string RawData { get; set; }

        public ActivityType Parent { get; set; }
        public ActivityType Root { get; set; }
        public List<ActivityType> Children { get; set; }
    }

    public class MetsSpeed
    {
        public string Mets { get; set; }

        public double Speed { get; set; }
    }

    public class ActivityTypeTemplate
    {
        public string ActivitiesAre { get; set; }
        public string UserActionPast { get; set; }
        public string UserActionPresent { get; set; }
    }
}