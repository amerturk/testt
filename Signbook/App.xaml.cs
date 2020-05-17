using Signbook.Dependency;
using Signbook.Models;
using Signbook.Resources;
using Signbook.Services.Localization;
using Signbook.ViewModels;
using Signbook.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WhiteMvvm.Bases;
using WhiteMvvm.Configuration;
using WhiteMvvm.Services.DeviceUtilities;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Plugin.FirebasePushNotification;
using WhiteMvvm.Services.Navigation;
//using UIKit;


//[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Signbook
{
    public partial class App : WhiteMvvm.WhiteApplication
    {
        public App()
        {
            InitializeComponent();
            SetUpConfiguration();
            StartNavigation();
           // Xamarin.Forms.Application.Current.On<Xamarin.Forms.PlatformConfiguration.Android>().UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
            

        }
        public interface ISettingsService
        {
            void OpenSettings();
        }
        private static void StartNavigation()
        {
            var preferences = BaseLocator.Instance.Resolve<IPreferences>();
            var isFirstTime = preferences.Get("IsFirstTime", null);
            LocalizationService.Current.SetCurrentLanguage(Language.Arabic);
            if (isFirstTime != null && isFirstTime == "True")
            {
                var lang = Current.Properties["CurrentLanguage"] as string;
                SetHomePage<MainViewModel>();
            }
            else
            {                
                SetHomePage<CountriesViewModel>();
            }
        }
        private static void SetUpConfiguration()
        {
            Locator.Init();
            ConfigurationManager.Current.UseBaseIndicator = true;
            ConfigurationManager.Current.IndicatorMaskType = Acr.UserDialogs.MaskType.Black;
            ConfigurationManager.Current.LoadingDisplay = AppResource.Loading;
        }
        protected override void OnStart()
        {
            // Handle when your app starts
            CrossFirebasePushNotification.Current.RegisterForPushNotifications();
            CrossFirebasePushNotification.Current.Subscribe("general");
            var topics = CrossFirebasePushNotification.Current.SubscribedTopics;
            CrossFirebasePushNotification.Current.OnTokenRefresh += (sender, arg) =>
            {
                var token = arg.Token;
                // to get the user token everytime the token changes
                MainViewModel mainviewObj = new MainViewModel();
                mainviewObj.setUserToken(token.ToString());

            };
            CrossFirebasePushNotification.Current.OnNotificationOpened += Current_OnNotificationOpened;
            CrossFirebasePushNotification.Current.OnNotificationReceived += Current_OnNotificationReceived;
            
        }

        private void Current_OnNotificationOpened(object source, Plugin.FirebasePushNotification.Abstractions.FirebasePushNotificationResponseEventArgs e)
        {
            try
            {
                if (e.Data["key"].ToString() == "normal")
                {

                }
                else {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        var navigation = BaseLocator.Instance.Resolve<NavigationService>();
                        navigation.NavigateToAsync<AdminCallViewModel>();
                    });
                }
            }
            catch (Exception) {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var navigation = BaseLocator.Instance.Resolve<NavigationService>();
                    navigation.NavigateToAsync<AdminCallViewModel>();
                });
            }
           
        }

        private void Current_OnNotificationReceived(object source, Plugin.FirebasePushNotification.Abstractions.FirebasePushNotificationDataEventArgs e)
        {
            Console.WriteLine("Notification Received");
           // Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(5));
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
            if (Device.RuntimePlatform == Device.iOS)
            {
                //iOS stuff
                // UIApplication.SharedApplication.CancelAllLocalNotifications();
            }
            else if (Device.RuntimePlatform == Device.Android)
            {
               
            }
            
        }
    }
}
