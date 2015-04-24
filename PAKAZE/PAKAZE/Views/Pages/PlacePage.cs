using PAKAZE.Controls;
using PAKAZE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace PAKAZE.Views
{
    public class PlacePage : ContentPage
    {
        public PlacePage()
        {
            BackgroundColor = Color.White;
            Content = CreatePlacesLayout();
        }

        StackLayout CreatePlacesLayout()
        {

            var placeDataSource = new List<Place>();
            for (var i = 0; i < 10; i++)
            {
                var place = new Place
                {
                    Avatar = "LittleParis.png",
                    Name = "Little Paris",
                    CareerDescription = "Restaurant, bar, takeway",
                    Address = "Chaussée de Bruxelles 89 1410 Waterloo",
                    Distance = string.Format("{0} km", i),
                    Ranking = new Random(DateTime.Now.Millisecond).Next(1, 3),
                    OpeningHours = "Open till 23:00",
                    NumberOfCheckIn = string.Format("{0} check-in there", 25 - i)
                };
                placeDataSource.Add(place);
            }

            var placeListView = new ListView
            {
                HasUnevenRows = true,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                //RowHeight = 60,
                ItemTemplate = new DataTemplate(typeof(PlaceCell))
                //BackgroundColor = Device.OnPlatform(Color.White, Color.White, Color.Gray),
                //RowHeight = Device.OnPlatform(44, 44, 60)
            };
            placeListView.ItemsSource = placeDataSource;

            //search bar
            var sbSearchPlace = new SearchBar
            {
                Placeholder = "Search places",
            };

            //tabbed controls
            var tabbedControl = new TabbledControl
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TintColor = App.TintColor,
            };
            tabbedControl.AddSegment("Nearby", "nearby_icon_active.png");
            tabbedControl.AddSegment("Hot now", "hot_icon_active.png");
            tabbedControl.AddSegment("Popular", "popular_icon_active.png");
            tabbedControl.AddSegment("Events", "events_icon_active.png");

            //view types
            var imgListMode = new Image
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Source = "list_mode_active.png",
            };
            var imgSwipeMode = new Image
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Source = "swipe_mode.png",
            };
            var imgMapMode = new Image
            {
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center,
                Source = "map_mode.png",
            };

            var viewTypes = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.End,
                HorizontalOptions = LayoutOptions.Fill,
                BackgroundColor = Color.Black,
                HeightRequest = 50,
                IsVisible = true,
                Opacity = 0.5,
                Children = { 
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Spacing = 10,
                        Padding = new Thickness(0, 0, 10, 0),
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Children = 
                        {                           
                            imgListMode, imgSwipeMode, imgMapMode 
                        }
                    }
                    
                }
            };

            //root layout
            return new StackLayout
            {
                //Padding = 10,
                Spacing = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = {
                    sbSearchPlace,
                    tabbedControl,
                    placeListView,
                    viewTypes
                }
            };
        }
    }
}
