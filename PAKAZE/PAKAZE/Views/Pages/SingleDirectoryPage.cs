using PAKAZE.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace PAKAZE.Views
{
    public class SingleDirectoryPage : CarouselPage
    {
        public SingleDirectoryPage()
        {
            //common liked pages pseudo-data
            var commonLikePages1 = new ObservableCollection<FacebookPage>();
            for (int i = 0; i < 4; i++)
            {
                commonLikePages1.Add(new FacebookPage { Avatar = "page.png", Name="Le Vif" });
            }

            var commonLikePages2 = new ObservableCollection<FacebookPage>();
            for (int i = 0; i < 3; i++)
            {
                commonLikePages2.Add(new FacebookPage { Avatar = "page.png", Name="Little Paris" });
            }
            //common fb friends pseudo-data
            var commonFBFriends1 = new ObservableCollection<FacebookFriend>();
            for (int i = 0; i < 4; i++)
            {
                commonFBFriends1.Add(new FacebookFriend { Avatar = "friend.png", Age = "26 years old" });
            }

            var commonFBFriends2 = new ObservableCollection<FacebookFriend>();
            for (int i = 0; i < 3; i++)
            {
                commonFBFriends2.Add(new FacebookFriend { Avatar = "friend.png", Age = "28 years old" });
            }
            //common places pseudo-data
            var commonPlaces1 = new ObservableCollection<Place>();
            for (int i = 0; i < 4; i++)
            {
                commonPlaces1.Add(new Place { Avatar = "place.png", Name = "Brussels" });
            }

            var commonPlaces2 = new ObservableCollection<Place>();
            for (int i = 0; i < 3; i++)
            {
                commonPlaces2.Add(new Place { Avatar = "place.png", Name = "HCM" });
            }

            var dataSource = new SingleInfo[]
            {
                new SingleInfo 
                { 
                    Name = "Nancy Jones", 
                    Age = "28 years old", 
                    Distance = "1km", 
                    NumberOfLikes = 125, 
                    Avatar = "NancyJones.jpg", 

                    Height = "168 cm",
                    Weight = "64 kg",
                    HairColor = "Short & black",
                    EyesColor = "Brown",
                    Languages = "French, English",
                    Religion = "Atheist",
                    Job = "Independant",
                    Family = "2 Children",
                    Interests = "Shopping, cooking, traveling music, tennis, reading, concerts, expositions, photography,..",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    CommonLikedFacebookPages = commonLikePages1, 
                    CommonFacebookFriends = commonFBFriends1, 
                    CommonPlaces = commonPlaces1,
                    Photos = new ObservableCollection<string>(new string[]{ "imgsingle1.jpg", "imgsingle2.jpg"})

                },
                new SingleInfo 
                { 
                    Name = "Emily Doe", 
                    Age = "26 years old", 
                    Distance = "2km", 
                    NumberOfLikes = 125, 
                    Avatar = "EmilyDoe.jpg", 

                    Height = "165 cm",
                    Weight = "60 kg",
                    HairColor = "Blonde",
                    EyesColor = "Blue",
                    Languages = "French, English",
                    Religion = "Atheist",
                    Job = "Independant",
                    Family = "1 Children",
                    Interests = "Shopping, cooking, traveling music, tennis, reading, concerts, expositions, photography,..",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.",
                    
                    CommonLikedFacebookPages = commonLikePages2, 
                    CommonFacebookFriends = commonFBFriends2, 
                    CommonPlaces = commonPlaces2,
                    Photos = new ObservableCollection<string>(new string[]{ "imgsingle2.jpg", "imgsingle3.jpg"})

                },
            };

            foreach (var single in dataSource)
            {
                this.Children.Add(new SinglePage(single));
            }

            //this.ItemsSource = new SingleInfo[]
            //{
            //    new SingleInfo { Name = "Nancy Jones", Age = "28 years old", Distance = "1km", NumberOfLikes = 125, Avatar = "NancyJones.jpg", LikedFacebookPages = commonLikePages1, CommonFacebookFriends = commonFBFriends1, CommonPlaces = commonPlaces1},
            //    new SingleInfo { Name = "Emily Doe", Age = "26 years old", Distance = "2km", NumberOfLikes = 125, Avatar = "EmilyDoe.jpg", LikedFacebookPages = commonLikePages2, CommonFacebookFriends = commonFBFriends2, CommonPlaces = commonPlaces2},
            //};

            //this.ItemTemplate = new DataTemplate(() =>
            //{
            //    return new SinglePage();
            //});
        }
    }
}
