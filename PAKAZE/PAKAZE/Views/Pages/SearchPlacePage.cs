using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace PAKAZE.Views
{
    public class SearchPlacePage : ContentPage
    {
        public SearchPlacePage()
        {
            Content = new StackLayout
            {
                Children = {
					new Label { Text = "Search place" }
				}
            };
        }
    }
}
