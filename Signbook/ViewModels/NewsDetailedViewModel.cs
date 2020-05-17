using Signbook.Constants;
using Signbook.Services.NewsDetailed;
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
    public class NewsDetailedViewModel : BaseViewModel
    {


        #region Feilds
        public string SelectedNewsType;
        #endregion
        #region Properties
        private ObservableRangeCollection<Signbook.Models.NewsDetailed> _news;
        private bool _isRefreshing;
        private IBrowser _browser;
        private INewsDetailedService _newsService;
        //private string _IsReadColor = "#oooffo";
        public ObservableRangeCollection<Signbook.Models.NewsDetailed> News
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
        public NewsDetailedViewModel(IBrowser browser, INewsDetailedService newsService)
        {
            _browser = browser;
            _newsService = newsService;
            News = new ObservableRangeCollection<Models.NewsDetailed>();
            PullToRefreshCommand = new Command(PullToRefresh);
            SelectNewsCommand = new Command(OnSelectNews);
            OnCloseTap = new Command(CloseTap);
        }

        /*public string IsReadColor
        {
            get { return _IsReadColor; }
            set { _IsReadColor = value; OnPropertyChanged(); }
        }*/

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
            /* IsBusy = true;
             IsRefreshing = true;
             News.Clear();
             await Task.Delay(10);
             News = await _newsService.GetMainNewsCollection(SelectedNewsType);
             //News = service.GetThirdNews();
             IsRefreshing = false;
             IsBusy = false;*/

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
                if (item is Models.NewsDetailed seletedNews)
                {

                    // we have to set the article id in the local storage 

                    string CurrentReadArticles = "";
                    string NewlyClickedArticle = "";

                    if (Application.Current.Properties.ContainsKey("ReadArticles"))
                    {
                        CurrentReadArticles = Application.Current.Properties["ReadArticles"] as string;

                        if (!CurrentReadArticles.Contains("$"+seletedNews.NewsId+"$"))
                        {
                            // here we have to add it to the local storage
                            NewlyClickedArticle = "$" + seletedNews.NewsId + "$";

                            Application.Current.Properties.Remove("ReadArticles");
                            Application.Current.Properties.Add("ReadArticles", CurrentReadArticles + NewlyClickedArticle);
                            await Application.Current.SavePropertiesAsync();

                        }


                    }
                    else
                    {
                        NewlyClickedArticle = NewlyClickedArticle + "$" + seletedNews.NewsId + "$";
                        Application.Current.Properties.Add("ReadArticles", NewlyClickedArticle);
                        await Application.Current.SavePropertiesAsync();

                    }

                    Urls.newsUrl = seletedNews.Url;
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
                    }
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
            News = await _newsService.GetMainNewsCollection(GetNewsUrl("advertisment"));
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
            News = await _newsService.GetMainNewsCollection(GetNewsUrl("generalnews"));
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
            News = await _newsService.GetMainNewsCollection(GetNewsUrl("news"));
            //News = service.GetInternationalNews();
            IsRefreshing = false;
            IsBusy = false;
        }
        #endregion
        #region Methods
        public async void LoadData()
        {
            //IsReadColor = "#384B6C";
            IsBusy = true;
            IsRefreshing = true;
            News.Clear();
            await Task.Delay(10);
            var SelectedNewsSection = Application.Current.Properties["CurrentNewsSection"].ToString();
            News = await _newsService.GetMainNewsCollection(SelectedNewsSection);
            string CurrentReadArticles = "";
            //check if the returned news already read !
            for (int x = 0; x < News.Count; x++)
            {

                if (Application.Current.Properties.ContainsKey("ReadArticles"))
                {
                    CurrentReadArticles = Application.Current.Properties["ReadArticles"] as string;
                    if (!CurrentReadArticles.Contains("$" + News[x].NewsId + "$"))
                    {
                        // here we have to color it by gray!
                        News[x].IsReadColor = "#efefef";
                    }
                }
            }
                
            //News = service.GetThirdNews();
            IsRefreshing = false;
            IsBusy = false;
            /*if (SelectedNewsType == null)
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
            }*/
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
