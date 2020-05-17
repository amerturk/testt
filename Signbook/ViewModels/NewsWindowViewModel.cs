using Signbook.Constants;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace Signbook.ViewModels
{
    public class NewsWindowViewModel : BaseViewModel
    {
        private string _channelSource;
        public static string url;

        public ICommand CloseCommand { get; set; }
        public NewsWindowViewModel()
        {
            CloseCommand = new Command(OnClose);
        }
        private async void OnClose(object arg)
        {
            await NavigationService.Navigation.PopAsync(false);
        }   
        protected override Task OnAppearing()
        {
            if (Urls.newsUrl != null)
            {
                var VideoLink = Urls.newsUrl;
                var fullChannelLink = VideoLink;
                ChannelSource = fullChannelLink;
                base.OnAppearing();
            }
         
            return Task.CompletedTask;
        }
        protected override Task InitializeAsync(object navigationData)
        {
            return Task.CompletedTask;
        }
        public string ChannelSource
        {
            get => _channelSource;
            set
            {
                _channelSource = value;
                OnPropertyChanged();
            }
        }
    }

}
