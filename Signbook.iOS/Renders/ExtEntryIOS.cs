
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using CoreGraphics;
using Foundation;
using Signbook.Controls;
using Signbook.iOS.Renders;
using Signbook.Services.Localization;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;



[assembly: ExportRenderer(typeof(ExtEntry), typeof(ExtEntryIOS))]
namespace Signbook.iOS.Renders
{
    public class ExtEntryIOS : EntryRenderer
    {

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control == null || e.NewElement == null)
                return;
            Control.BorderStyle = UITextBorderStyle.None;
            Control.LeftView = new UIView(new CGRect(0, 0, 15, 0));
            Control.LeftViewMode = UITextFieldViewMode.Always;
            Control.RightView = new UIView(new CGRect(0, 0, 15, 0));
            Control.RightViewMode = UITextFieldViewMode.Always;
            SetTextAlignment();
        }

        private void SetTextAlignment()
        {
            Control.TextAlignment = UITextAlignment.Right;
            /*if (LocalizationService.Current.GetCurrentLanguage() == Language.Arabic)
            {
                Control.TextAlignment = UITextAlignment.Right;
            }
            else
            {
                Control.TextAlignment = UITextAlignment.Left;
            }*/
        }
    }
}