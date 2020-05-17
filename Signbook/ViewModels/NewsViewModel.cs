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
    public class NewsViewModel : BaseViewModel
    {


        #region Feilds
        public string SelectedNewsType;
        #endregion
        #region Properties
        private ObservableRangeCollection<Signbook.Models.News> _news;
        private bool _isRefreshing;
        private IBrowser _browser;
        private INewsService _newsService;

        public ObservableRangeCollection<Signbook.Models.News> News
        {
            get { return _news; }
            set { _news = value; OnPropertyChanged(); }
        }
        public bool IsRefreshing
        {
            get => _isRefreshing;
            set { _isRefreshing = value; OnPropertyChanged(); }
        }

        #endregion
        #region Commands
        public ICommand PullToRefreshCommand { get; set; }
        public ICommand SelectNewsCommand { get; set; }
        public ICommand OnCloseTap { get; set; }
        #endregion

        #region PageEvent
        public NewsViewModel(IBrowser browser, INewsService newsService)
        {
            _browser = browser;
            _newsService = newsService;
            News = new ObservableRangeCollection<Models.News>();
            PullToRefreshCommand = new Command(PullToRefresh);
            SelectNewsCommand = new Command(OnSelectNews);
            OnCloseTap = new Command(CloseTap);
        }

        private async void CloseTap(object obj)
        {
            await NavigationService.Navigation.PopAsync();
        }
        protected override Task OnAppearing()
        {
            LoadData();
            return base.OnAppearing();
        }
        #endregion

        #region Commands Implementation
        private async void PullToRefresh(object obj)
        {
            IsRefreshing = false;
            IsBusy = false;

            /*if (SelectedNewsType == null)
                return;
            switch (SelectedNewsType)
            {
                case "InternationalNews":
                    InternationalNewsSelected(null);
                    break;

                case "LocalNews":
                    LocalNewsSelected(null);
                    break;

                case "Advertisement":
                    AdvertismentSelected(null);
                    break;
            }*/
        }
        private async void OnSelectNews(object item)
        {
            try
            {

                

                if (item is Models.News seletedNews)
                {

                    IsBusy = true;

                    var viewModel = BaseLocator.Instance.Resolve<NewsDetailedViewModel>();
                    //viewModel.SelectedNewsType = ;

                    if (Application.Current.Properties.ContainsKey("CurrentNewsSection"))
                    {
                        Application.Current.Properties.Remove("CurrentNewsSection");
                        Application.Current.Properties.Add("CurrentNewsSection", seletedNews.NewsFile);
                        await Application.Current.SavePropertiesAsync();

                    }
                    else
                    {
                        Application.Current.Properties.Add("CurrentNewsSection", seletedNews.NewsFile);
                        await Application.Current.SavePropertiesAsync();

                    }

                    await NavigationService.NavigateToAsync<NewsDetailedViewModel>();
                    IsBusy = false;

                    /* Urls.newsUrl = seletedNews.Url;
                     if (Device.RuntimePlatform == Device.Android)
                     {
                         await NavigationService.NavigateToAsync<NewsWindowViewModel>();
                     }
                     else
                     {
                         await _browser.OpenAsync(new Uri(seletedNews.Url.ToString()), new BrowserLaunchOptions()
                         {
                             TitleMode = BrowserTitleMode.Hide,
                             LaunchMode = BrowserLaunchMode.SystemPreferred
                         }); ;
                     }*/
                }
            }
            catch (Exception exception)
            {
                await DialogService.ShowErrorAsync(exception.Message, exception);
            }
        }

        #endregion
        #region command Implementation
        private async void AdvertismentSelected(object obj)
        {
            IsBusy = true;
            IsRefreshing = true;
            News.Clear();
            await Task.Delay(10);
            News = await _newsService.GetMainNewsCollection(GetNewsUrl("advertismentsections"));
            //News = service.GetThirdNews();
            IsRefreshing = false;
            IsBusy = false;
        }
        private async void InternationalNewsSelected(object obj)
        {
            IsBusy = true;
            IsRefreshing = true;
            News.Clear();
            await Task.Delay(10);
            News = await _newsService.GetMainNewsCollection(GetNewsUrl("generalnewssections"));
            //News = service.GetInternationalNews();
            IsRefreshing = false;
            IsBusy = false;
        }
        private async void LocalNewsSelected(object obj)
        {
            IsBusy = true;
            IsRefreshing = true;
            News.Clear();
            await Task.Delay(10);
            News = await _newsService.GetMainNewsCollection(GetNewsUrl("newssections"));
            //News = service.GetInternationalNews();
            IsRefreshing = false;
            IsBusy = false;
        }
        #endregion
        #region Methods
        public void LoadData()
        {
            if (SelectedNewsType == null)
                return;
            //IsRefreshing = true;
            switch (SelectedNewsType)
            {
                case "InternationalNews":
                    InternationalNewsSelected(null);
                    break;

                case "LocalNews":
                    LocalNewsSelected(null);
                    break;

                case "Advertisement":
                    AdvertismentSelected(null);
                    break;
            }
            //IsRefreshing = false;
        }
        #endregion
        #region Methods

        private string GetNewsUrl(string newsType)
        {
            string Country = DetemineCountry();
            if (!string.IsNullOrEmpty(Country))
            {
                switch (Country)
                {
                    case "Omman":
                        return Urls.baseNewsUrl + "omn/" + newsType + ".txt";
                    case "Jordan":
                        return Urls.baseNewsUrl + "jor/" + newsType + ".txt";

                    case "Egypt":
                        return Urls.baseNewsUrl + "egy/" + newsType + ".txt";
                    default:
                        return null;
                }
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


        public void OpenFullScreenPage(string url)
        {
            NavigationService.NavigateToAsync<BarcodeWindowViewModel>();
        }
        #endregion
    }
}
