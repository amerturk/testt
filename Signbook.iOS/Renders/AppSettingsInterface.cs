using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Signbook.Dependency;
using Signbook.iOS.Renders;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppSettingsInterface))]
namespace Signbook.iOS.Renders
{
    public class AppSettingsInterface : IAppSettingsHelper
    {
        public void OpenAppSettings()
        {
            var url = new NSUrl($"app-settings:");
            UIApplication.SharedApplication.OpenUrl(url);
        }
    }
}