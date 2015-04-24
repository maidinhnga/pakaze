using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms;
using PAKAZE.Droid.Controls;


[assembly: ExportRenderer(typeof(TextCell), typeof(TableSectionHeaderRenderer))]
namespace PAKAZE.Droid.Controls
{
    public class TableSectionHeaderRenderer : TextCellRenderer
    {
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, ViewGroup parent, Context context)
        {
            // Hide cells of TableSections with no title.
            var view = base.GetCellCore(item, convertView, parent, context) as ViewGroup;
            if (item is TextCell)
            {
                if (String.IsNullOrWhiteSpace((item as TextCell).Text))
                {
                    view.Visibility = ViewStates.Gone;
                    while (view.ChildCount > 0)
                    {
                        view.RemoveViewAt(0);
                    }
                    view.SetMinimumHeight(0);
                    view.SetPadding(0, 0, 0, 0);
                }
            }
            return view;
        }
    }
}