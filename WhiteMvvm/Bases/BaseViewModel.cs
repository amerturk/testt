using System;
using System.Threading.Tasks;
using WhiteMvvm.Configuration;
using WhiteMvvm.Services.Dialog;
using WhiteMvvm.Services.Navigation;
using WhiteMvvm.Utilities;
using Xamarin.Forms;

namespace WhiteMvvm.Bases
{
    public class BaseViewModel : NotifiedObject
    {
        protected readonly IDialogService DialogService;
        protected readonly INavigationService NavigationService;
        private volatile bool  _isInitialize;
        private bool _isBusy;
        private volatile bool _isOnAppeared;
        private bool _isDataLoaded;
        public object NavigationData { get; private set; }
        public BaseViewModel()
        {
            DialogService = BaseLocator.Instance.Resolve<IDialogService>();
            NavigationService = BaseLocator.Instance.Resolve<INavigationService>();

        }

        protected internal virtual Task OnNavigateFrom(BaseViewModel page , object parameter)
        {
            return Task.CompletedTask;
        }

        public bool IsDataLoaded
        {
            get => _isDataLoaded;
            set
            {
                _isDataLoaded = value; 
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get => _isBusy;
            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    if (ConfigurationManager.Current.UseBaseIndicator)
                    {
                        if (value)
                        {
                            DialogService.ShowLoading(ConfigurationManager.Current.IndicatorMaskType);
                        }
                        else
                        {
                            DialogService.HideLoading();
                        }
                    }
                    _isBusy = value;
                    OnPropertyChanged();
                });
            }
        }
        protected internal virtual Task OnPopupAppearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnPopupDisappearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnAppearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task OnDisappearing()
        {
            return Task.CompletedTask;
        }
        protected internal virtual Task InitializeAsync(object navigationData)
        {
            NavigationData = navigationData;
            return Task.CompletedTask;
        }
        protected internal virtual Task InitializeVolatileAsync(object navigationData)
        {
            NavigationData = navigationData;
            return Task.CompletedTask;
        }
        internal void InternalInitialize(object navigationData)
        {
            InitializeVolatileAsync(navigationData);
            if (_isInitialize)
                return;
            _isInitialize = true;
            InitializeAsync(navigationData);
        }
        protected virtual Task OnAppeared()
        {
            return Task.CompletedTask;
        }
        internal void InternalOnAppeared()
        {
            if (_isOnAppeared)
                return;
            _isOnAppeared = true;
            OnAppeared();
        }
        protected internal virtual bool HandleBackButton()
        {
            return false;
        }
        public void OnPagePopup()
        {
            NavigationService.OnPagePopup(this, new EventArgs());
        }
    }
}
