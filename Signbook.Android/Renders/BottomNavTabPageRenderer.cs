using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Views;
using Android.Widget;
using Signbook.Droid.Helpers;
using Signbook.Droid.Renders;
using Signbook.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(BottomNavTabPage), typeof(BottomNavTabPageRenderer))]
namespace Signbook.Droid.Renders
{
    public class BottomNavTabPageRenderer : TabbedPageRenderer
    {
        private bool _isShiftModeSet;
        public BottomNavTabPageRenderer(Context context)
            : base(context)
        {
        }
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            try
            {
                if (!_isShiftModeSet)
                {
                    var children = GetAllChildViews(ViewGroup);

                    if (children.SingleOrDefault(x => x is BottomNavigationView) is BottomNavigationView bottomNav)
                    {
                        bottomNav.SetShiftMode(false, false);
                        _isShiftModeSet = true;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error setting ShiftMode: {e}");
            }
        }

        private List<Android.Views.View> GetAllChildViews(Android.Views.View view)
        {
            if (!(view is ViewGroup group))
            {
                return new List<Android.Views.View> { view };
            }

            var result = new List<Android.Views.View>();

            for (int i = 0; i < group.ChildCount; i++)
            {
                var child = group.GetChildAt(i);

                var childList = new List<Android.Views.View> { child };
                childList.AddRange(GetAllChildViews(child));

                result.AddRange(childList);
            }

            return result.Distinct().ToList();
        }
       
    }
    public static class ViewGroupExtension
    {
        public static IEnumerable<T> Children<T>(this ViewGroup parent)
            where T : Android.Views.View
        {
            for (int x = 0; x < parent.ChildCount; ++x)
            {
                Android.Views.View child = parent.GetChildAt(x);
                if (child is T tChild)
                    yield return tChild;
                if (child is Android.Views.ViewGroup vg)
                {
                    foreach (T item in vg.Children<T>())
                        yield return item;
                }
            }
        }
    }
    //public class BottomNavTabPageRenderer : TabbedPageRenderer
    //{
    //    private bool _isShiftModeSet;

    //    public BottomNavTabPageRenderer(Context context)
    //        : base(context)
    //    {

    //    }

    //    protected override void OnLayout(bool changed, int l, int t, int r, int b)
    //    {
    //        base.OnLayout(changed, l, t, r, b);
    //        try
    //        {
    //            if (!_isShiftModeSet)
    //            {
    //                var children = GetAllChildViews(ViewGroup);

    //                if (children.SingleOrDefault(x => x is BottomNavigationView) is BottomNavigationView bottomNav)
    //                {
    //                    bottomNav.SetShiftMode(false, false);
    //                    _isShiftModeSet = true;
    //                }
    //            }
    //        }
    //        catch (Exception e)
    //        {
    //            Console.WriteLine($"Error setting ShiftMode: {e}");
    //        }
    //    }

    //    private List<Android.Views.View> GetAllChildViews(Android.Views.View view)
    //    {
    //        if (!(view is ViewGroup group))
    //        {
    //            return new List<Android.Views.View> { view };
    //        }

    //        var result = new List<Android.Views.View>();

    //        for (int i = 0; i < group.ChildCount; i++)
    //        {
    //            var child = group.GetChildAt(i);

    //            var childList = new List<Android.Views.View> { child };
    //            childList.AddRange(GetAllChildViews(child));

    //            result.AddRange(childList);
    //        }

    //        return result.Distinct().ToList();
    //    }
    //}
}