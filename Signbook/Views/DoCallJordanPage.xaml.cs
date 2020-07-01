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
    public partial class DoCallJordanPage : BaseContentPage
    {
        Dictionary<string, string> valuess = new Dictionary<string, string>();
        Dictionary<string, string> valuessLong = new Dictionary<string, string>();
        Dictionary<string, string> valuessLat = new Dictionary<string, string>();
        Dictionary<string, string> CompaniesLocations = new Dictionary<string, string>();
        Dictionary<string, string> CompaniesImages = new Dictionary<string, string>();
        


        public DoCallJordanPage()
        {
            InitializeComponent();

            /* we have to build a dectionary and a list 
             where we will add the list items to the Xamarin picker and we will
             get the values from the dectionary 
             */
            FillcompaniesDD();



        }


        public void FillcompaniesDD()
        {


            var getCompaniesURL = "http://jodata.signbook.co/api/SitesApis/APPGetAllCompaniesFull?PageNo=1&PageSize=10&SearchTerm=&Orderby=date";
                                                     

            var companiesList = new List<string>();
                 


            using (var clientt = new WebClient())
            {

                string result = clientt.DownloadString(getCompaniesURL);



                JObject SerialaizedJSON = JObject.Parse(result);
                string CompaniesString = SerialaizedJSON["Table"].ToString();


                int NumberoOfCompanies = Check.CheckOccurrences(CompaniesString.ToLower(), "companyid");

                for (int x = 0; x < NumberoOfCompanies; x++)
                {                    
                    
                    valuess.Add(SerialaizedJSON["Table"][x]["CompanyID"].ToString(), SerialaizedJSON["Table"][x]["CompanyName"].ToString());
                    valuessLong.Add(SerialaizedJSON["Table"][x]["CompanyID"].ToString(), SerialaizedJSON["Table"][x]["long"].ToString());
                    valuessLat.Add(SerialaizedJSON["Table"][x]["CompanyID"].ToString(), SerialaizedJSON["Table"][x]["lat"].ToString());
                    CompaniesImages.Add(SerialaizedJSON["Table"][x]["CompanyName"].ToString(), SerialaizedJSON["Table"][x]["ImageURL"].ToString());
                    CompaniesLocations.Add(SerialaizedJSON["Table"][x]["CompanyName"].ToString(), SerialaizedJSON["Table"][x]["OpenArea"].ToString());
                    companiesList.Add(SerialaizedJSON["Table"][x]["CompanyName"].ToString());
                }
                //var Npicker = picker;
                picker.ItemsSource = companiesList;

            }

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
        async void SelectionChanged(object sender, EventArgs args)
        { 
            var SelectedCompany = picker.SelectedIndex;
            var SelectedCompanyz = picker.SelectedItem;
            var SelectedCompanyImage = CompaniesImages.FirstOrDefault(x => x.Key == SelectedCompanyz).Value;

            CompanyImage.Source = new UriImageSource
            {
                Uri = new Uri(SelectedCompanyImage)
            };

            callBtn.IsEnabled = true;

        }
        async void DoVideoCall(object sender, EventArgs args)
        {
            var SelectedCompany = picker.SelectedItem;

            var SelectedCompanyOpenArea = CompaniesLocations.FirstOrDefault(x => x.Key == SelectedCompany).Value;
            var SelectedCompanyID = valuess.FirstOrDefault(x => x.Value == SelectedCompany).Key;

            var SelectedCompanyLong = valuessLong[SelectedCompanyID];
            var SelectedCompanyLat = valuessLat[SelectedCompanyID];

            if (SelectedCompanyOpenArea == "1")
            {
                // it is allowed to do the call without checking the location 
                string UserName = Application.Current.Properties["UserName"].ToString();
                string UserId = Application.Current.Properties["UserID"].ToString();
                string CompanyID = SelectedCompanyID.ToString();
                string CompanyName = SelectedCompany.ToString();


                string VideoCallPage = string.Format("https://storage.googleapis.com/signboo/JorVidCallMob/callScreen/AppPage.html?UserID={0}&UserName={1}&CompanyID={2}&CompanyName={3}&", UserId, UserName, CompanyID, CompanyName);

                Browser.OpenAsync(new Uri(VideoCallPage), new BrowserLaunchOptions()
                {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Hide,
                    PreferredToolbarColor = System.Drawing.Color.Black,
                    PreferredControlColor = System.Drawing.Color.Black
                });
            }
            else
            {
                //here we have to check the location and then do the call 
                try
                {
                    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location != null)
                    {
                        // Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                        // get the distance from your current location comparing to company location
                        double distanceFromCurrentLocation = DistanceTo(location.Latitude, location.Longitude, Convert.ToDouble(SelectedCompanyLat), Convert.ToDouble(SelectedCompanyLong));
                        //double distanceFromCurrentLocation = DistanceTo(location.Latitude, location.Longitude, 31.9564843, 35.9040618);
                        if (distanceFromCurrentLocation > 0.1)
                        {
                            // in this case the distance is more than 100 meter
                            //so the call is not allowed 
                            ErrorLocation.Text = "انت خارج نطاق المؤسسة";
                        }
                        else
                        {
                            // the user is too close he can do the call
                            //here we have to get all needed information for call, user ID, Company ID, User Name and Company Name 

                            //Application.Current.Properties[""];
                            string UserName = Application.Current.Properties["UserName"].ToString();
                            string UserId = Application.Current.Properties["UserID"].ToString();
                            string CompanyID = SelectedCompanyID.ToString();
                            string CompanyName = SelectedCompany.ToString();


                            string VideoCallPage = string.Format("https://storage.googleapis.com/signboo/JorVidCallMob/callScreen/AppPage.html?UserID={0}&UserName={1}&CompanyID={2}&CompanyName={3}&", UserId, UserName, CompanyID, CompanyName);

                            Browser.OpenAsync(new Uri(VideoCallPage), new BrowserLaunchOptions()
                            {
                                LaunchMode = BrowserLaunchMode.SystemPreferred,
                                TitleMode = BrowserTitleMode.Hide,
                                PreferredToolbarColor = System.Drawing.Color.Black,
                                PreferredControlColor = System.Drawing.Color.Black
                            });
                            #region oldcode

                            /* var isPermissioned = await RequestPermission(Plugin.Permissions.Abstractions.Permission.Microphone);
                             if (isPermissioned)
                             {
                                 var browser = new WebView();
                                 browser.Source = "https://storage.googleapis.com/signboo/OmanVidCallMob/callScreen/AppPage.html";
                                 Content = browser;
                             }*/
                            /* var browser = new WebView();
                             browser.Source = "https://storage.googleapis.com/signboo/OmanVidCallMob/callScreen/AppPage.html";
                             Content = browser;*/

                            /* Browser.OpenAsync(new Uri("https://storage.googleapis.com/signboo/OmanVidCallMob/callScreen/AppPage.html"),new BrowserLaunchOptions()
                               {
                                     LaunchMode = BrowserLaunchMode.SystemPreferred,
                                     TitleMode = BrowserTitleMode.Hide,
                                     PreferredToolbarColor = System.Drawing.Color.Black,
                                     PreferredControlColor = System.Drawing.Color.Black
                                });*/


                            //Device.OpenUri("https://storage.googleapis.com/signboo/vidcall/TokBox/tokbox.html");
                            #endregion

                        }
                    }
                }
                catch (FeatureNotSupportedException e)
                {
                    // Handle not supported on device exception
                }

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
           /* var isPermissioned = await RequestPermission(Permission.Microphone);
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