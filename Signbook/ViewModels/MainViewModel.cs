using Signbook.Dependency;
using Signbook.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using Xamarin.Forms;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Net;

namespace Signbook.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Color _barcodeBackgroundColor;
        private Color _settingTextColor;
        private Color _barcodeTextColor;
        private Color _settingBackgroundColor;
        private Color _newsBackgroundColor;
        private Color _newsTextColor;
        private Color _vCallBackgroundColor;
        private Color _vCallTextColor;

        private bool _isDeaf;
        private bool _isOmani;
        #region Properties
        public bool IsDeaf
        {
            get { return _isDeaf; }
            set { _isDeaf = value; OnPropertyChanged(); }
        }
        public bool IsOmani
        {
            get { return _isOmani; }
            set { _isOmani = value; OnPropertyChanged(); }
        }
        public Color SettingBackgroundColor
        {
            get { return _settingBackgroundColor; }
            set { _settingBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color SettingTextColor
        {
            get { return _settingTextColor; }
            set { _settingTextColor = value; OnPropertyChanged(); }
        }
        public Color BarcodeBackgroundColor
        {
            get { return _barcodeBackgroundColor; }
            set { _barcodeBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color BarcodeTextColor
        {
            get { return _barcodeTextColor; }
            set { _barcodeTextColor = value; OnPropertyChanged(); }
        }
        public Color NewsBackgroundColor
        {
            get { return _newsBackgroundColor; }
            set { _newsBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color NewsTextColor
        {
            get { return _newsTextColor; }
            set { _newsTextColor = value; OnPropertyChanged(); }
        }

        public Color VcallBackgroundColor
        {
            get { return _vCallBackgroundColor; }
            set { _vCallBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color VcallTextColor
        {
            get { return _vCallTextColor; }
            set { _vCallTextColor = value; OnPropertyChanged(); }
        }
        #endregion
        #region Commands
        public ICommand SettingSelectedCommand { get; set; }
        public ICommand BarcodeSelectedCommand { get; set; }
        public ICommand NewsSelectedCommand { get; set; }
        public ICommand RoomClickCommand { get; set; }
        public ICommand VideoWebClickCommand { get; set; }
        public ICommand LogoutCommand { get; set; }

        
        #endregion
        #region Page Events
        public MainViewModel()
        {
            SettingSelectedCommand = new Command(SettingSelected);
            BarcodeSelectedCommand = new Command(BarcodeSelected);
            NewsSelectedCommand = new Command(NewsSelected);
            RoomClickCommand = new Command(OnRoomClick);
            VideoWebClickCommand = new Command(OnVideoWebClick);
            LogoutCommand = new Command(OnLogoutClick);
            
        }


        
            private async void OnLogoutClick(object obj)
            {
            // we have to clear the user name and user id
            try
            {
                Application.Current.Properties.Remove("UserName");
            }
            catch (Exception e)
            { }
            try
            {
                Application.Current.Properties.Remove("UserID");
            }
            catch (Exception e)
            { }

        }
            private async void OnVideoWebClick(object obj)
        {

            VcallBackgroundColor = Color.FromHex("#384B6C");
            VcallTextColor = Color.White;
            await Task.Delay(200);

            //here we have to check if he is logged in or not !
            //Save Selected Country
            if (Application.Current.Properties.ContainsKey("UserName") && Application.Current.Properties.ContainsKey("UserID"))
            {
                // he is logged in already and it should redirect to the call page 
                if (Application.Current.Properties["UserName"] != null && Application.Current.Properties["UserID"] != null)
                {
                    if (Application.Current.Properties["UserName"] != "" && Application.Current.Properties["UserID"] != "")
                    {
                        // he is logged in and the values are not null

                        //check what is the user country 
                        var Country = Application.Current.Properties["SelectedCountry"] as string;
                        if (Country != null && !string.IsNullOrEmpty(Country.ToString()))
                        {
                            
                            if (Country == "Jordan")
                            {
                                //Jordanian User
                                NavigationService.NavigateToAsync<MainCallJordanViewModel>();
                            }
                            else {
                                //Omani User
                                NavigationService.NavigateToAsync<MainCallViewModel>();
                            }



                        }                           
                        VcallBackgroundColor = Color.White;
                        VcallTextColor = Color.Gray;

                    }
                }
                
            }
            else {
                //he is not logged in 
                //we have to ask him to signup
                  //NavigationService.NavigateToAsync<MainCallViewModel>();
                NavigationService.NavigateToAsync<LoginViewModel>();
                VcallBackgroundColor = Color.White;
                VcallTextColor = Color.Gray;

            }


            /*var isPermissioned = await RequestPermission(Permission.Microphone);
            if (isPermissioned)
            {
                await NavigationService.NavigateToAsync<VideoWebViewModel>();
            }*/

            

        }

        async Task<bool> RequestPermission(Permission permission)
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
        }

        
        private void OnRoomClick(object obj)
        {
            NavigationService.NavigateToAsync<RoomViewModel>();
        }

        protected override Task OnAppearing()
        {
            SetPageSetting();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IStatusBar>().ShowStatusBar();
            DependencyService.Get<IStatusBarStyleManager>().SetLightTheme();
            return base.OnAppearing();
        }
        protected override bool HandleBackButton()
        {
            NavigationService.Navigation.PopAsync();
            return base.HandleBackButton();
        }

        #endregion
        #region Command implementation
        private async void SettingSelected(object obj)
        {
            SettingBackgroundColor = Color.FromHex("#384B6C");
            SettingTextColor = Color.White;
            await Task.Delay(200);
            NavigationService.NavigateToAsync<SettingViewModel>();
            SettingBackgroundColor = Color.White;
            SettingTextColor = Color.Gray;

        }

        private async void BarcodeSelected(object obj)
        {
            BarcodeBackgroundColor = Color.FromHex("#384B6C");
            BarcodeTextColor = Color.White;
            await Task.Delay(200);
            var page = new FullScreenScanning();
            NavigationPage.SetHasNavigationBar(page, false);
            App.Current.MainPage.Navigation.PushAsync(page);
            BarcodeBackgroundColor = Color.White;
            BarcodeTextColor = Color.Gray;

        }

        private async void NewsSelected(object obj)
        {
            NewsBackgroundColor = Color.FromHex("#384B6C");
            NewsTextColor = Color.White;
            await Task.Delay(200);
            NavigationService.NavigateToAsync<NewsMainViewModel>();
            NewsBackgroundColor = Color.White;
            NewsTextColor = Color.Gray;
        }
        #endregion
        #region Methods
        public void SetPageSetting()
        {
            
            if(IsUserDeaf())
            {
                IsDeaf = true;
            }
            else
            {
                IsDeaf = false;
            }
            if (IsUserOmani())
            {
                _isOmani = true;
            }
            else
            {
                _isOmani = false;
            }
        }
        private bool IsUserDeaf()
        {
            if (Application.Current.Properties.ContainsKey("SelectedDisablitiyType"))
            {
                var DisabilityType = Application.Current.Properties["SelectedDisablitiyType"] as string;
                if (DisabilityType != null && !string.IsNullOrEmpty(DisabilityType.ToString()))
                {
                    switch (DisabilityType)
                    {
                        case "Blind":
                            return false;
                        case "Deaf":
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool IsUserOmani()
        {
            //Application.Current.Properties.Add("SelectedCountry", "Egypt");
            if (Application.Current.Properties.ContainsKey("SelectedCountry"))
            {
                var Country = Application.Current.Properties["SelectedCountry"] as string;
                if (Country != null && !string.IsNullOrEmpty(Country.ToString()))
                {
                    switch (Country)
                    {
                        case "Omman":
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }
        #endregion


        public void setUserToken(string userTkn)
        {

            if (Application.Current.Properties.ContainsKey("UserID"))
            {
                //the key is existis we have only to update the key
                Application.Current.Properties["UserToken"] = userTkn;
                string UserId = Application.Current.Properties["UserID"].ToString();
                //string UpdateUserToken = "http://omnapp.signbook.co/api/SitesApis/InsertUserToken?UserID=4A9A8FE7-5A32-4ED9-99AB-E4720EEDB2CA&UserToken=4A9A8FE7-5A32-4ED9-99AB-E4720EEDB2CA&"
                string UpdateUserToken = string.Format("http://omnapp.signbook.co/api/SitesApis/InsertUserToken?UserID={0}&UserToken={1}", UserId, userTkn);


                using (var clientt = new WebClient())
                {

                    string result = clientt.DownloadString(UpdateUserToken);

                    //cases data exist
                    if (result.ToLower().Contains("error"))
                    {
                        // we have to update the user 

                    }


                }
            }
            else {
                // he is not logged in user we have just to capture the usertoken and add it once the user login
                if (Application.Current.Properties.ContainsKey("UserToken"))
                {
                    Application.Current.Properties["UserToken"] = userTkn;
                }
                else {
                    //the key is not added before
                    Application.Current.Properties.Add("UserToken", userTkn);
                }
            }
                       
            
        }
    }
}
