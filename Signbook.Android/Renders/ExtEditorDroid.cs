
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using Signbook.Controls;
using Signbook.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TextAlignment = Android.Views.TextAlignment;

[assembly: ExportRenderer(typeof(ExtEditor), typeof(ExtEditorDroid))]
namespace Signbook.Droid.Renders
{
    public class ExtEditorDroid : EditorRenderer
    {
        ExtEditor _element;
        public ExtEditorDroid(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (Control == null || !(Element is ExtEditor element))
                    return;
                _element = element;
                Control.Hint = _element.Placeholder;
                Control.SetHintTextColor(_element.PlaceholderColor.ToAndroid());

                UpdateReturnKeyType(_element);

                if (_element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.End)
                {
                    Control.TextAlignment = Android.Views.TextAlignment.ViewEnd;
                }
                else if (_element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Start)
                {
                    Control.TextAlignment = Android.Views.TextAlignment.ViewStart;
                }
                else if (_element.HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center)
                {
                    Control.TextAlignment = Android.Views.TextAlignment.Center;
                }

                if (!_element.HasUnderLine)
                    Control.SetBackgroundColor(Color.Transparent.ToAndroid());

                var intPtrtextViewClass = JNIEnv.FindClass(typeof(TextView));
                var mCursorDrawableResProperty = JNIEnv.GetFieldID(intPtrtextViewClass, "mCursorDrawableRes", "I");
                JNIEnv.SetField(Control.Handle, mCursorDrawableResProperty, Resource.Drawable.myCursor);

                SetTextAlignment();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
            }
        }
        private void SetTextAlignment()
        {
            Control.TextDirection = TextDirection.Locale;
            Control.TextAlignment = TextAlignment.ViewStart;
        }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == ExtEditor.ReturnKeyTypeProperty.PropertyName)
            {
                if (_element != null)
                {
                    UpdateReturnKeyType(_element);
                }
            }
        }
        private void UpdateReturnKeyType(ExtEditor element)
        {
            switch (element.ReturnKeyType)
            {
                case ReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel(element.ReturnKeyType.ToString(), Control.ImeOptions);
                    break;
                case ReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel(element.ReturnKeyType.ToString(), Control.ImeOptions);
                    break;
                case ReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel(element.ReturnKeyType.ToString(), Control.ImeOptions);
                    break;
                case ReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel(element.ReturnKeyType.ToString(), Control.ImeOptions);
                    break;
                case ReturnType.Done:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel(element.ReturnKeyType.ToString(), Control.ImeOptions);
                    break;
                default:
                    Control.ImeOptions = ImeAction.None;
                    Control.SetImeActionLabel(element.ReturnKeyType.ToString(), Control.ImeOptions);
                    break;
            }
        }
    }

}