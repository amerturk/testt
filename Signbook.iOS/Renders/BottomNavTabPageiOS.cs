using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Signbook.Controls;
using Signbook.iOS.Renders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BottomNavTabPage), typeof(BottomNavTabPageiOS))]

namespace Signbook.iOS.Renders
{
        public class BottomNavTabPageiOS : TabbedRenderer
        {
            protected override void OnElementChanged(VisualElementChangedEventArgs e)
            {
                base.OnElementChanged(e);

                //TabBar.TintColor = UIColor.White;
                //TabBar.BarTintColor = UIColor.Black;
                TabBar.BackgroundColor = UIColor.White;

            }
        }
    
}