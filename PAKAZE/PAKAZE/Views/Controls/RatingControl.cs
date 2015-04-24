
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PAKAZE.Controls
{
    public class RatingControl : BaseContent
    {
        List<Image> listImage = new List<Image>();
        StackLayout stloContent;
        string pageId;
        int itemId;
        Topic myTopic;
        Action callBack;
        public RatingControl(double starWidthRequest, double starHeightRequest, bool isFocusable = false) 
        {
            InitializeComponent(starWidthRequest, starHeightRequest, isFocusable);
        }
        public RatingControl(double starWidthRequest, double starHeightRequest, Topic topic, double Spacing = 0, double SpacingBetweenHalfStars = 0, bool isFocusable = false)
        {
            InitializeComponent(starWidthRequest, starHeightRequest, isFocusable, Spacing, SpacingBetweenHalfStars);
            myTopic = topic;
            BindRatingValue((topic == null ? 0 : topic.Rating), isFocusable);
        }
        public void RatingFor(string pUrl, int id, Action handler = null)
        {
            this.pageId = pUrl;
            this.itemId = id;
            this.callBack = handler;
        }
        void InitializeComponent(double starWidthRequest, double starHeightRequest, bool isFocusable, double Spacing = 0, double SpacingBetweenHalfStars = 0)
        {
            //if (!isFocusable)
            //{
                Image starNo1 = new Image
                {
                    Source = Device.OnPlatform("leftbluestar.png", "leftbluestar.png", "Images/leftbluestar.png"),
                    ClassId = "1"
                };
                Image starNo2 = new Image
                {
                    Source = Device.OnPlatform("rightbluestar.png", "rightbluestar.png", "Images/rightbluestar.png"),
                    ClassId = "2"
                };
                Image starNo3 = new Image
                {
                    Source = Device.OnPlatform("leftbluestar.png", "leftbluestar.png", "Images/leftbluestar.png"),
                    ClassId = "3"
                };
                Image starNo4 = new Image
                {
                    Source = Device.OnPlatform("rightbluestar.png", "rightbluestar.png", "Images/rightbluestar.png"),
                    ClassId = "4"
                };
                Image starNo5 = new Image
                {
                    Source = Device.OnPlatform("leftbluestar.png", "leftbluestar.png", "Images/leftbluestar.png"),
                    ClassId = "5"
                };
                Image starNo6 = new Image
                {
                    Source = Device.OnPlatform("rightbluestar.png", "rightbluestar.png", "Images/rightbluestar.png"),
                    ClassId = "6"
                };
                Image starNo7 = new Image
                {
                    Source = Device.OnPlatform("leftbluestar.png", "leftbluestar.png", "Images/leftbluestar.png"),
                    ClassId = "7"
                };
                Image starNo8 = new Image
                {
                    Source = Device.OnPlatform("rightbluestar.png", "rightbluestar.png", "Images/rightbluestar.png"),
                    ClassId = "8"
                };
                Image starNo9 = new Image
                {
                    Source = Device.OnPlatform("leftbluestar.png", "leftbluestar.png", "Images/leftbluestar.png"),
                    ClassId = "9"
                };
                Image starNo10 = new Image
                {
                    Source = Device.OnPlatform("rightbluestar.png", "rightbluestar.png", "Images/rightbluestar.png"),
                    ClassId = "10"
                };
                stloContent = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal,
                    Spacing = Spacing,
                    Children = 
                    {
                        new StackLayout
                        {
                            Spacing = SpacingBetweenHalfStars,
                            Orientation = StackOrientation.Horizontal,
                            Children = 
                            {
                        starNo1,
                                starNo2
                            }
                        },
                        new StackLayout
                        {
                            Spacing = SpacingBetweenHalfStars,
                            Orientation = StackOrientation.Horizontal,
                            Children = 
                            {
                        starNo3,
                        starNo4,
                            }
                        },
                        new StackLayout
                        {
                            Spacing = SpacingBetweenHalfStars,
                            Orientation = StackOrientation.Horizontal,
                            Children = 
                            {
                        starNo5,
                        starNo6,
                            }
                        },
                        new StackLayout
                        {
                            Spacing = SpacingBetweenHalfStars,
                            Orientation = StackOrientation.Horizontal,
                            Children = 
                            {
                        starNo7,
                        starNo8,
                            }
                        },
                        new StackLayout
                        {
                            Spacing = SpacingBetweenHalfStars,
                            Orientation = StackOrientation.Horizontal,
                            Children = 
                            {
                        starNo9,
                        starNo10
                    }
                        }
                    }
                };
                listImage.Clear();
                listImage.Add(starNo1);
                listImage.Add(starNo2);
                listImage.Add(starNo3);
                listImage.Add(starNo4);
                listImage.Add(starNo5);
                listImage.Add(starNo6);
                listImage.Add(starNo7);
                listImage.Add(starNo8);
                listImage.Add(starNo9);
                listImage.Add(starNo10);
                if (starHeightRequest > 0 && starWidthRequest > 0)
                {
                    foreach (Image item in listImage)
                    {
                        item.WidthRequest = starWidthRequest;
                        item.HeightRequest = starHeightRequest;
                    }
                }
            //}
            //else
            //{
            //    Image starNo1 = new Image
            //    {
            //        Source = Device.OnPlatform("bluestar.png", "bluestar.png", "Images/bluestar.png"),
            //        WidthRequest = starWidthRequest,
            //        HeightRequest = starHeightRequest,
            //        ClassId = "1"
            //    };
            //    Image starNo2 = new Image
            //    {
            //        Source = Device.OnPlatform("bluestar.png", "bluestar.png", "Images/bluestar.png"),
            //        WidthRequest = starWidthRequest,
            //        HeightRequest = starHeightRequest,
            //        ClassId = "2"
            //    };
            //    Image starNo3 = new Image
            //    {
            //        Source = Device.OnPlatform("bluestar.png", "bluestar.png", "Images/bluestar.png"),
            //        WidthRequest = starWidthRequest,
            //        HeightRequest = starHeightRequest,
            //        ClassId = "3"
            //    };
            //    Image starNo4 = new Image
            //    {
            //        Source = Device.OnPlatform("bluestar.png", "bluestar.png", "Images/bluestar.png"),
            //        WidthRequest = starWidthRequest,
            //        HeightRequest = starHeightRequest,
            //        ClassId = "4"
            //    };
            //    Image starNo5 = new Image
            //    {
            //        Source = Device.OnPlatform("bluestar.png", "bluestar.png", "Images/bluestar.png"),
            //        WidthRequest = starWidthRequest,
            //        HeightRequest = starHeightRequest,
            //        ClassId = "5"
            //    };
            //    stloContent = new StackLayout
            //    {
            //        Orientation = StackOrientation.Horizontal,
            //        Spacing = Spacing,
            //        Children = 
            //        {
            //            starNo1,
            //            starNo2,
            //            starNo3,
            //            starNo4,
            //            starNo5,
            //        }
            //    };
            //    listImage.Clear();
            //    listImage.Add(starNo1);
            //    listImage.Add(starNo2);
            //    listImage.Add(starNo3);
            //    listImage.Add(starNo4);
            //    listImage.Add(starNo5);
                if (isFocusable)
                {
                var tapGestureRecognize = new TapGestureRecognizer();
                tapGestureRecognize.NumberOfTapsRequired = 1;
                //tapGestureRecognize.Tapped += OnTapGestureRecognizerTapped;
                foreach (Image item in listImage)
                {
                    item.GestureRecognizers.Add(tapGestureRecognize);
                }
            }
            //}
            Content = stloContent;
        }
        public void BindRatingValue(double rating, bool isFocusable = false)
        {
            //if (isFocusable)
            //{
            //    int r = (int)rating;
            //    for (int i = 0; i < r; i++)
            //    {
            //        listImage[i].Source = Device.OnPlatform("bluestar.png", "bluestar.png", "Images/bluestar.png");
            //    }
            //    for (int i = r; i < listImage.Count; i++)
            //    {
            //        listImage[i].Source = Device.OnPlatform("whitestar.png", "whitestar.png", "Images/whitestar.png");
            //    }
            //}
            //else
            //{
                int r = (int)(rating * 2);
                for (int i = 0; i < r; i++)
                {
                    if (i % 2 > 0)
                    {
                        listImage[i].Source = Device.OnPlatform("rightbluestar.png", "rightbluestar.png", "Images/rightbluestar.png");
                    }
                    else
                    {
                        listImage[i].Source = Device.OnPlatform("leftbluestar.png", "leftbluestar.png", "Images/leftbluestar.png");
                    }
                };
                for (int i = r; i < listImage.Count; i++)
                {
                    if (i % 2 > 0)
                    {
                        listImage[i].Source = Device.OnPlatform("rightwhitestar.png", "rightwhitestar.png", "Images/rightwhitestar.png");
                    }
                    else
                    {
                        listImage[i].Source = Device.OnPlatform("leftwhitestar.png", "leftwhitestar.png", "Images/leftwhitestar.png");
                    }
                };
            //}
        }

        //private async void OnTapGestureRecognizerTapped(object sender, EventArgs e)
        //{
        //    Image image = sender as Image;
        //    double id = 1;
        //    double.TryParse(image.ClassId, out id);
        //    id = Math.Round(id / 2);
        //    float rv = -1;
        //    bool cf;
        //    if (id > 0 && id < 11)
        //    {
        //        cf = await Confirm(string.Format("Rate this topic as {0} star?", id), "Confirmation");
        //        if (cf)
        //        {
        //            rv = await SetRating(pageId, itemId, (int)id);
        //            if (rv > -1)
        //            {
        //                myTopic.Rating = rv;
        //                BindRatingValue(rv, true);
        //            }
        //        }
        //    }
        //}
    }
}
