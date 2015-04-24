using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PAKAZE.Views;
using PAKAZE.Droid.Controls;

[assembly: ExportRenderer(typeof(ExtendedStackLayout), typeof(ExtendedStackLayoutRenderer))]
namespace PAKAZE.Droid.Controls
{
    public class ExtendedStackLayoutRenderer : VisualElementRenderer<StackLayout>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null) // perform initial setup
            {
            }
        }

        protected override void OnAttachedToWindow()
        {
            base.OnAttachedToWindow();

            SetBackgroundDrawable(Resources.GetDrawable(Resource.Drawable.GradientBackground));
        }
    }
}