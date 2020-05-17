

using Signbook.Services.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Signbook.Controls
{
    public class ExtEditor : Editor
    {
        /// <summary>
        /// The has under line property.
        /// </summary>
        public static readonly BindableProperty HorizontalTextAlignmentProperty = BindableProperty.Create(
            "HorizontalTextAlignment",
            typeof(TextAlignment),
            typeof(ExtEditor),
            TextAlignment.Start);
        /// <summary>
        /// Gets or sets the font attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public TextAlignment HorizontalTextAlignment
        {
            get => (TextAlignment)GetValue(HorizontalTextAlignmentProperty);
            set => SetValue(HorizontalTextAlignmentProperty, value);
        }
        /// <summary>
        /// The has under line property.
        /// </summary>
        public static readonly BindableProperty HasUnderLineProperty = BindableProperty.Create(
            "HasUnderLine",
            typeof(bool),
            typeof(ExtEditor),
            true);
        /// <summary>
        /// Gets or sets the font attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public bool HasUnderLine
        {
            get => (bool)GetValue(HasUnderLineProperty);
            set => SetValue(HasUnderLineProperty, value);
        }
        /// <summary>
        /// The Placeholder attributes property.
        /// </summary>
        public new static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(
            nameof(Placeholder),
            typeof(string),
            typeof(ExtEditor),
            string.Empty);
        /// <summary>
        /// Gets or sets the Placeholder attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public new string Placeholder
        {
            get => (string)GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        /// <summary>
        /// The PlaceholderColor attributes property.
        /// </summary>
        public new static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(
            nameof(PlaceholderColor),
            typeof(Color),
            typeof(ExtEditor),
            Color.Gray);
        /// <summary>
        /// Gets or sets the PlaceholderColor attributes.
        /// </summary>
        /// <value>The font attributes.</value>
        public new Color PlaceholderColor
        {
            get => (Color)GetValue(PlaceholderColorProperty);
            set => SetValue(PlaceholderColorProperty, value);
        }


        public static readonly BindableProperty ReturnKeyTypeProperty = BindableProperty.Create(
                    propertyName: nameof(ReturnKeyType),
                    returnType: typeof(ReturnType),
                    declaringType: typeof(ExtEditor),
                    defaultValue: ReturnType.Done);

        public ReturnType ReturnKeyType
        {
            get { return (ReturnType)GetValue(ReturnKeyTypeProperty); }
            set { SetValue(ReturnKeyTypeProperty, value); }
        }

        public ExtEditor()
        {
            this.HorizontalTextAlignment = LocalizationService.Current.GetCurrentLanguage() == Language.Arabic
                ? TextAlignment.End
                : TextAlignment.Start;
        }
    }

}
