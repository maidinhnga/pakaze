using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PAKAZE.Models
{
    public class FacebookPage
    {
        public string URL { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }

    public class FacebookFriend
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string URL { get; set; }
    }

    public class SingleInfo
    {
        public string Avatar { get; set; }
        public string Name { get; set; }
        public string Age { get; set; }
        public string Distance { get; set; }
        public int NumberOfLikes { get; set; }

        /// <summary>
        /// liked facebook pages in common with user
        /// </summary>
        public ObservableCollection<FacebookPage> CommonLikedFacebookPages { get; set; }
        
        /// <summary>
        /// facebook friends in common with user
        /// </summary>
        public ObservableCollection<FacebookFriend> CommonFacebookFriends { get; set; }

        /// <summary>
        /// PAKAZE places in common with user (where both have already checked-in in the past)
        /// </summary>
        public ObservableCollection<Place> CommonPlaces { get; set; }

        public ObservableCollection<string> Photos { get; set; }

        //additional info
        public string Height { get; set; }

        public string Weight { get; set; }

        public string HairColor { get; set; }

        public string EyesColor { get; set; }

        public string Languages { get; set; }

        public string Religion { get; set; }

        public string Job { get; set; }

        public string Family { get; set; }

        public string Interests { get; set; }

        public string Description { get; set; }
    }
}
