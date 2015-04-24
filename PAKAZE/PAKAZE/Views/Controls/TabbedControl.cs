using PAKAZE.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace PAKAZE.Controls
{
    public class TabbledControl : ContentView
    {
        private StackLayout layout;

        public Font Font { get; set; }

        private Color tintColor = App.TintColor;
        public Color TintColor
        {
            get { return tintColor; }
            set
            {
                tintColor = value;
                if (layout == null)
                {
                    return;
                }
                //layout.BackgroundColor = tintColor;
                for (var iBtn = 0; iBtn < layout.Children.Count; iBtn++)
                {
                    SetSelectedState(iBtn, iBtn == selectedSegment, true);
                }
            }
        }

        private int selectedSegment;
        public int SelectedSegment
        {
            get
            {
                return selectedSegment;
            }
            set
            {
                //reset the original selected segment
                if (value == selectedSegment)
                {
                    return;
                }
                SetSelectedState(selectedSegment, false);
                selectedSegment = value;
                if (value < 0 || value >= layout.Children.Count)
                {
                    return;
                }
                SetSelectedState(selectedSegment, true);

                SetSelectedSegment(value);
            }
        }

        public event EventHandler<int> SelectedSegmentChanged;
        private Command ClickedCommand;
        public TabbledControl()
        {
            layout = new ExtendedStackLayout
            {                
                BackgroundColor = Color.Silver,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Padding = 0.5,
                Spacing = 0.5
            };
            //HorizontalOptions = LayoutOptions.Fill;
            //VerticalOptions = LayoutOptions.Start;
            //Padding = new Thickness(0, 0);
            Content = layout;
            selectedSegment = 0;
            ClickedCommand = new Command(SetSelectedSegment);
        }

        public void AddSegment(string segmentText, string icon)
        {
            // TODO: TextColor needs to be a bound property
            var button = new ImageButton
            {
                Source = icon,
                Orientation = XLabs.Enums.ImageOrientation.ImageOnTop,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                BorderRadius = 0,
                BorderWidth = 1,
                BorderColor = Color.Black,
                FontSize = 12,
                Text = segmentText,
                TextColor = Color.Gray,
                BackgroundColor = Color.FromHex("EDEDED"),
                Command = ClickedCommand,
                CommandParameter = layout.Children.Count,  
                HeightRequest = 75,
                //WidthRequest = 80,
            };

            var buttonStack = new StackLayout { Spacing = 0, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.FillAndExpand };
            buttonStack.Children.Add(button);
            if (layout.Children.Count > 0)
            {
                buttonStack.Padding = Device.OnPlatform(new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 5), new Thickness(-25, 0, 0, 0));
            }
            layout.Children.Add(buttonStack);
            //layout.Children.Add(button);

            SetSelectedState(layout.Children.Count - 1, layout.Children.Count - 1 == selectedSegment, true);
        }

        public void AddSegment(string segmentText, string icon, double width, double height)
        {
            // TODO: TextColor needs to be a bound property
            var button = new ImageButton
            {
                Source = icon,
                Orientation = XLabs.Enums.ImageOrientation.ImageOnTop,
                HorizontalOptions = LayoutOptions.Center,  
                VerticalOptions = LayoutOptions.StartAndExpand,
                BorderRadius = 0,
                BorderWidth = 1,
                BorderColor = Color.Black,
                FontSize = 12,
                Text = segmentText,
                TextColor = Color.Gray,
                BackgroundColor = Color.FromHex("EDEDED"),                
                Command = ClickedCommand,
                CommandParameter = layout.Children.Count,
                WidthRequest = width,
                HeightRequest = height,
            };
            var buttonStack = new StackLayout { Spacing = 0, Orientation = StackOrientation.Horizontal, HorizontalOptions = LayoutOptions.Fill };
            buttonStack.Children.Add(button);
            if (layout.Children.Count > 0)
            {
                buttonStack.Padding = Device.OnPlatform(new Thickness(0, 0, 0, 0), new Thickness(0, 0, 0, 5), new Thickness(-25, 0, 0, 0));
            }
            layout.Children.Add(buttonStack);
            //layout.Children.Add(button);

            SetSelectedState(layout.Children.Count - 1, layout.Children.Count - 1 == selectedSegment, true);
        }

        private void SetSelectedSegment(object o)
        {
            var selectedIndex = (int)o;
            SelectedSegment = selectedIndex;
            if (SelectedSegmentChanged != null)
            {
                SelectedSegmentChanged(this, selectedIndex);
            }
        }

        public void SetSegmentText(int iSegment, string segmentText)
        {
            if (iSegment >= layout.Children.Count || iSegment < 0)
            {
                throw new IndexOutOfRangeException("SetSegmentText: Attempted to change segment text for a segment doesn't exist.");
            }
            ((Button)layout.Children[iSegment]).Text = segmentText;
        }
        private void SetSelectedState(int indexer, bool isSelected, bool setBorderColor = false)
        {
            if (layout.Children.Count <= indexer)
            {
                return; //Out of bounds
            }
            var button = (Button)(((StackLayout)layout.Children[indexer]).Children[0]);

            //var button = (Button)layout.Children[indexer];
            // TODO: TextColor needs to be a bound property
            if (isSelected)
            {
                button.BackgroundColor = Color.Transparent;
                button.TextColor = App.TintColor;
                button.BorderColor = Color.Black;
            }
            else
            {
                button.BackgroundColor = Color.FromHex("EDEDED");
                button.TextColor = Color.Gray;
                button.BorderColor = Color.Black;
            }
        }
    }

}
