using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Signbook.Dependency;
using Signbook.Droid.Renders;
using Xamarin.Forms;

[assembly: Dependency(typeof(AppSettingsInterface))]
namespace Signbook.Droid.Renders
{
    public class AppSettingsInterface : IAppSettingsHelper
    {
        public void OpenAppSettings()
        {
            var intent = new Intent(Android.Provider.Settings.ActionApplicationDetailsSettings);
            intent.AddFlags(ActivityFlags.NewTask);
            string package_name = "sign.com.com";
            var uri = Android.Net.Uri.FromParts("package", package_name, null);
            intent.SetData(uri);
            Android.App.Application.Context.StartActivity(intent);
        }
    }
}