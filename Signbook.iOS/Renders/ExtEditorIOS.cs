

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Cirrious.FluentLayouts.Touch;
using Firebase.Core;
using Foundation;

using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Microsoft.AppCenter.Crashes;
using Signbook.Controls;
using Signbook.iOS.Renders;
using Signbook.Services.Localization;

[assembly: ExportRenderer(typeof(ExtEditor), typeof(ExtEditorIOS))]

namespace Signbook.iOS.Renders
{
    public class ExtEditorIOS : EditorRenderer
    {
        private UILabel _placeholderLabel;
        ExtEditor _element;

        protected override void OnElementChanged(ElementChangedEventArgs<Editor> e)
        {
            try
            {
                base.OnElementChanged(e);

                if (Element == null)
                    return;

                _element = (ExtEditor)Element;
                CreatePlaceholderLabel(_element, Control);
                UpdateReturnKeyType(_element);
                Control.Ended += OnEnded;
                Control.Changed += OnChanged;
                SetTextAlignment();
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                {
                    { "ExtEditorIOS", "OnElementChanged" },
                };
                Crashes.TrackError(exception, properties);
            }
        }
        private void SetTextAlignment()
        {
            if (LocalizationService.Current.GetCurrentLanguage() == Language.Arabic)
            {
                Control.TextAlignment = UITextAlignment.Right;
            }
            else
            {
                Control.TextAlignment = UITextAlignment.Left;
            }
        }
        private void UpdateReturnKeyType(ExtEditor entryEx)
        {
            switch (entryEx.ReturnKeyType)
            {
                case ReturnType.Go:
                    Control.ReturnKeyType = UIReturnKeyType.Go;
                    break;
                case ReturnType.Next:
                    Control.ReturnKeyType = UIReturnKeyType.Next;
                    break;
                case ReturnType.Send:
                    Control.ReturnKeyType = UIReturnKeyType.Send;
                    break;
                case ReturnType.Search:
                    Control.ReturnKeyType = UIReturnKeyType.Search;
                    break;
                case ReturnType.Done:
                    Control.ReturnKeyType = UIReturnKeyType.Done;
                    break;
                default:
                    Control.ReturnKeyType = UIReturnKeyType.Default;
                    break;
            }
        }
        private void CreatePlaceholderLabel(ExtEditor element, UITextView parent)
        {
            _placeholderLabel = new UILabel
            {
                Text = element.Placeholder,
                TextColor = element.PlaceholderColor.ToUIColor(),
                BackgroundColor = UIColor.Clear,
                Font = UIFont.FromName(element.FontFamily, (nfloat)element.FontSize),
            };

            _placeholderLabel.SizeToFit();
            parent.AddSubview(_placeholderLabel);
            parent.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            var attributedString = parent.AttributedText.Size.Width;

            var size = Xamarin.Essentials.DeviceDisplay.MainDisplayInfo.Width;
            nfloat.TryParse(((size) - ((element.Margin.Right * 3) + element.Margin.Left)).ToString(), out var elementWidth);

            if (LocalizationService.Current.GetCurrentLanguage() == Language.English)
            {
                parent.AddConstraints(_placeholderLabel.WithSameTop(parent), _placeholderLabel.AtRightOf(parent, -elementWidth));
                if (size <= 320)
                {
                    _placeholderLabel.Font = UIFont.FromName(element.FontFamily, (nfloat)(element.FontSize * 0.7));
                }
            }
            else
            {
                parent.AddConstraints(_placeholderLabel.WithSameTop(parent), _placeholderLabel.AtLeftOf(parent, (nfloat)element.Margin.Left + 10));
            }
            parent.LayoutIfNeeded();

            _placeholderLabel.Hidden = parent.HasText;

        }

        private void OnEnded(object sender, EventArgs args)
        {
            try
            {
                if (!((UITextView)sender).HasText && _placeholderLabel != null)
                    _placeholderLabel.Hidden = false;
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                {
                    { "ExtEditorIOS", "OnEnded" },
                };
                // Crashes.TrackError(exception, properties);
            }
        }

        private void OnChanged(object sender, EventArgs args)
        {
            if (_placeholderLabel != null)
                _placeholderLabel.Hidden = ((UITextView)sender).HasText;
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Control.Ended -= OnEnded;
                Control.Changed -= OnChanged;

                _placeholderLabel?.Dispose();
                _placeholderLabel = null;
            }

            base.Dispose(disposing);

        }

    }

}