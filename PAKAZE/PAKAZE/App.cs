using PAKAZE.Helpers;
using PAKAZE.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace PAKAZE
{
    public class App : Application, ILoginManager
    {
        public static Color TintColor = Color.FromHex("E20D50");
        public static Color ButtonBGColor = Color.FromHex("F0F4F7");
        public static Color InfoTextColor = Color.FromHex("5A5A5A");
        public App()
        {
            // The root page of your application
            var isLoggedIn = Properties.ContainsKey("IsLoggedIn") ? (bool)Properties["IsLoggedIn"] : false;

            // we remember if they're logged in, and only display the login page if they're not
            //isLoggedIn = false; //temp

            if (isLoggedIn)
                MainPage = new MainPage();
            else
                MainPage = new NavigationPage(new LandingPage(this));
        }

        public static Page GetMainPage()
        {
            return Application.Current.MainPage;
        }
        public void ShowMainPage()
        {
            MainPage = new MainPage();
        }

        public void Logout()
        {
            Properties["IsLoggedIn"] = false; // only gets set to 'true' on the LoginPage
            MainPage = new NavigationPage(new LandingPage(this));
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
