using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace PAKAZE.Views
{
    public class MenuItem
    {
        public string Title { get; set; }

        public string IconSource { get; set; }

        public Type TargetType { get; set; }
    }

    public class MenuListView : ListView
    {
        public MenuListView()
        {
            List<MenuItem> data = new MenuListData();

            ItemsSource = data;
            VerticalOptions = LayoutOptions.FillAndExpand;
            BackgroundColor = Color.Transparent;

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "Title");
            cell.SetBinding(ImageCell.ImageSourceProperty, "IconSource");
            ItemTemplate = cell;
            RowHeight = 44;
        }
    }
    public class MenuListData : List<MenuItem>
    {
        public MenuListData()
        {
            this.Add(new MenuItem()
            {
                Title = "Home",
                IconSource = "home.png",
                 
                TargetType = typeof(HomePage)
            });

            this.Add(new MenuItem()
            {
                Title = "Singles",
                IconSource = "singles.png",
                TargetType = typeof(SingleDirectoryPage)
            });

            this.Add(new MenuItem()
            {
                Title = "Places",
                IconSource = "places.png",
                TargetType = typeof(PlacePage)
            });
        }
    }

    public class MenuPage : ContentPage
    {
        public ListView Menu { get; set; }

        public MenuPage()
        {
            Icon = "menu_icon.png";
            Title = "menu"; // The Title property must be set.

            Menu = new MenuListView();

            var layout = new StackLayout
            {
                Spacing = 0,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            
            layout.Children.Add(Menu);

            Content = layout;
        }
    }
}
