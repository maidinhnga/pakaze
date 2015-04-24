using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace PAKAZE.Views
{
    public class CheckoutPopup : ContentPage
    {
        public event EventHandler PopupClosed;

        StackLayout _popup;
        public CheckoutPopup()
        {
            BackgroundColor = Color.Transparent;
            var contentLayout = new RelativeLayout();

            //title
            var lblTitle = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 28,
                Text = "Check out",
                TextColor = Color.FromHex("787878")
            };
            var lblMessage = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                Text = "Are you sure you want to check out?",
                TextColor = Color.FromHex("C3C3C3")
            };

            var btnYes = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Yes",
                TextColor = Color.White,
                BackgroundColor = App.TintColor,
                BorderRadius = 0,
            };

            var btnNo = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "No",
                TextColor = Color.White,
                BackgroundColor = App.TintColor,
                BorderRadius = 0,
            };

            //check out
            btnYes.Clicked += (obj, evt) =>
            {
                Checkout();
            };

            //close the popup
            btnNo.Clicked += (obj, evt) =>
            {
                Navigation.PopModalAsync();
                PopupClosed(this, evt);
            };
            
            _popup = new StackLayout
            {
                Padding = 20,
                Spacing = 20,
                BackgroundColor = Color.White,
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                WidthRequest = this.Width * 0.85,
                HeightRequest = this.Height * 0.75,
                Children = 
                {
                    lblTitle,
                    lblMessage,
                    new StackLayout {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Orientation = StackOrientation.Horizontal,
                        Children = {btnYes, btnNo}
                    }
                }
            };

            this.SizeChanged += (sender, e) =>
            {
                _popup.WidthRequest = this.Width * 0.85;
                _popup.HeightRequest = this.Height * 0.75;
            };

            var overlay = new BoxView
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Opacity = 0.5,
                Color = Color.Black,
                InputTransparent = false,
            };

            var gesture = new TapGestureRecognizer();
            gesture.Tapped += (sender, args) =>
            {
                //this.DismissPopup();
            };
            overlay.GestureRecognizers.Add(gesture);
            contentLayout.Children.Add(
                overlay,
                Constraint.RelativeToParent(p => 0),
                Constraint.RelativeToParent(p => 0),
                Constraint.RelativeToParent(p => this.Width),
                Constraint.RelativeToParent(p => this.Height)
                );

            contentLayout.Children.Add(
                _popup,
                 Constraint.RelativeToParent(p => (this.Width / 2) - (_popup.WidthRequest / 2)),
                Constraint.RelativeToParent(p => (this.Height / 2) - (_popup.HeightRequest / 2)),
                Constraint.RelativeToParent(p => _popup.WidthRequest),
                Constraint.RelativeToParent(p => _popup.HeightRequest)
                );


            Content = contentLayout;
        }

        /// <summary>
        /// check out
        /// </summary>
        private void Checkout()
        {
            _popup.Children.Clear();

            //title
            var lblTitle = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 28,
                Text = "Check out",
                TextColor = Color.FromHex("787878")
            };
            _popup.Children.Add(lblTitle);

            var imgCheckout = new Image
            {
                Source = "checkout.png",
                HorizontalOptions = LayoutOptions.EndAndExpand
            };
            _popup.Children.Add(imgCheckout);

            var lblMessage = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                Text = "You have been checked out!",
                TextColor = Color.FromHex("C3C3C3")
            };
            _popup.Children.Add(lblMessage);

            var btnClose = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Text = "Close this window",
                TextColor = Color.White,
                BackgroundColor = App.TintColor,
                BorderRadius = 0,
            };
            //close the popup
            btnClose.Clicked += (obj, evt) =>
            {
                Navigation.PopModalAsync();
                PopupClosed(this, evt);
            };
            _popup.Children.Add(btnClose);
        }
    }
}
