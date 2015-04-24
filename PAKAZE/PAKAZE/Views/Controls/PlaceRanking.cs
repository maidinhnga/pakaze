using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PAKAZE.Controls
{
    public class PlaceRanking : StackLayout
    {
        private static event EventHandler _RankingChanged;

        /// <summary>
        /// Backing Storage for the Orientation property
        /// </summary>
        public static readonly BindableProperty RankingProperty =
            BindableProperty.Create<PlaceRanking, int>(w => w.Ranking, 0,
                propertyChanged: Ranking_OnPropertyChanged);

        /// <summary>
        /// Orientation (Horizontal or Vertical)
        /// </summary>
        public int Ranking
        {
            get { return (int)GetValue(RankingProperty); }
            set { SetValue(RankingProperty, value); }
        }

        public PlaceRanking()
        {
            Orientation = StackOrientation.Horizontal;
            Spacing = 2;
            HorizontalOptions = LayoutOptions.StartAndExpand;
        }

        private static void Ranking_OnPropertyChanged(BindableObject bindable, int oldvalue, int newvalue)
        {
            var ranking = (PlaceRanking)bindable;
            ranking.Children.Clear();
            for (var i = 0; i < newvalue; i++)
            {
                ranking.Children.Add(new Image { Source = "rating_full.png" });
            }
            for (var i = newvalue; i < 3; i++)
            {
                ranking.Children.Add(new Image { Source = "rating_empty.png" });
            }
        }
    }
}
