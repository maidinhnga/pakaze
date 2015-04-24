using PAKAZE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using Xamarin.Forms;
using XLabs.Forms.Controls;

namespace PAKAZE.Views
{
    public class SinglePage : ContentPage
    {
        public SinglePage(SingleInfo singleInfo)
        {
            BackgroundColor = Color.White;
            BindingContext = singleInfo;
            //Title = "Singles";
            //Icon = "single.png";
            Content = new ScrollView
            {
                Padding = 10,
                IsClippedToBounds = true,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Orientation = ScrollOrientation.Vertical,
                Content = CreateSingleDirectoryLayout()
            };
        }

        /// <summary>
        /// root layout for the page
        /// </summary>
        /// <returns></returns>
        StackLayout CreateSingleDirectoryLayout()
        {
            var singleInfoLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                HeightRequest = 390
            };

            //avatar
            var imgSingleAvatar = new Image
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Aspect = Aspect.Fill
            };
            imgSingleAvatar.SetBinding(Image.SourceProperty, "Avatar");
            //image tapped event
            var singleAvatarTappedGesture = new TapGestureRecognizer { };
            singleAvatarTappedGesture.Tapped += async (obj, evt) =>
            {
                await Navigation.PushAsync(new SingleDetailPage(BindingContext as SingleInfo));
                //if (App.GetMainPage() is MainPage)
                //{
                //    ((MainPage)App.GetMainPage()).NavigateTo(new SingleDetailPage(BindingContext as SingleInfo)); //new MenuItem() { TargetType = typeof(SingleDirectoryPage) }
                //}

            };
            imgSingleAvatar.GestureRecognizers.Add(singleAvatarTappedGesture);

            var imageLayout = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.Fill,
                    Children = { imgSingleAvatar }
                };
            singleInfoLayout.Children.Add(imageLayout,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;// parent.Width / 2 - imgSingleAvatar.Width / 2;
                }));

            //name-age (top left)
            var lblName = new Label
            {
                FontSize = 18,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                TextColor = App.TintColor
            };
            lblName.SetBinding(Label.TextProperty, "Name");

            var lblAge = new Label
            {
                FontSize = 12,
                TextColor = Color.White
            };
            lblAge.SetBinding(Label.TextProperty, "Age");

            singleInfoLayout.Children.Add(
                new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = 10,
                    Children = { lblName, lblAge }
                },
                Constraint.RelativeToView(imageLayout, (parent, sibling) => { return sibling.X; }),
                Constraint.RelativeToView(imageLayout, (parent, sibling) => { return 0; })
            );
            //distance-likes
            var lblDistance = new Label
            {
                FontSize = 12,
                TextColor = Color.White
            };
            lblDistance.SetBinding(Label.TextProperty, "Distance");

            var lblNbrLikes = new Label
            {
                FontSize = 12,
                TextColor = Color.White
            };
            lblNbrLikes.SetBinding(Label.TextProperty, "NumberOfLikes");

            singleInfoLayout.Children.Add(
                new StackLayout
                {
                    Orientation = StackOrientation.Vertical,
                    Padding = 10,
                    Children = 
                    { 
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.End,
                            Children = 
                            {
                                lblDistance,
                                new Image { Source = "distance_icon.png"}
                            }
                        }, 
                        new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.End,
                            Children = 
                            {
                                lblNbrLikes,
                                new Image { Source = "single_like_icon.png"}
                            }
                        }                         
                    }
                },
                Constraint.RelativeToView(imageLayout, (parent, sibling) => { return sibling.X + sibling.Width - 75; }),
                Constraint.RelativeToView(imageLayout, (parent, sibling) => { return 0; })
            );


            //like-dislike buttons
            var imgUnlikeButton = new Image
            {
                Source = "unlike.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,

            };
            var imgLikeButton = new Image
            {
                Source = "like.png",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };

            var buttonStack = new StackLayout
            {
                //BackgroundColor = Color.Red,
                Spacing = 20,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Padding = new Thickness(20, 0, 20, 0),
                Children = { imgUnlikeButton, imgLikeButton }
            };

            singleInfoLayout.Children.Add(buttonStack,
                 Constraint.RelativeToView(imageLayout, (parent, sibling) =>
                 {
                     return sibling.X;
                 }),
                Constraint.RelativeToView(imageLayout, (parent, sibling) =>
                    {
                        return sibling.Height - buttonStack.Height + 10;
                    }),
                     Constraint.RelativeToView(imageLayout, (parent, sibling) =>
                     {
                         return sibling.Width;
                     })
            );

            //search bar
            var btnSearch = new SearchBar
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Search for a single"
            };

            //common liked pages
            var btnCommonLikedPage = new ImageButton
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = XLabs.Enums.ImageOrientation.ImageOnTop,
                Source = "common_liked_pages.png",
                Text = "Common liked pages",
                BackgroundColor = Color.Transparent,
                TextColor = App.TintColor,
                FontSize = 14
            };

            var rptCommonLikedPages = new XLabs.Forms.Controls.RepeaterView<FacebookPage>
            {
                Spacing = 10,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                ItemTemplate = new DataTemplate(() =>
                {
                    var imgAvatar = new CircleImage
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Aspect = Aspect.AspectFill,
                        WidthRequest = 72,
                        HeightRequest = 72
                    };
                    imgAvatar.SetBinding(CircleImage.SourceProperty, "Avatar");
                    ViewCell cell = new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children = { imgAvatar }
                        }
                    };

                    return cell;
                })
            };
            rptCommonLikedPages.SetBinding(RepeaterView<FacebookPage>.ItemsSourceProperty, "CommonLikedFacebookPages");

            //common friends
            var btnCommonFriends = new ImageButton
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = XLabs.Enums.ImageOrientation.ImageOnTop,
                Source = "common_friends.png",
                Text = "Common friends",
                BackgroundColor = Color.Transparent,
                TextColor = App.TintColor,
                FontSize = 14
            };

            var rptCommonFriends = new XLabs.Forms.Controls.RepeaterView<FacebookFriend>
            {
                Spacing = 10,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                ItemTemplate = new DataTemplate(() =>
                {
                    var imgAvatar = new CircleImage
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Aspect = Aspect.AspectFill,
                        WidthRequest = 72,
                        HeightRequest = 72
                    };
                    imgAvatar.SetBinding(CircleImage.SourceProperty, "Avatar");
                    ViewCell cell = new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children = { imgAvatar }
                        }
                    };

                    return cell;
                })
            };
            rptCommonFriends.SetBinding(RepeaterView<FacebookFriend>.ItemsSourceProperty, "CommonFacebookFriends");

            //common places
            var btnCommonPlaces = new ImageButton
            {
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Orientation = XLabs.Enums.ImageOrientation.ImageOnTop,
                Source = "common_places.png",
                Text = "Common places",
                BackgroundColor = Color.Transparent,
                TextColor = App.TintColor,
                FontSize = 14
            };
            var rptCommonPlaces = new XLabs.Forms.Controls.RepeaterView<Place>
            {
                Spacing = 10,
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                ItemTemplate = new DataTemplate(() =>
                {
                    var imgAvatar = new CircleImage
                    {
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        Aspect = Aspect.AspectFill,
                        WidthRequest = 72,
                        HeightRequest = 72
                    };
                    imgAvatar.SetBinding(CircleImage.SourceProperty, "Avatar");
                    ViewCell cell = new ViewCell
                    {
                        View = new StackLayout
                        {
                            Children = { imgAvatar }
                        }
                    };

                    return cell;
                })
            };
            rptCommonPlaces.SetBinding(RepeaterView<Place>.ItemsSourceProperty, "CommonPlaces");

            //root layout
            return new StackLayout
            {
                Spacing = 10,
                Orientation = StackOrientation.Vertical,
                //Padding = new Thickness(20, 0, 20, 10),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,

                Children = {
                    btnSearch,
                    singleInfoLayout,
                    new ExtendedStackLayout
                    {
                        HorizontalOptions = LayoutOptions.Fill,
                        BackgroundStartColor = Color.FromHex("DCDCDC"),
                        BackgroundEndColor = Color.FromHex("FFFFFF"),
                        Children = {
                            btnCommonLikedPage,
                            new ScrollView
                            {
                                Orientation = ScrollOrientation.Horizontal,
                                Content = rptCommonLikedPages
                            }                        
                        }                  
                    },
                    new ExtendedStackLayout
                    {
                        HorizontalOptions = LayoutOptions.Fill,
                        BackgroundStartColor = Color.FromHex("DCDCDC"),
                        BackgroundEndColor = Color.FromHex("FFFFFF"),
                        Children = {
                            btnCommonFriends,
                            new ScrollView
                            {
                                Orientation = ScrollOrientation.Horizontal,
                                Content = rptCommonFriends
                            }                            
                        }                        
                    },
                    new ExtendedStackLayout
                    {
                        HorizontalOptions = LayoutOptions.Fill,
                        BackgroundStartColor = Color.FromHex("DCDCDC"),
                        BackgroundEndColor = Color.FromHex("FFFFFF"),
                        Children = {
                            btnCommonPlaces,
                            new ScrollView {
                                Orientation = ScrollOrientation.Horizontal,
                                Content = rptCommonPlaces
                            }
                        }                        
                    }
                }
            };
        }
    }
}
