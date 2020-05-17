using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Signbook.Controls;
using Signbook.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtSwitch), typeof(ExtSwitchDroid))]
namespace Signbook.Droid.Renders
{
    public class ExtSwitchDroid : SwitchRenderer
    {
        private ExtSwitch _extSwitch;
        private Android.Graphics.Color _greyColor = new Android.Graphics.Color(211, 211, 211);
        private Android.Graphics.Color _greenColor = new Android.Graphics.Color(37, 173, 59);
        private Android.Graphics.Color _whiteColor = new Android.Graphics.Color(255, 255, 255);
        public ExtSwitchDroid(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Switch> e)
        {
            base.OnElementChanged(e);
            if (Control == null || Element == null)
                return;
            _extSwitch = (ExtSwitch)e.NewElement;
            if (this.Control.Checked)
            {
                this.Control.ThumbDrawable.SetColorFilter(_greyColor, PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(_greenColor, PorterDuff.Mode.Darken);
            }
            else
            {
                this.Control.ThumbDrawable.SetColorFilter(_whiteColor, PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(_greyColor, PorterDuff.Mode.Darken);
            }
            this.Control.CheckedChange += this.OnCheckedChange;
        }
        protected override void Dispose(bool disposing)
        {
            if (Control != null)
            {
                this.Control.CheckedChange -= this.OnCheckedChange;
            }
            base.Dispose(disposing);
        }
        private void OnCheckedChange(object sender, CompoundButton.CheckedChangeEventArgs e)
        {
            if (this.Control.Checked)
            {
                _extSwitch.IsToggled = this.Control.Checked;
                this.Control.ThumbDrawable.SetColorFilter(_greyColor, PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(_greenColor, PorterDuff.Mode.Darken);
            }
            else
            {
                this.Control.ThumbDrawable.SetColorFilter(_whiteColor, PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(_greyColor, PorterDuff.Mode.Darken);
            }
            var args = new ToggledEventArgs(e.IsChecked);
            _extSwitch.OnToggledDroid(sender, args);
        }
    }

}