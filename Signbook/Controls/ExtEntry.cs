using Signbook.Services.Localization;
using Xamarin.Forms;

namespace Signbook.Controls
{
    public class ExtEntry : Entry
    {
        public ExtEntry()
        {
            FlowDirection = LocalizationService.Current.AppFlowDirection;
            this.HorizontalTextAlignment = LocalizationService.Current.GetCurrentLanguage() == Language.Arabic
                ? TextAlignment.End
                : TextAlignment.Start;
        }

        public static readonly BindableProperty HasBorderProperty = BindableProperty.Create(
            nameof(HasBorder),
            typeof(bool),
            typeof(ExtEntry),
            true);
        /// <summary>
        /// Gets or sets the font attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public bool HasBorder
        {
            get => (bool)GetValue(HasBorderProperty);
            set => SetValue(HasBorderProperty, value);
        }
        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
         nameof(BorderColor),
         typeof(string),
         typeof(ExtEntry),
         //lightgray
         null);
        /// <summary>
        /// Gets or sets the PlaceholderColor attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public string BorderColor
        {
            get => (string)GetValue(BorderColorProperty);
            set => SetValue(BorderColorProperty, value);
        }

        public static readonly BindableProperty PaddingProperty = BindableProperty.Create(
            nameof(Padding),
            typeof(Thickness),
            typeof(ExtEntry),
            null);
        /// <summary>
        /// Gets or sets the PlaceholderColor attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }

    }
}
