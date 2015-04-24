using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;
using XLabs.Enums;
using XLabs.Forms.Controls;

namespace PAKAZE.Views
{
    public class HomePage : ContentPage
    {
        const int bottomSectionDimension = 85;
        public HomePage()
        {
            //Title = "Home";            
            BackgroundColor = Color.FromHex("EDEDED");
            Content = CreateHomePageLayout();
            //Content = new ScrollView
            //{
            //    IsClippedToBounds = true,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    Orientation = ScrollOrientation.Vertical,
            //    Content = CreateHomePageLayout()
            //};
        }


        /// <summary>
        /// create root layout
        /// </summary>
        /// <returns></returns>
        StackLayout CreateHomePageLayout()
        {
            //check-in 
            var checkInLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 20, 0, 0),
                Spacing = 10,
            };

            var imgCheckIn = new Image
            {
                Aspect = Xamarin.Forms.Aspect.AspectFit,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Source = "check_in.png",
                WidthRequest = 100,

            };
            checkInLayout.Children.Add(imgCheckIn);

            //infos
            var checkInInfoLayout = new StackLayout
            {
                Spacing = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(0, 10, 10, 0)
            };
            checkInLayout.Children.Add(checkInInfoLayout);
            //name
            var lblPlaceName = new Label
            {
                Text = "Promisys",
                FontAttributes = FontAttributes.Bold,
                FontSize = 18
            };
            var lblPlaceAddress = new Label
            {
                Text = "Chaussée de Tubize 483A",
                //FontAttributes = FontAttributes.Bold,
                FontSize = 14
            };
            var placeInfoLayout = new StackLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Orientation = StackOrientation.Vertical,
                Spacing = 1,
                Children = { lblPlaceName, lblPlaceAddress }
            };
            checkInInfoLayout.Children.Add(placeInfoLayout);
            //change place
            var btnChangePlace = new Picker
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Title = "Change location...",
                //Text = "Change location...",
                //BackgroundColor = Color.Transparent,
            };
            var changePlaceGesture = new TapGestureRecognizer { };
            changePlaceGesture.Tapped += (obj, evt) =>
            {
                Navigation.PushModalAsync(new SearchPlacePage());
            };
            btnChangePlace.GestureRecognizers.Add(changePlaceGesture);

            checkInInfoLayout.Children.Add(btnChangePlace);

            //last check-in
            var lblLastCheckInTitle = new Label
            {
                Text = "Your last Check-in",
                FontAttributes = FontAttributes.Bold,
                FontSize = 18
            };
            checkInInfoLayout.Children.Add(lblLastCheckInTitle);
            //name
            var lblLastCheckInName = new ExtendedLabel
            {
                Text = "Little Paris",
                IsUnderline = true,
                FontSize = 14
            };
            //icons
            var imgUsers = new Image
            {
                Source = "users_icon.png"
            };
            var imgLikes = new Image
            {
                Source = "likes_icon.png"
            };
            var imgCheckout = new Image
            {
                Source = "check_out_icon.png"
            };
            var checkoutGesture = new TapGestureRecognizer();
            checkoutGesture.Tapped += (obj, evt) =>
            {
                imgCheckout.IsEnabled = false;
                var checkoutPopup = new CheckoutPopup();
                checkoutPopup.PopupClosed += (sender, e) =>
                {
                    imgCheckout.IsEnabled = true;
                };
                Navigation.PushModalAsync(checkoutPopup);
            };
            imgCheckout.GestureRecognizers.Add(checkoutGesture);

            var lastCheckInPlaceDetails = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Spacing = 10,
                Children = { lblLastCheckInName, imgUsers, imgLikes, imgCheckout }
            };
            checkInInfoLayout.Children.Add(lastCheckInPlaceDetails);
            //benefits
            var btnBenefits = new ImageButton
            {
                Image = "benefit_icon.png",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = "Your benefit",
                BackgroundColor = Color.FromHex("03A8C4"),
                TextColor = Color.White,
                FontSize = 14,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                //Scale = 0.85,
                //HeightRequest = 30,
                //WidthRequest = 225,
                Orientation = ImageOrientation.ImageToLeft,
                ImageHeightRequest = 25,
                //ImageWidthRequest = 50,
                BorderRadius = 0,
            };
            btnBenefits.Clicked += (obj, evt) =>
            {
                btnBenefits.IsEnabled = false;
                var benefitPopup = new BenefitPopup();
                benefitPopup.PopupClosed += (sender, e) =>
                {
                    btnBenefits.IsEnabled = true;
                };
                Navigation.PushModalAsync(benefitPopup);
            };
            checkInInfoLayout.Children.Add(btnBenefits);

            //bottom sections: places, singles, my pakaze
            var imgPlacesSection = new Image
            {
                Source = "places_icon.png",
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                //WidthRequest = bottomSectionDimension,
                //HeightRequest = bottomSectionDimension
            };
            var placeTappedGesture = new TapGestureRecognizer { };
            placeTappedGesture.Tapped += (obj, evt) =>
            {
                if (App.GetMainPage() is MainPage)
                {
                    ((MainPage)App.GetMainPage()).NavigateTo(new MenuItem() { TargetType = typeof(PlacePage) });
                }
            };
            imgPlacesSection.GestureRecognizers.Add(placeTappedGesture);

            var imgSinglesSection = new Image
            {
                Source = "singles_icon.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                //WidthRequest = bottomSectionDimension,
                //HeightRequest = bottomSectionDimension
            };
            var singleTappedGesture = new TapGestureRecognizer { };
            singleTappedGesture.Tapped += (obj, evt) =>
            {
                if (App.GetMainPage() is MainPage)
                {
                    ((MainPage)App.GetMainPage()).NavigateTo(new MenuItem() { TargetType = typeof(SingleDirectoryPage) });
                }
            };
            imgSinglesSection.GestureRecognizers.Add(singleTappedGesture);

            var imgMyPakazeSection = new Image
            {
                Source = "my_pakaze_icon.png",
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.EndAndExpand,
                //WidthRequest = bottomSectionDimension,
                //HeightRequest = bottomSectionDimension
            };

            var bottomLayout = new StackLayout
            {
                //BackgroundColor = Color.Yellow,
                Padding = new Thickness(10, 10, 10, 20),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Horizontal,
                Children = { imgPlacesSection, imgSinglesSection, imgMyPakazeSection }
            };
            //for the background image
            var gridBottom = new Grid
            {
                //BackgroundColor = Color.Red,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            var imgBackground = new Image
            {
                //BackgroundColor = Color.Yellow,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Aspect = Xamarin.Forms.Aspect.Fill,
                VerticalOptions = LayoutOptions.EndAndExpand,

                Source = "home_bg.png",
            };

            //add the background image first then the bottom section
            gridBottom.Children.Add(imgBackground, 0, 0);
            gridBottom.Children.Add(bottomLayout, 0, 0);

            //root layout
            return new StackLayout
            {
                Spacing = 10,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                Children = {
                    checkInLayout,
                    gridBottom
                }
            };
        }
    }
}
