using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Plugin.FirebasePushNotification;
using UIKit;
using UserNotifications;
using WhiteMvvm.Utilities;

namespace Signbook.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            UIApplication.SharedApplication.CancelAllLocalNotifications();
            //global::Xamarin.Agora.Full.Forms.AgoraServiceIos.Init();
            global::Xamarin.Forms.Forms.Init();
            FirebasePushNotificationManager.Initialize(options, true);
            ZXing.Net.Mobile.Forms.iOS.Platform.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            NSNotificationCenter.DefaultCenter.AddObserver(new NSString("UIWindowDidBecomeHiddenNotification"), HandleAction);
            UNUserNotificationCenter.Current.RequestAuthorization(UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound, (approved, error) =>
            {
                // do something with approved
                // approved will be true if user given permission or false if not
            });
            LoadApplication(new App());
            /*Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(1));
            Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(2));
            Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(3));
            Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(4));*/

            return base.FinishedLaunching(app, options);
        }
        void HandleAction(NSNotification obj)
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }

        public override void RegisteredForRemoteNotifications(UIApplication application, NSData deviceToken)
        {

            FirebasePushNotificationManager.DidRegisterRemoteNotifications(deviceToken);
        }
        public override void FailedToRegisterForRemoteNotifications(UIApplication application, NSError error)
        {
            FirebasePushNotificationManager.RemoteNotificationRegistrationFailed(error);
        }
        // To receive notifications in foregroung on iOS 9 and below.
        // To receive notifications in background in any iOS version
        public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        {
            // If you are receiving a notification message while your app is in the background,
            // this callback will not be fired 'till the user taps on the notification launching the application.
            // If you disable method swizzling, you'll need to call this method. 
            // This lets FCM track message delivery and analytics, which is performed
            // automatically with method swizzling enabled.
            /*Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(1));
            Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(2));
            Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(3));
            Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(4));*/

            Xamarin.Essentials.Vibration.Vibrate(TimeSpan.FromSeconds(1));
            FirebasePushNotificationManager.DidReceiveMessage(userInfo);
            System.Console.WriteLine(userInfo);
            completionHandler(UIBackgroundFetchResult.NewData);
        }

    }
}
