using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;

namespace PAKAZE.Views
{
    public class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            var menuPage = new MenuPage();

            menuPage.Menu.ItemSelected += (sender, e) => NavigateTo(e.SelectedItem as MenuItem);

            MasterBehavior = Xamarin.Forms.MasterBehavior.Popover;
            Master = menuPage;
            Detail = new NavigationPage(new HomePage());

            CreateToolbarItems();
        }

        void CreateToolbarItems()
        {
            //toolbar items
            var itmMyPakaze = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Icon = "my_pakaze_top_icon.png",
            };
            ToolbarItems.Add(itmMyPakaze);

            var itmNotification = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Icon = "notifications_icon.png",
            };
            ToolbarItems.Add(itmNotification);

            var itmChat = new ToolbarItem
            {
                Order = ToolbarItemOrder.Primary,
                Icon = "chat_icon.png",
            };
            ToolbarItems.Add(itmChat);

            var itmSettings = new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Icon = "settings.png",
                Text = "Settings"
            };
            ToolbarItems.Add(itmSettings);

            var itmHelp = new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Icon = "help.png",
                Text = "Help"
            };
            ToolbarItems.Add(itmHelp);

            var itmLogout = new ToolbarItem
            {
                Order = ToolbarItemOrder.Secondary,
                Icon = "logout.png",
                Text = "Log out"
            };
            ToolbarItems.Add(itmLogout);
        }

        /// <summary>
        /// navigate through item menu
        /// </summary>
        /// <param name="menu"></param>
        public void NavigateTo(MenuItem menu)
        {
            Page displayPage = (Page)Activator.CreateInstance(menu.TargetType);

            Detail = new NavigationPage(displayPage);

            IsPresented = false;
        }

        /// <summary>
        /// navigate to a page
        /// </summary>
        /// <param name="displayPage"></param>
        public void NavigateTo(Page displayPage)
        {
            Detail = new NavigationPage(displayPage);

            IsPresented = false;
        }
    }
}
