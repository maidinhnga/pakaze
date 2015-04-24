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
    public class BenefitPopup : ContentPage
    {
        public event EventHandler PopupClosed;
        StackLayout _popup;
        public BenefitPopup()
        {
            BackgroundColor = Color.Transparent;
            var contentLayout = new RelativeLayout();

            //title
            var lblTitle = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 28,
                Text = "Your benefit",
                TextColor = Color.FromHex("787878")
            };
            var lblMessage = new Label
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 20,
                Text = "1+1 free beer",
                TextColor = Color.FromHex("C3C3C3")
            };

            //pin code entry
            var txtPinCode = new ExtendedEntry
            {
                Keyboard = Keyboard.Numeric,
                MaxLength = 4,
                IsPassword = true,
            };
            
            var btnUseBenefits = new ImageButton
            {
                Image = "benefit_icon.png",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                Text = "Use my benefit",
                BackgroundColor = Color.FromHex("03A8C4"),
                TextColor = Color.White,
                ///FontSize = 12,
                //Scale = 0.85,
                //HeightRequest = 30,
                //WidthRequest = 225,
                Orientation = ImageOrientation.ImageToLeft,
                //ImageHeightRequest = 20,
                //ImageWidthRequest = 50,
                BorderRadius = 0,
            };

            var btnClose = new Button
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,                
                //VerticalOptions = LayoutOptions.EndAndExpand,
                Text = "Close this window",
                TextColor = Color.White,
                BackgroundColor = App.TintColor,
                BorderRadius = 0,
            };

            //check out
            btnUseBenefits.Clicked += (obj, evt) =>
            {
                if(string.IsNullOrWhiteSpace(txtPinCode.Text))
                {
                    DisplayAlert("PAKAZE", "Please enter your secret code", "OK");
                    return;
                }
                Navigation.PopModalAsync();
                PopupClosed(this, evt);
            };

            //close the popup
            btnClose.Clicked += (obj, evt) =>
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
                    txtPinCode,
                    btnUseBenefits, 
                    btnClose                   
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
    }
}
