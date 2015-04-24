using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using XLabs.Ioc;
using XLabs.Platform.Device;

namespace PAKAZE.Droid
{
    [Activity(Label = "PAKAZE", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            RequestedOrientation = ScreenOrientation.Portrait;

            var resolverContainer = new SimpleContainer();
            resolverContainer.Register<IDevice>(t => XLabs.Platform.Device.AndroidDevice.CurrentDevice);
            Resolver.SetResolver(resolverContainer.GetResolver());

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

    }
}

