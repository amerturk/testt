using Signbook.Services.Localization;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Signbook.Constants
{
    public static class Fonts
    {
        private const string ArabicFontNameBold = "Tajawal-Medium";
        private const string EnglishFontNameBold = "OpenSans-Bold";

        private const string ArabicFontNameRegular = "Tajawal-Light";
        private const string EnglishFontNameRegular = "OpenSans-Regular";

        /// <summary>
        /// Method to get font bold depend on language
        /// </summary>
        public static string FontBold
        {
            get
            {
                if (LocalizationService.Current.GetCurrentLanguage() == Language.Arabic)
                {
                    return Device.RuntimePlatform == Device.Android ? $"{ArabicFontNameBold}.ttf#{ArabicFontNameBold}" : $"Tajawal-Medium";
                }
                return Device.RuntimePlatform == Device.Android ? $"{EnglishFontNameBold}.ttf#{EnglishFontNameBold}" : $"{EnglishFontNameBold}";
            }
        }
        /// <summary>
        /// Method to get arabic font bold to use in intro page
        /// </summary>
        public static string FontBoldArabic => Device.RuntimePlatform == Device.Android ? $"{ArabicFontNameBold}.ttf#{ArabicFontNameBold}" : $"Tajawal-Medium";

        /// <summary>
        /// Method to get font regular depend on language
        /// </summary>
        public static string FontRegular
        {
            get
            {
                if (LocalizationService.Current.GetCurrentLanguage() == Language.Arabic)
                {
                    return Device.RuntimePlatform == Device.Android ? $"{ArabicFontNameRegular}.ttf#{ArabicFontNameRegular}" : $"Tajawal-Light";
                }
                return Device.RuntimePlatform == Device.Android ? $"{EnglishFontNameRegular}.ttf#{EnglishFontNameRegular}" : $"{EnglishFontNameRegular}";
            }
        }
    }
}
