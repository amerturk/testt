using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Signbook.Dependency;
using Signbook.iOS.Renders;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarStyleManagerIOS))]
namespace Signbook.iOS.Renders
{
    public class StatusBarStyleManagerIOS : IStatusBarStyleManager
    {
        public void SetDarkTheme()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {

                    bool systemVersion = UIDevice.CurrentDevice.CheckSystemVersion(13, 0);
                    if (systemVersion)
                    {
                        UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                        statusBar.BackgroundColor = UIColor.FromRGB(30, 30, 30);
                        statusBar.TintColor = UIColor.White;
                        UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;
                        UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
                    }
                    else
                    {
                        UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.BlackOpaque, false);
                        GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
                        var statusBarView = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                        if (statusBarView.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                        {
                            statusBarView.BackgroundColor = UIColor.FromRGB(30, 30, 30);
                            statusBarView.TintColor = UIColor.White;
                        }
                        UIApplication.SharedApplication.SetStatusBarHidden(false, UIKit.UIStatusBarAnimation.None);
                    }
                }
                catch (Exception exception)
                {
                    //var properties = new Dictionary<string, string>
                    //{
                    //       { "SetDarkTheme", "StatusBarStyleManagerIOS" }
                    //};
                    //Crashes.TrackError(exception, properties);
                }
            });
        }

        public void SetLightTheme()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    bool systemVersion = UIDevice.CurrentDevice.CheckSystemVersion(13, 0);
                    if (systemVersion)

                    {
                        UIView statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
                        statusBar.BackgroundColor = UIColor.White;//FromRGB(237, 237, 237);
                        statusBar.TintColor = UIColor.Black;    
                        UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.BlackOpaque;
                        UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
                    }
                    else
                    {
                        UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, false);
                        GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
                        var statusBarView = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
                        if (statusBarView.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
                        {
                            statusBarView.BackgroundColor = UIColor.White;//FromRGB(237, 237, 237);
                            statusBarView.TintColor = UIColor.Black;
                        }
                    }
                }
                catch (Exception exception)
                {
                    //var properties = new Dictionary<string, string>
                    //{
                    //       { "SetLightTheme", "StatusBarStyleManagerIOS" }
                    //};
                    //Crashes.TrackError(exception, properties);

                }
            });
        }
        UIViewController GetCurrentViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;
            return vc;
        }
    }

}