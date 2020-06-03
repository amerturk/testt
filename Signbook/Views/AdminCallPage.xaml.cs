using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Signbook.Controls;
using Signbook.ViewModels;
using WhiteMvvm.Bases;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AdminCallPage : BaseContentPage
    {
        Dictionary<string, string> valuess = new Dictionary<string, string>();
        Dictionary<string, string> valuessLong = new Dictionary<string, string>();
        Dictionary<string, string> valuessLat = new Dictionary<string, string>();

        public AdminCallPage()
        {
            InitializeComponent();

            //open the admin call page direct 

            string VideoCallPage = "https://storage.googleapis.com/signboo/OmanVidCallMob/callScreen/AppPageDirect.html";

            Browser.OpenAsync(new Uri(VideoCallPage), new BrowserLaunchOptions()
            {
                LaunchMode = BrowserLaunchMode.SystemPreferred,
                TitleMode = BrowserTitleMode.Hide,
                PreferredToolbarColor = System.Drawing.Color.Black,
                PreferredControlColor = System.Drawing.Color.Black
            });




        }




        public static class Check
        {
            public static int CheckOccurrences(string str1, string pattern)
            {
                int count = 0;
                int a = 0;
                while ((a = str1.IndexOf(pattern, a)) != -1)
                {
                    a += pattern.Length;
                    count++;
                }
                return count;
            }
        }
        
       /* async Task<bool> RequestPermission(Plugin.Permissions.Abstractions.Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (permissionStatus != PermissionStatus.Granted)
            {
                if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(permission))
                {
                    return false;
                }
                var result = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                permissionStatus = result[permission];
                return permissionStatus == PermissionStatus.Granted;
            }
            return permissionStatus == PermissionStatus.Granted;
        }*/


        public async void OpenVideoCallPage()
        {
           /*ar isPermissioned = await RequestPermission(Permission.Microphone);
            if (isPermissioned)
            {
                var browser = new VideoCallWebView();
                browser.Source = "https://storage.googleapis.com/signboo/vidcall/TokBox/tokbox.html";
                Content = browser;
            }*/

        }

        public static double DistanceTo(double lat1, double lon1, double lat2, double lon2, char unit = 'K')
        {
            double rlat1 = Math.PI * lat1 / 180;
            double rlat2 = Math.PI * lat2 / 180;
            double theta = lon1 - lon2;
            double rtheta = Math.PI * theta / 180;
            double dist =
                Math.Sin(rlat1) * Math.Sin(rlat2) + Math.Cos(rlat1) *
                Math.Cos(rlat2) * Math.Cos(rtheta);
            dist = Math.Acos(dist);
            dist = dist * 180 / Math.PI;
            dist = dist * 60 * 1.1515;

            switch (unit)
            {
                case 'K': //Kilometers -> default
                    return dist * 1.609344;
                case 'N': //Nautical Miles 
                    return dist * 0.8684;
                case 'M': //Miles
                    return dist;
            }

            return dist;
        }

        protected override void OnDisappearing()
        {
           
        }
    }
}