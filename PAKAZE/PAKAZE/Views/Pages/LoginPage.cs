using PAKAZE.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace PAKAZE.Views
{
    public class LoginPage : ContentPage
    {
        Entry txtLogin;
        Entry txtPassword;
        ILoginManager ilm;

        public LoginPage(ILoginManager _ilm)
        {
            NavigationPage.SetHasBackButton(this, false);
            BackgroundColor = Color.White;
            ilm = _ilm;

            Content = new ScrollView
            {
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Vertical,
                Content = CreateLoginLayout()
            };
        }

        StackLayout CreateLoginLayout()
        {
            var layout = new StackLayout
            {
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
            layout.Children.Add(new StackLayout { HeightRequest = 20 });

            //login, password inputs
            txtLogin = new Entry
            {
                Text = "s.colin@promisys.be",
                Placeholder = "E-mail",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Keyboard = Keyboard.Email
            };
            layout.Children.Add(txtLogin);

            txtPassword = new Entry
            {
                Text = "123456",
                Placeholder = "Password",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                IsPassword = true,
            };
            layout.Children.Add(txtPassword);

            //forgot password
            var btnForgotPassword = new ExtendedLabel
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "Forgot password",
                TextColor = Color.Gray,
                BackgroundColor = Color.Transparent,
                IsUnderline = true
            };
            layout.Children.Add(btnForgotPassword);

            //login button
            var btnLogin = new ExtendedButton
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HorizontalContentAlignment = TextAlignment.Center,
                Text = "Connexion",
                TextColor = App.TintColor,
                BackgroundColor = App.ButtonBGColor,
                BorderRadius = 5,
            };
            btnLogin.Clicked += btnLogin_Clicked;
            layout.Children.Add(btnLogin);

            return layout;
        }

        /// <summary>
        /// login
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnLogin_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLogin.Text)
                && !string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                if (txtLogin.Text == "s.colin@promisys.be"
                    && txtPassword.Text == "123456")
                {
                    //redirect user to home page
                    App.Current.Properties["IsLoggedIn"] = true;
                    //Navigation.PopToRootAsync(true);
                    ilm.ShowMainPage();

                }
                else
                {
                    DisplayAlert("PAKAZE", "Invalid login or password. Please retry or contact your administrator.", "OK");
                }
            }
        }
    }
}
