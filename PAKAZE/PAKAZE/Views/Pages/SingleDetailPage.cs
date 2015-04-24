using PAKAZE.Controls;
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
    public class SingleDetailPage : ContentPage
    {
        Image btnLike;
        Image btnUnlike;
        Image btnBlock;
        Image btnSendMessage;
        bool IsBlock = false;
        WrapLayout tabLayout;

        public SingleDetailPage(SingleInfo single)
        {
            BackgroundColor = Color.White;
            BindingContext = single;

            Content = new ScrollView
            {
                IsClippedToBounds = true,
                Orientation = ScrollOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Content = CreateSingleDetailLayout()
            };
        }

        /// <summary>
        /// create root layout
        /// </summary>
        /// <returns></returns>
        StackLayout CreateSingleDetailLayout()
        {
            var singlePhotoLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Padding = 0,
                //BackgroundColor = Color.Red,
            };
            //images gallery
            var photoGallery = new ImageGallery
            {
                //BackgroundColor = Color.Yellow,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 395
            };
            photoGallery.SetBinding(ImageGallery.ItemsSourceProperty, "Photos");

            singlePhotoLayout.Children.Add(
                photoGallery,
                Constraint.RelativeToParent((parent) =>
                {
                    return 0;
                }));
            //name
            var lblName = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 18,
                FontAttributes = Xamarin.Forms.FontAttributes.Bold,
                TextColor = App.TintColor,
            };
            lblName.SetBinding(Label.TextProperty, "Name");

            var lblResume = new Label
            {
                VerticalOptions = LayoutOptions.CenterAndExpand,
                FontSize = 12,
                TextColor = App.InfoTextColor
            };
            lblResume.SetBinding(Label.TextProperty, "Age");
            var singleTopResumeLayout = new StackLayout
            {
                Spacing = 0,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Orientation = StackOrientation.Vertical,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Padding = 10,
                BackgroundColor = Color.FromHex("DDDCDC"),
                HeightRequest = 70,
                Children = { lblName, lblResume }
            };

            singlePhotoLayout.Children.Add(singleTopResumeLayout,
                Constraint.RelativeToView(photoGallery, (parent, sibling) =>
                {
                    return sibling.X;
                }),
                Constraint.RelativeToView(photoGallery, (parent, sibling) =>
                {
                    return sibling.Y + sibling.Height;
                }),
                Constraint.RelativeToView(photoGallery, (parent, sibling) =>
                {
                    return sibling.Width;
                }),
                Constraint.Constant(70)
                );
            //action buttons
            btnUnlike = new Image
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 50,
                Source = "unlike_50.png"
            };
            var unlikeGesture = new TapGestureRecognizer();
            unlikeGesture.Tapped += UnlikeGesture_Tapped;
            btnUnlike.GestureRecognizers.Add(unlikeGesture);

            btnLike = new Image
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 50,
                Source = "like_50.png"
            };
            var likeGesture = new TapGestureRecognizer();
            likeGesture.Tapped += LikeGesture_Tapped;
            btnLike.GestureRecognizers.Add(likeGesture);

            btnBlock = new Image
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 50,
                Source = "block_50.png"
            };
            var blockGesture = new TapGestureRecognizer();
            blockGesture.Tapped += BlockGesture_Tapped;
            btnBlock.GestureRecognizers.Add(blockGesture);

            btnSendMessage = new Image
            {
                HorizontalOptions = LayoutOptions.EndAndExpand,
                WidthRequest = 50,
                Source = "message_50.png"
            };
            var sendMessageGesture = new TapGestureRecognizer();
            sendMessageGesture.Tapped += SendMessageGesture_Tapped;
            btnSendMessage.GestureRecognizers.Add(sendMessageGesture);

            var actionButtonsLayout = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Spacing = 0,
                Children = { 
                    new StackLayout {
                        Orientation = StackOrientation.Horizontal,
                        HorizontalOptions = LayoutOptions.EndAndExpand,
                        Spacing = 2,
                        Children = {
                            btnUnlike, btnLike, btnBlock, 
                            btnSendMessage 
                        }
                    }
                }
            };
            singlePhotoLayout.Children.Add(actionButtonsLayout,
                Constraint.RelativeToView(photoGallery, (parent, sibling) =>
                {
                    return 0; // sibling.X + sibling.Width - actionButtonsLayout.Width;
                }),
                Constraint.RelativeToView(photoGallery, (parent, sibling) =>
                {
                    return sibling.Y + sibling.Height - 25;
                }),
                Constraint.RelativeToView(photoGallery, (parent, sibling) =>
                {
                    return sibling.Width;
                })
            );

            //Single additional infos
            var additionalView = CreateAdditionalInfoView();

            //liked pages, common friends, common places            
            var commonTabs = CreateCommonTabs();
            //root layout
            var rootLayout = new StackLayout
            {
                Spacing = 0,
                Orientation = StackOrientation.Vertical,
                Padding = new Thickness(20, 10, 20, 10),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Children = 
                {                    
                    singlePhotoLayout,
                    additionalView,
                    commonTabs
                }
            };
            return rootLayout;
        }

        View CreateCommonTabs()
        {
            tabLayout = new WrapLayout
            {
                //Spacing = 5,
                Orientation = StackOrientation.Horizontal,
                Padding = new Thickness(0, 20, 0, 0),
                //Padding = new Thickness(5, Device.OnPlatform(20, 0, 0), 5, 0),
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center,
            };

            var tabControl = new TabbledControl
            {
                //Padding = new Thickness(0, 50, 0, 10),
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                TintColor = App.TintColor,
            };
            tabControl.AddSegment("Common liked pages", "common_liked_pages.png", 100, 75);
            tabControl.AddSegment("Common friends", "common_friends.png", 100, 75);
            tabControl.AddSegment("Common places", "common_places.png", 100, 75);
            tabControl.SelectedSegmentChanged += tabControl_SelectedSegmentChanged;
            tabControl.SelectedSegment = 1;
            return new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand,
                Children = 
                { 
                    tabControl, 
                    tabLayout       
                }
            };
        }

        /// <summary>
        /// will be re-implemented in the future for better performance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tabControl_SelectedSegmentChanged(object sender, int e)
        {
            tabLayout.Children.Clear();
            var singleInfo = BindingContext as SingleInfo;
            if (e == 0)
            {
                foreach (var page in singleInfo.CommonLikedFacebookPages)
                {
                    var grid = new Grid
                    {
                        WidthRequest = 150,
                        HeightRequest = 150,
                    };
                    var bg = new ExtendedStackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center,
                        HeightRequest = 75,
                    };
                    grid.Children.Add(bg, 0, 0);

                    var cell = new StackLayout
                    {
                        WidthRequest = 150,
                        HeightRequest = 150,
                        Children = {
						    new CircleImage 
                            {
                                Aspect = Xamarin.Forms.Aspect.AspectFit,
                                Source = page.Avatar, 
							    VerticalOptions = LayoutOptions.Start,
							    WidthRequest=75,
							    HeightRequest=75
                            },
						    new Label 
                            {
                                Text = page.Name,					
	                            TextColor = App.InfoTextColor,
							    VerticalOptions = LayoutOptions.Start, 
							    HorizontalOptions = LayoutOptions.Center
                            }
					    }
                    };
                    grid.Children.Add(cell, 0, 0);

                    tabLayout.Children.Add(grid);
                }
            }
            else if (e == 1)
            {
                foreach (var friend in singleInfo.CommonFacebookFriends)
                {
                    var grid = new Grid
                    {
                        WidthRequest = 150,
                        HeightRequest = 150,
                    };
                    var bg = new ExtendedStackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center,
                        HeightRequest = 75,
                    };
                    grid.Children.Add(bg, 0, 0);

                    var cell = new StackLayout
                    {
                        WidthRequest = 150,
                        HeightRequest = 150,
                        Children = {
						    new CircleImage 
                            {
                                Aspect = Xamarin.Forms.Aspect.AspectFit,
                                Source = friend.Avatar, 
							    VerticalOptions = LayoutOptions.Start,
							    WidthRequest=75,
							    HeightRequest=75
                            },
						    new Label 
                            {
                                Text = friend.Age, 						
	                            TextColor = App.InfoTextColor,
							    VerticalOptions = LayoutOptions.Start, 
							    HorizontalOptions = LayoutOptions.Center
                            }
					    }
                    };

                    grid.Children.Add(cell, 0, 0);
                    tabLayout.Children.Add(grid);
                }
            }
            else if (e == 2)
            {
                foreach (var place in singleInfo.CommonPlaces)
                {
                    var grid = new Grid
                    {
                        WidthRequest = 150,
                        HeightRequest = 150,
                    };
                    var bg = new ExtendedStackLayout
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        VerticalOptions = LayoutOptions.Center,
                        HeightRequest = 75,
                    };
                    grid.Children.Add(bg, 0, 0);

                    var cell = new StackLayout
                    {
                        WidthRequest = 150,
                        HeightRequest = 150,
                        Children = 
                        {
                            new StackLayout
                            {
                                VerticalOptions = LayoutOptions.Start, 
							    HorizontalOptions = LayoutOptions.Center,
                                WidthRequest=75,
							    HeightRequest=75,
                                Children = 
                                {
                                    new CircleImage 
                                    {
                                        Aspect = Xamarin.Forms.Aspect.AspectFill,
                                        Source = place.Avatar, 
							            VerticalOptions = LayoutOptions.Start,
							            //WidthRequest=75,
							            //HeightRequest=75
                                    }
                                }
                            }
						    ,
						    new Label 
                            {
                                Text = place.Name,					
	                            TextColor = App.InfoTextColor,
							    VerticalOptions = LayoutOptions.Start, 
							    HorizontalOptions = LayoutOptions.Center
                            }
					    }
                    };

                    grid.Children.Add(cell, 0, 0);
                    tabLayout.Children.Add(grid);
                }
            }
        }

        /// <summary>
        /// additional info
        /// </summary>
        /// <returns></returns>
        View CreateAdditionalInfoView()
        {
            var lblHeightTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Height"
            };
            var lblHeightValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblHeightValue.SetBinding(Label.TextProperty, "Height");

            var lblWeightTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Weight"
            };
            var lblWeightValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblWeightValue.SetBinding(Label.TextProperty, "Weight");
            //hair&eyes color
            var lblHairColorTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Hair color"
            };
            var lblHairColorValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblHairColorValue.SetBinding(Label.TextProperty, "HairColor");

            var lblEyesColorTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Eyes color"
            };
            var lblEyesColorValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblEyesColorValue.SetBinding(Label.TextProperty, "EyesColor");

            //languages, religion
            var lblLanguageTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Languages"
            };
            var lblLanguageValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblLanguageValue.SetBinding(Label.TextProperty, "Languages");

            var lblReligionTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Religion"
            };
            var lblReligionValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblReligionValue.SetBinding(Label.TextProperty, "Religion");

            //job, family
            var lblJobTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Job"
            };
            var lblJobValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblJobValue.SetBinding(Label.TextProperty, "Job");

            var lblFamilyTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Family"
            };
            var lblFamilyValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblFamilyValue.SetBinding(Label.TextProperty, "Family");

            //interesting
            var lblInterestTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Interests"
            };
            var lblInterestValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblInterestValue.SetBinding(Label.TextProperty, "Interests");

            var lblDescriptionTitle = new Label
            {
                FontSize = 12,
                TextColor = App.TintColor,
                FontAttributes = FontAttributes.Bold,
                Text = "Description"
            };
            var lblDescriptionValue = new Label
            {
                FontSize = 12,
                TextColor = App.InfoTextColor,
            };
            lblDescriptionValue.SetBinding(Label.TextProperty, "Description");

            var tvAdditionalInfo = new TableView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Intent = TableIntent.Data,
                HasUnevenRows = true,
                //RowHeight = 60,
                Root = new TableRoot
                {
                    new TableSection("")
                    {
                        new ViewCell
                        {
                             View = new StackLayout
                             {
                                 //HeightRequest = 60,
                                 Padding = 10,
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 Orientation = StackOrientation.Horizontal,
                                 Children = 
                                 {
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         {
                                             lblHeightTitle, 
                                             lblHeightValue,
                                         } 
                                     },
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         {
                                             lblWeightTitle, 
                                             lblWeightValue,                                        
                                         } 
                                     },
                                 }
                             }
                        },
                        new ViewCell
                        {
                             View = new StackLayout
                             {
                                 //HeightRequest = 60,
                                 Padding = 10,
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 Orientation = StackOrientation.Horizontal,
                                 Children = 
                                 {
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         {
                                             lblHairColorTitle, 
                                             lblHairColorValue,
                                         } 
                                     },
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         { 
                                             lblEyesColorTitle, 
                                             lblEyesColorValue,
                                         } 
                                     },
                                 }
                             }
                        },
                        new ViewCell
                        {
                             View = new StackLayout
                             {
                                 //HeightRequest = 60,
                                 Padding = 10,
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 Orientation = StackOrientation.Horizontal,
                                 Children = 
                                 {
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         {
                                             lblLanguageTitle, 
                                             lblLanguageValue,
                                         } 
                                     },
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         {
                                             lblReligionTitle, 
                                             lblReligionValue,

                                         } 
                                     },
                                 }
                             }
                        },
                        new ViewCell
                        {
                             View = new StackLayout
                             {
                                 Padding = 10,
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 Orientation = StackOrientation.Horizontal,
                                 Children = 
                                 {
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         {
                                             lblJobTitle, 
                                             lblJobValue,
                                         } 
                                     },
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         {
                                             lblFamilyTitle, 
                                             lblFamilyValue,
                                         } 
                                     },
                                 }
                             }
                        },
                        new ViewCell
                        {
                            //Height = 60,
                             View = new StackLayout
                             {
                                 //HeightRequest = 60,
                                 Padding = 10,
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 Orientation = StackOrientation.Horizontal,
                                 Children = 
                                 {
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = 
                                         {
                                             lblInterestTitle, 
                                             lblInterestValue,
                                         } 
                                     }
                                 }
                             }
                        },
                        new ViewCell
                        {
                             View = new StackLayout
                             {
                                 //HeightRequest = 60,
                                 Padding = 10,
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 Orientation = StackOrientation.Horizontal,
                                 Children = 
                                 {
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = {
                                             lblDescriptionTitle, 
                                             lblDescriptionValue,
                                         } 
                                     }
                                 }
                             }
                        },
                        new ViewCell
                        {
                             View = new StackLayout
                             {
                                 //HeightRequest = 60,
                                 Padding = 10,
                                 HorizontalOptions = LayoutOptions.FillAndExpand,
                                 VerticalOptions = LayoutOptions.FillAndExpand,
                                 Orientation = StackOrientation.Horizontal,
                                 Children = 
                                 {
                                     new StackLayout 
                                     { 
                                         Orientation = StackOrientation.Vertical, 
                                         HorizontalOptions = LayoutOptions.FillAndExpand, 
                                         VerticalOptions = LayoutOptions.Center, 
                                         Spacing = 0, 
                                         Children = {
                                             lblDescriptionTitle, 
                                             lblDescriptionValue,
                                         }
                                     }
                                 }
                             }
                        }
                    }
                }
            };

            return tvAdditionalInfo;
        }

        #region events
        void SendMessageGesture_Tapped(object sender, EventArgs e)
        {

        }

        void BlockGesture_Tapped(object sender, EventArgs e)
        {
            IsBlock = !IsBlock;
            btnBlock.Source = IsBlock ? "unblock_50.png" : "block_50.png";
        }

        void LikeGesture_Tapped(object sender, EventArgs e)
        {
            btnUnlike.IsEnabled = true;
            btnUnlike.Source = "unlike_50.png";

            btnLike.IsEnabled = false;
            btnLike.Source = "like_disabled_50.png";

            btnSendMessage.IsEnabled = true;
            btnSendMessage.Source = "message_50.png";
        }

        void UnlikeGesture_Tapped(object sender, EventArgs e)
        {
            btnUnlike.IsEnabled = false;
            btnUnlike.Source = "unlike_disabled_50.png";

            btnLike.IsEnabled = true;
            btnLike.Source = "like_50.png";

            btnSendMessage.IsEnabled = false;
            btnSendMessage.Source = "message_disabled_50.png";
        }
        #endregion
    }
}
