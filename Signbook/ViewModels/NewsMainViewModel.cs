using Signbook.Constants;
using Signbook.Services.News;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.DeviceUtilities;
using WhiteMvvm.Utilities;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Signbook.ViewModels
{
    public class NewsMainViewModel : BaseViewModel
    {
        #region Feilds
        bool isNotFirst;
        private Color _localNewsTextColor;
        private Color _internationalNewsTextColor;
        private Color _thirdNewsTextColor;
        private bool _localNewsLineVisibility;
        private bool _internationalNewsLineVisibility;
        private bool _thirdNewsLineVisibility;
        MockNewsService service;
        private IBrowser _browser;
        private INewsService _newsService;
        private bool _isDeaf;
        private Color _localNewsBackgroundColor;
        private Color _internationalNewsBackgroundColor;
        private Color _thirdNewsBackgroundColor;
        private string thirdNewsGIF;
        private string internationalNewsGIF;
        private string localNewsGIF;
        #endregion
        #region Properties
        public bool IsDeaf
        {
            get { return _isDeaf; }
            set { _isDeaf = value; OnPropertyChanged(); }
        }

        
        public Color LocalNewsBackgroundColor
        {
            get { return _localNewsBackgroundColor; }
            set { _localNewsBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color LocalNewsTextColor
        {
            get { return _localNewsTextColor; }
            set { _localNewsTextColor = value; OnPropertyChanged(); }
        }
        public Color InternationalNewsBackgroundColor
        {
            get { return _internationalNewsBackgroundColor; }
            set { _internationalNewsBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color InternationalNewsTextColor
        {
            get { return _internationalNewsTextColor; }
            set { _internationalNewsTextColor = value; OnPropertyChanged(); }
        }
        public Color ThirdNewsBackgroundColor
        {
            get { return _thirdNewsBackgroundColor; }
            set { _thirdNewsBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color ThirdNewsTextColor
        {
            get { return _thirdNewsTextColor; }
            set { _thirdNewsTextColor = value; OnPropertyChanged(); }
        }
        public string ThirdNewsGIF
        {
            get => thirdNewsGIF; set
            {
                thirdNewsGIF = value;
                OnPropertyChanged();
            }
        }
        public string InternationalNewsGIF
        {
            get => internationalNewsGIF; set
            {
                internationalNewsGIF = value;
                OnPropertyChanged();
            }
        }
        public string LocalNewsGIF
        {
            get => localNewsGIF; set
            {
                localNewsGIF = value;
                OnPropertyChanged();
            }
        }
        //private ObservableRangeCollection<Signbook.Models.News> _news;
        //private bool _isRefreshing;

        //public ObservableRangeCollection<Signbook.Models.News> News
        //{
        //    get { return _news; }
        //    set { _news = value; OnPropertyChanged(); }
        //}
        //public bool IsRefreshing
        //{
        //    get => _isRefreshing;
        //    set { _isRefreshing = value; OnPropertyChanged(); }
        //}
        //public Color LocalNewsTextColor
        //{
        //    get { return _localNewsTextColor; }
        //    set { _localNewsTextColor = value; OnPropertyChanged(); }
        //}
        //public Color InternationalNewsTextColor
        //{
        //    get { return _internationalNewsTextColor; }
        //    set { _internationalNewsTextColor = value; OnPropertyChanged(); }
        //}
        //public Color ThirdNewsTextColor
        //{
        //    get { return _thirdNewsTextColor; }
        //    set { _thirdNewsTextColor = value; OnPropertyChanged(); }
        //}
        //public bool LocalNewsLineVisibility
        //{
        //    get { return _localNewsLineVisibility; }
        //    set { _localNewsLineVisibility = value; OnPropertyChanged(); }
        //}
        //public bool InternationalNewsLineVisibility
        //{
        //    get { return _internationalNewsLineVisibility; }
        //    set { _internationalNewsLineVisibility = value; OnPropertyChanged(); }
        //}
        //public bool ThirdNewsLineVisibility
        //{
        //    get { return _thirdNewsLineVisibility; }
        //    set { _thirdNewsLineVisibility = value; OnPropertyChanged(); }
        //}
        #endregion

        #region Commands
        public ICommand OnCloseTap { get; set; }
        public ICommand LocalNewsSelectedCommand { get; set; }
        public ICommand InternationalNewsSelectedCommand { get; set; }
        public ICommand ThirdNewsSelectedCommand { get; set; }
        public ICommand PullToRefreshCommand { get; set; }
        public ICommand SelectNewsCommand { get; set; }

        #endregion

        #region Page Events
        public NewsMainViewModel()
        {
            LocalNewsSelectedCommand = new Command(LocalNewsSelected);
            InternationalNewsSelectedCommand = new Command(InternationalNewsSelected);
            ThirdNewsSelectedCommand = new Command(AdvertismentSelected);
            OnCloseTap = new Command(CloseTap);
        }
        private async void CloseTap(object obj)
        {
            await NavigationService.Navigation.PopAsync();
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
                        case "Jordan":
                            return "Jordan";
                        case "Egypt":
                            return "Egypt";
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
        protected override Task OnAppearing()
        {
            InitializePageSettings();
            //Application.Current.Properties.Add("SelectedCountry", "Egypt");
            var country = DetemineCountry();
            switch (country)
            {
                case "Omman":
                    LocalNewsGIF = "GifLocalNews.gif";
                    InternationalNewsGIF = "GifInternationalNews.gif";
                    ThirdNewsGIF = "GifAdvertisement.gif";
                    break;
                case "Jordan":
                    LocalNewsGIF = "LocalNewsJordan.gif";
                    InternationalNewsGIF = "InternationalNewsJordan.gif";
                    ThirdNewsGIF = "AdsNewsJordan.gif";
                    break;
                case "Egypt":
                    LocalNewsGIF = "LocalNewsJordan.gif";
                    InternationalNewsGIF = "InternationalNewsJordan.gif";
                    ThirdNewsGIF = "AdsNewsJordan.gif";
                    break;
                default:
                    return null;
            }
            isNotFirst = true;
            return base.OnAppearing();
        }
        #endregion

        #region Commands Implementation

        private async void AdvertismentSelected(object obj)
        {
            IsBusy = true;
            ThirdNewsBackgroundColor = Color.FromHex("#384B6C");
            ThirdNewsTextColor = Color.White;
            await Task.Delay(200);

            var viewModel = BaseLocator.Instance.Resolve<NewsViewModel>();
            viewModel.SelectedNewsType = "Advertisement";
            await NavigationService.NavigateToAsync<NewsViewModel>();
            ThirdNewsBackgroundColor = Color.White;
            ThirdNewsTextColor = Color.Gray;
            IsBusy = false;
        }
        private async void InternationalNewsSelected(object obj)
        {
            IsBusy = true;
            InternationalNewsBackgroundColor = Color.FromHex("#384B6C");
            InternationalNewsTextColor = Color.White;
            await Task.Delay(200);

            var viewModel = BaseLocator.Instance.Resolve<NewsViewModel>();
            viewModel.SelectedNewsType = "InternationalNews";
            await NavigationService.NavigateToAsync<NewsViewModel>();
            InternationalNewsBackgroundColor = Color.White;
            InternationalNewsTextColor = Color.Gray;
            IsBusy = false;
        }
        private async void LocalNewsSelected(object obj)
        {
            IsBusy = true;
            LocalNewsBackgroundColor = Color.FromHex("#384B6C");
            LocalNewsTextColor = Color.White;
            await Task.Delay(200);
            var viewModel = BaseLocator.Instance.Resolve<NewsViewModel>();
            viewModel.SelectedNewsType = "LocalNews";
            await NavigationService.NavigateToAsync<NewsViewModel>();
            LocalNewsBackgroundColor = Color.White;
            LocalNewsTextColor = Color.Gray;
            IsBusy = false;
        }
        #endregion

        #region Methods
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


        
        public void InitializePageSettings()
        {
            LocalNewsBackgroundColor = Color.White;
            InternationalNewsBackgroundColor = Color.White;
            ThirdNewsBackgroundColor = Color.White;
            LocalNewsTextColor = Color.FromHex("#394B6C");
            InternationalNewsTextColor = Color.FromHex("#394B6C");
            ThirdNewsTextColor = Color.FromHex("#394B6C");
            if (IsUserDeaf())
            {
                IsDeaf = true;
            }
            else
            {
                IsDeaf = false;
            }
            
        }
        #endregion
    }
}
