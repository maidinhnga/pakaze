using PAKAZE.Helpers;
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
    public class LandingPage : ContentPage
    {
        ILoginManager ilm;

        public LandingPage(ILoginManager _ilm)
        {
            NavigationPage.SetHasBackButton(this, false);
            BackgroundColor = Color.White;
            ilm = _ilm;

            Content = CreateLandingLayout();
            //Content = new ScrollView
            //{
            //    IsClippedToBounds = true,
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand,
            //    Orientation = ScrollOrientation.Vertical,
            //    Content = CreateLandingLayout()
            //};
        }

        StackLayout CreateLandingLayout()
        {
            var layout = new StackLayout
            {
                Spacing = 5,
                Padding = new Thickness(20, 0, 20, 0),
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            //landing photo
            var imgLanding = new Image
            {
                Source = "pakaze.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
            };
            layout.Children.Add(imgLanding);

            //account buttons            
            var btnCreateAccount = new ExtendedButton
            {
                BackgroundColor = App.ButtonBGColor,
                HorizontalContentAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "Create account",
                TextColor = App.TintColor,
                BorderRadius = 5,
                //WidthRequest = 100,
            };
            var btnSignin = new ExtendedButton
            {
                WidthRequest = 150,
                BackgroundColor = App.ButtonBGColor,
                HorizontalContentAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "Sign in",
                TextColor = App.TintColor,
                BorderRadius = 5,
            };
            btnSignin.Clicked += btnSignin_Clicked;

            var loginStack = new StackLayout
            {
                Padding = new Thickness(0, 20, 0, 0),
                Spacing = 10,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Children = { btnCreateAccount, btnSignin }
            };
            layout.Children.Add(loginStack);

            //facebook login
            var btnFBLogin = new ImageButton
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = "Facebook connect",
                BackgroundColor = Color.FromHex("3d509f"),
                TextColor = Color.FromHex("ffffff"),
                HeightRequest = 50,
                Orientation = ImageOrientation.ImageToLeft,
                ImageHeightRequest = 25,
                ImageWidthRequest = 25,
                BorderRadius = 5,
                Image = "icon_facebook.png"
            };
            layout.Children.Add(btnFBLogin);

            var lblAgreement = new Label
            {
                VerticalOptions = LayoutOptions.EndAndExpand,
                FontSize = 12,
                Text = "En utilisant Pakaze, tu acceptes nos conditions générales et notre politique de confidentialité.",
                TextColor = Color.FromHex("808080")
            };
            //layout.Children.Add(lblAgreement);

            return layout;
        }

        #region button events
        /// <summary>
        /// sign-in
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void btnSignin_Clicked(object sender, EventArgs e)
        {
            var loginPage = new LoginPage(ilm);
            await this.Navigation.PushAsync(loginPage, true);
        }
        #endregion
    }
}
