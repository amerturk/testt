using Signbook.Dependency;
using Signbook.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.DeviceUtilities;
using Xamarin.Forms;

namespace Signbook.ViewModels
{
    public class CountriesViewModel : BaseViewModel
    {
        #region Fields
        private Color _jordanBackgroundColor;
        private Color _egyptBackgroundColor;
        private Color _ommanBackgroundColor;
        private Color _jordanTextColor;
        private Color _egyptTextColor;
        private Color _ommanTextColor;
        #endregion
        #region Properties
        public Color OmmanBackgroundColor
        {
            get { return _ommanBackgroundColor; }
            set { _ommanBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color JordanBackgroundColor
        {
            get { return _jordanBackgroundColor; }
            set { _jordanBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color EgyptBackgroundColor
        {
            get { return _egyptBackgroundColor; }
            set { _egyptBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color OmmanTextColor
        {
            get { return _ommanTextColor; }
            set { _ommanTextColor = value; OnPropertyChanged(); }
        }
        public Color JordanTextColor
        {
            get { return _jordanTextColor; }
            set { _jordanTextColor = value; OnPropertyChanged(); }
        }
        public Color EgyptTextColor
        {
            get { return _egyptTextColor; }
            set { _egyptTextColor = value; OnPropertyChanged(); }
        }
        #endregion
        #region Commands
        public ICommand JordanSelectedCommand { get; set; }
        public ICommand OmmanSelectedCommand { get; set; }
        public ICommand EgyptSelectedCommand { get; set; }
        #endregion
        #region Constractor
        public CountriesViewModel()
        {
            JordanSelectedCommand = new Command(JordanSelected);
            OmmanSelectedCommand = new Command(OmmanSelected);
            EgyptSelectedCommand = new Command(EgyptSelected);
        }
        #endregion
        protected override Task OnAppearing()
        {
            SetPageSetting();
            if (Device.RuntimePlatform == Device.Android)
                DependencyService.Get<IStatusBar>().ShowStatusBar();
            DependencyService.Get<IStatusBarStyleManager>().SetLightTheme();
            return base.OnAppearing();
        }
      
        #region Command Implementation
        private async void EgyptSelected(object obj)
        {
            EgyptBackgroundColor = Color.FromHex("#384B6C");
            EgyptTextColor = Color.White;
            await Task.Delay(200);
            //Save Selected Country
            if (Application.Current.Properties.ContainsKey("SelectedCountry"))
            {
                Application.Current.Properties.Remove("SelectedCountry");
                Application.Current.Properties.Add("SelectedCountry", "Egypt");
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                Application.Current.Properties.Add("SelectedCountry", "Egypt");
                await Application.Current.SavePropertiesAsync();
            }
            await NavigationService.NavigateToAsync<DisabilityTypeViewModel>();
        }
        private async void OmmanSelected(object obj)
        {
            OmmanBackgroundColor = Color.FromHex("#384B6C");
            OmmanTextColor = Color.White;
            await Task.Delay(200);
            //Save Selected Country
            if (Application.Current.Properties.ContainsKey("SelectedCountry"))
            {
                Application.Current.Properties.Remove("SelectedCountry");
                Application.Current.Properties.Add("SelectedCountry", "Omman");
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                Application.Current.Properties.Add("SelectedCountry", "Omman");
                await Application.Current.SavePropertiesAsync();
            }
            await NavigationService.NavigateToAsync<DisabilityTypeViewModel>();
        }
        private async void JordanSelected(object obj)
        {
            JordanBackgroundColor = Color.FromHex("#384B6C");
            JordanTextColor = Color.White;
            await Task.Delay(200);
            //Save Selected Country
            if (Application.Current.Properties.ContainsKey("SelectedCountry"))
            {
                Application.Current.Properties.Remove("SelectedCountry");
                Application.Current.Properties.Add("SelectedCountry", "Jordan");
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                Application.Current.Properties.Add("SelectedCountry", "Jordan");
                await Application.Current.SavePropertiesAsync();
            }
            await NavigationService.NavigateToAsync<DisabilityTypeViewModel>();
        }
        #endregion
        #region Methods

        public void SetPageSetting()
        {
            OmmanBackgroundColor = Color.White;
            JordanBackgroundColor = Color.White;
            EgyptBackgroundColor = Color.White;
            OmmanTextColor = Color.FromHex("#394B6C");
            JordanTextColor = Color.FromHex("#394B6C");
            EgyptTextColor = Color.FromHex("#394B6C");
        }
        #endregion
        protected override bool HandleBackButton()
        {
            NavigationService.Navigation.PopAsync();
            return base.HandleBackButton();
        }
    }
}
