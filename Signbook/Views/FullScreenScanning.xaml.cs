using Signbook.Constants;
using Signbook.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.Navigation;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Utilities;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FullScreenScanning : ZXingScannerPage
    {
       // public ICommand CloseCommand { get; set; }
        public FullScreenScanning()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
           // CloseCommand = new Command(OnClose);
        }
        private void OnTapped(object sender, EventArgs e)
        {
            // Do stuff
            
                //NavigationService.NavigateToAsync<NewsMainViewModel>();
            
            Application.Current.MainPage = new NavigationPage(new MainPage());
        }

        public async void Handle_OnScanResult(Result result)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (!string.IsNullOrEmpty(result.Text))
                {
                    string url = GetVideoOrMp3Url(result.Text);
                    if (!string.IsNullOrEmpty(url))
                    {
                        Urls.barcodeUrl = url;
                        if (Device.RuntimePlatform == Device.Android)
                        {
                          /*  var NewsViewModel =  BaseLocator.Instance.Resolve<NewsViewModel>();
                            NewsViewModel.OpenFullScreenPage(url);
                            */
                            var navigationService = BaseLocator.Instance.Resolve<NavigationService>();
                            await navigationService.NavigateToAsync<BarcodeWindowViewModel>();

                        }
                        else
                        {
                            await Xamarin.Essentials.Browser.OpenAsync(new Uri(url), new BrowserLaunchOptions()
                            {
                                TitleMode = BrowserTitleMode.Hide,
                                LaunchMode = BrowserLaunchMode.SystemPreferred
                            }); ;
                        }
                    }
                }
            });
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            IsAnalyzing = true;
            IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            IsScanning = false;
        }
        private string GetVideoOrMp3Url(string barcode)
        {
            string disabilityType = DetemineDisabilityType();
            string Country = DetemineCountry();
            if (!string.IsNullOrEmpty(disabilityType) && !string.IsNullOrEmpty(Country))
            {
                switch (Country)
                {
                    case "Omman":
                        switch (disabilityType)
                        {
                            case "Blind":
                                return Urls.baseScannerUrlToMp3 + "3/" + barcode + ".mp3";
                            case "Deaf":
                                return Urls.baseScannerUrlToMp4 + "3/" + barcode + ".mp4";
                            default:
                                return null;
                        }
                    case "Jordan":
                        switch (disabilityType)
                        {
                            case "Blind":
                                return Urls.baseScannerUrlToMp3 + "1/" + barcode + ".mp3";
                            case "Deaf":
                                return Urls.baseScannerUrlToMp4 + "1/" + barcode + ".mp4";
                            default:
                                return null;
                        }
                    case "Egypt":
                        switch (disabilityType)
                        {
                            case "Blind":
                                return Urls.baseScannerUrlToMp3 + "2/" + barcode + ".mp3";
                            case "Deaf":
                                return Urls.baseScannerUrlToMp4 + "2/" + barcode + ".mp4";
                            default:
                                return null;
                        }
                    default:
                        return null;
                }
            }
            else
            {
                return null;
            }
        }
        private string DetemineDisabilityType()
        {
            if (Application.Current.Properties.ContainsKey("SelectedDisablitiyType"))
            {
                var DisabilityType = Application.Current.Properties["SelectedDisablitiyType"] as string;
                if (DisabilityType != null && !string.IsNullOrEmpty(DisabilityType.ToString()))
                {
                    switch (DisabilityType)
                    {
                        // in this case media will be mp3
                        case "Blind":
                            return "Blind";
                        // in this case media will be mp4
                        case "Deaf":
                            return "Deaf";
                        default:
                            return null;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        private string DetemineCountry()
        {
            if (Application.Current.Properties.ContainsKey("SelectedCountry"))
            {
                var DisabilityType = Application.Current.Properties["SelectedCountry"] as string;
                if (DisabilityType != null && !string.IsNullOrEmpty(DisabilityType.ToString()))
                {
                    switch (DisabilityType)
                    {

                        case "Omman":
                            return "Omman";
                            //PersonImage = "OmanPerson.png";
                            break;
                        case "Jordan":
                            return "Jordan";
                            //PersonImage = "JordanPerson.png";
                            break;
                        case "Egypt":
                            return "Egypt";
                            //PersonImage = "EgyptPerson.png";
                            break;
                        default:
                            return null;
                    }
                }
                return null;
            }
            else
            {
                return null;
            }
        }

    }
}