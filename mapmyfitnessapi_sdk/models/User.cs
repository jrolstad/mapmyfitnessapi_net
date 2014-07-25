using System;
using System.Collections.Generic;

namespace mapmyfitnessapi_sdk.models
{
    public class User
    {
        public int Id { get; set; }
        public string Href { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LastInitial { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }

        public string Introduction { get; set; }
        public string GoalStatement { get; set; }
        public string ProfileStatement { get; set; }
        public string Hobbies { get; set; }

        public bool TwitterSharingEnabled { get; set; }
        public bool FacebookSharingEnabled { get; set; }

        public string Gender { get; set; }
        public string TimeZone { get; set; }
        public DateTime? Birthdate { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public string DisplayMeasurementSystem { get; set; }

        public DateTime? DateJoined { get; set; }
        public DateTime? LastLogin { get; set; }
       
        public string Country { get; set; }
        public string Region { get; set; }
        public string Locality { get; set; }
        public string Address { get; set; }

        public bool ReceivePromotions { get; set; }
        public bool ReceiveNewsletter { get; set; }
        public bool ReceiveSystemMessages { get; set; }

        public List<Link> Statistics { get; set; }
        public List<Link> PrivacySettings { get; set; }
        public List<Link> Images { get; set; }

        public Link DocumentationLink { get; set; }
        public Link DeactivationLink { get; set; }
        public Link UserAchievementLink { get; set; }
        public Link FriendshipsLink { get; set; }
        public Link WorkoutsLink { get; set; }
        public Link SelfLink { get; set; }
    }
}