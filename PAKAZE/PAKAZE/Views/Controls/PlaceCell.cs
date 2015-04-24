using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace PAKAZE.Controls
{
    public class PlaceCell : ViewCell
    {
        public PlaceCell()
        {
            var grid = new Grid
            {
                Padding = new Thickness(10, 5, 10, 5),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };

            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = 65 }); //column for place photo
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });

            //place photo
            var imgPlacePhoto = new Image
            {
                WidthRequest = 50,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.AspectFill
            };
            imgPlacePhoto.SetBinding(Image.SourceProperty, "Avatar");
            grid.Children.Add(imgPlacePhoto, 0, 0);

            //var imgPromotion = new Image
            //{
            //    HorizontalOptions = LayoutOptions.Center,
            //    VerticalOptions = LayoutOptions.EndAndExpand,
            //    Source = "promotion_icon.png"
            //};
            //grid.Children.Add(imgPlacePhoto, 0, 0);

            //place Info
            var ranking = new PlaceRanking();
            ranking.SetBinding(PlaceRanking.RankingProperty, "Ranking");

            var lblPlaceName = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 14,
            };
            lblPlaceName.SetBinding(Label.TextProperty, "Name");
            var lblPlaceCareerDescription = new Label
            {
                TextColor = Color.Gray,
                FontSize = 12,
            };
            lblPlaceCareerDescription.SetBinding(Label.TextProperty, "CareerDescription");

            var lblPlaceAddress = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
                LineBreakMode = LineBreakMode.WordWrap
            };
            lblPlaceAddress.SetBinding(Label.TextProperty, "Address");

            var placeInfoLayout = new StackLayout
            {
                Spacing = 1,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { 
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.StartAndExpand,
                        Spacing = 5,
                        Children = {ranking, lblPlaceName}
                    },
                    lblPlaceCareerDescription, 
                    lblPlaceAddress 
                }
            };
            grid.Children.Add(placeInfoLayout, 1, 0);

            //place additional info
            var lblDistance = new Label
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                FontSize = 12,
            };
            lblDistance.SetBinding(Label.TextProperty, "Distance");

            var lblOpeningHours = new Label
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                FontSize = 12,
            };
            lblOpeningHours.SetBinding(Label.TextProperty, "OpeningHours");

            var lblNumberOfCheckIn = new Label
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                FontSize = 12,
                TextColor = App.TintColor,
            };
            lblNumberOfCheckIn.SetBinding(Label.TextProperty, "NumberOfCheckIn");

            var placeAdditionalInfoLayout = new StackLayout
            {
                Padding = new Thickness(0, 5, 0, 0),
                Spacing = 1,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = { 
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.End,
                        Children = {
                            lblDistance,
                            new Image {Source = "distance_icon.png"}
                        }
                    }, 
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.End,
                        Children = {
                            lblOpeningHours,
                            new Image {Source = "opening_icon.png"}
                        }
                    }, 
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.End,
                        Children = {
                            lblNumberOfCheckIn,
                            new Image {Source = "popular_icon_active.png"}
                        }
                    }
                     
                }
            };
            grid.Children.Add(placeAdditionalInfoLayout, 2, 0);

            View = grid;
        }
    }
}
