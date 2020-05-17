using Microsoft.AppCenter.Crashes;
using Signbook.Resources;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WhiteMvvm.Bases;
using WhiteMvvm.Utilities;
using Xamarin.Forms;

namespace Signbook.Services.Localization
{
    public class LocalizationService : NotifiedObject
    {
        private static readonly Lazy<LocalizationService> Lazy = new Lazy<LocalizationService>(() => new LocalizationService());
        public static LocalizationService Current => Lazy.Value;
        private LocalizationService()
        {
        }
        private FlowDirection _appFlowDirection;
        private CultureInfo _cultureInfo;
        public CultureInfo CultureInfo
        {
            get => _cultureInfo;
            set
            {
                _cultureInfo = value; OnPropertyChanged();
            }
        }
        public FlowDirection AppFlowDirection
        {
            get => _appFlowDirection;
            set
            {
                _appFlowDirection = value;
                OnPropertyChanged();
            }
        }
        public Language GetCurrentLanguage()
        {
            CultureInfo ci = Current.CultureInfo;
            if (ci == null)
            {
                return Language.Arabic;
            }

            if (ci.ToString().Contains("ar") || ci.ToString().Contains("Ar"))
            {
                return Language.Arabic;
            }
            if (ci.ToString().Contains("en") || ci.ToString().Contains("En"))
            {
                return Language.Arabic;
            }
            return Language.Arabic;
        }
        public async void SetCurrentLanguage(Language language, bool isRefreshing = true)
        {
            try
            {
                string languageName = "";
                if (language == Language.English)
                {
                    languageName = "en";
                    CultureInfo englishCulture = new CultureInfo(languageName);
                    Current.CultureInfo = englishCulture;
                    Current.AppFlowDirection = FlowDirection.LeftToRight;
                    AppResource.Culture = englishCulture;

                    if (Application.Current.Properties.ContainsKey("CurrentLanguage"))
                    {
                        Application.Current.Properties.Remove("CurrentLanguage");
                        Application.Current.Properties.Add("CurrentLanguage", "Arabic");
                        await Application.Current.SavePropertiesAsync();

                    }
                    else
                    {
                        Application.Current.Properties.Add("CurrentLanguage", "Arabic");
                        await Application.Current.SavePropertiesAsync();

                    }
                }
                else if (language == Language.Arabic)
                {
                    languageName = "ar";
                    CultureInfo arabicCulture = new System.Globalization.CultureInfo(languageName);
                    Current.CultureInfo = arabicCulture;
                    Current.AppFlowDirection = FlowDirection.RightToLeft;
                    AppResource.Culture = arabicCulture;
                    if (Application.Current.Properties.ContainsKey("CurrentLanguage"))
                    {
                        Application.Current.Properties.Remove("CurrentLanguage");
                        Application.Current.Properties.Add("CurrentLanguage", "Arabic");
                        await Application.Current.SavePropertiesAsync();
                    }
                    else
                    {
                        Application.Current.Properties.Add("CurrentLanguage", "Arabic");
                        await Application.Current.SavePropertiesAsync();
                    }
                }
                await Application.Current.SavePropertiesAsync();
                if (isRefreshing)
                {
                    BaseLocator.Instance.RefreshLocator();
                }
                WhiteMvvm.Configuration.ConfigurationManager.Current.LoadingDisplay = AppResource.Loading;
                
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                {
                   { "SetCurrentLanguage", "LocalizationService" }
                };
                Crashes.TrackError(exception, properties);
            }
        }
    }

}
