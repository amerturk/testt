using Android.Content;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Signbook.Controls;
using Signbook.Droid.Renders;
using Signbook.Services.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;
using TextAlignment = Android.Views.TextAlignment;

[assembly: ExportRenderer(typeof(ExtEntry), typeof(ExtEntryDroid))]
namespace Signbook.Droid.Renders
{
    public class ExtEntryDroid : EntryRenderer
    {
        public ExtEntryDroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control == null || !(Element is ExtEntry entry))
                return;

            Control.SetPadding(15, 15, 15, 0);

            SetEntryBorder(entry);

            SetCursor();

            SetTextAlignment();

            SetCustomPadding(entry);
        }

        private void SetCustomPadding(ExtEntry entry)
        {
            if (entry.Padding != default(Thickness))
            {
                Control.SetPadding((int)entry.Padding.Left, (int)entry.Padding.Top, (int)entry.Padding.Right, (int)entry.Padding.Bottom);
            }
        }

        private void SetTextAlignment()
        {
            var currentLanguage = LocalizationService.Current.GetCurrentLanguage();
            //Control.TextDirection = currentLanguage == Language.Arabic ? TextDirection.Rtl : TextDirection.Ltr;
            Control.TextDirection = TextDirection.Rtl;
            Control.TextAlignment = TextAlignment.ViewStart;
        }
        private void SetCursor()
        {
            var intPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
            var mCursorDrawableResProperty = JNIEnv.GetFieldID(intPtrtextViewClass, "mCursorDrawableRes", "I");
            JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.myCursor);

        }
        private void SetEntryBorder(ExtEntry entry)
        {
            if (entry.HasBorder && !string.IsNullOrEmpty(entry.BorderColor))
            {
                var gd = new GradientDrawable();
                gd.SetColor(Android.Graphics.Color.White);
                gd.SetCornerRadius(10);
                gd.SetStroke(2, Android.Graphics.Color.ParseColor(entry.BorderColor));
                Control.Background = gd;
            }
            else
            {
                Control.SetBackgroundColor(Color.Transparent.ToAndroid());
            }
        }
    }
}