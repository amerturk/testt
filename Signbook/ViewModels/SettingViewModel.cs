using Signbook.Dependency;
using Signbook.Resources;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace Signbook.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {
        #region Feilds
        private bool isDisplayAlart;
        private bool _breakingNewToggled;
        private string suggestionAndComplitionGIF;
        private string changeCountryGIF;
        private string changeDisabilityTypeGIF;
        private string notificationGIF;
        #endregion

        #region Properties
        public bool BreakingNewToggled
        {
            get => _breakingNewToggled;
            set { _breakingNewToggled = value; OnPropertyChanged(); }
        }
        public string SuggestionAndComplitionGIF
        {
            get => suggestionAndComplitionGIF; set
            {
                suggestionAndComplitionGIF = value;
                OnPropertyChanged();
            }
        }
        public string ChangeCountryGIF
        {
            get => changeCountryGIF;
            set
            {
                changeCountryGIF = value;
                OnPropertyChanged();
            }
        }
        public string ChangeDisabilityTypeGIF
        {
            get => changeDisabilityTypeGIF;
            set
            {
                changeDisabilityTypeGIF = value;
                OnPropertyChanged();
            }
        }
        public string NotificationGIF
        {
            get => notificationGIF; set
            {
                notificationGIF = value;
                OnPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand OnCloseTap { get; set; }
        public ICommand ChangeDisabilityTypeSelectedCommand { get; set; }
        public ICommand ChangeCountrySelectedCommand { get; set; }
        public ICommand SuggestionAndComplitionSelectedCommand { get; set; }
        public ICommand BreakingNewsCommand { get; set; }
        
        #endregion

        #region Page Event
        protected override Task OnAppearing()
        {
            var country = DetemineCountry();
            switch (country)
            {
                case "Omman":
                    ChangeDisabilityTypeGIF = "ChangeDisabilityTypeOmanGIF.gif";
                    ChangeCountryGIF = "ChangeCountryOmanGIF.gif";
                    SuggestionAndComplitionGIF = "SuggestionOmanGIF";
                    NotificationGIF = "NotificationOmanGIF";
                    break;
                case "Jordan":
                    ChangeDisabilityTypeGIF = "ChangeDisabilityTypeJordanGIF.gif";
                    ChangeCountryGIF = "ChangeCountryJordanGIF.gif";
                    SuggestionAndComplitionGIF = "SuggestionJordanGIF.gif";
                    NotificationGIF = "NotificationJordanGIF.gif";
                    break;
                case "Egypt":
                    ChangeDisabilityTypeGIF = "ChangeDisabilityTypeJordanGIF.gif";
                    ChangeCountryGIF = "ChangeCountryJordanGIF.gif";
                    SuggestionAndComplitionGIF = "SuggestionJordanGIF.gif";
                    NotificationGIF = "NotificationJordanGIF.gif";
                    break;
                default:
                    return null;
            }
            GetUserSetting();
            return base.OnAppearing();
        }
        public SettingViewModel()
        {
            ChangeDisabilityTypeSelectedCommand = new Command(ChangeDisabilityTypeSelected);
            ChangeCountrySelectedCommand = new Command(ChangeCountrySelected);
            SuggestionAndComplitionSelectedCommand = new Command(SuggestionAndComplitionSelected);
            BreakingNewsCommand = new Command(BreakingNewsSelected);
            OnCloseTap = new Command(CloseTap);
        }

        private async void CloseTap(object obj)
        {
           await NavigationService.Navigation.PopAsync();
        }

        public SettingViewModel(bool breakingNewToggled)
        {
            BreakingNewToggled = breakingNewToggled;
        }
        #endregion

        #region Commands Implementation
        private async void ChangeDisabilityTypeSelected(object obj)
        {
            await NavigationService.NavigateToAsync<EditDisabilityTypeViewModel>();
        }

        private async void ChangeCountrySelected(object obj)
        {
            await NavigationService.NavigateToAsync<EditCountriesViewModel>();
        }

        private async void SuggestionAndComplitionSelected(object obj)
        {
            await NavigationService.NavigateToAsync<SuggestionViewModel>();
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

        public async void BreakingNewsSelected(object obj)
        {
            try
            {
               // DependencyService.Get<IAppSettingsHelper>().OpenAppSettings();
                

                if (obj is Xamarin.Forms.ToggledEventArgs selectedValue)
                {
                    BreakingNewToggled = selectedValue.Value;
                    //BreakingNewToggled = !BreakingNewToggled;
                    if (Application.Current.Properties.ContainsKey("BreakingNews"))
                    {
                        Application.Current.Properties.Remove("BreakingNews");
                    }
                    if (BreakingNewToggled)
                    {
                        Application.Current.Properties.Add("BreakingNews", "true");
                        var isPushEnabled = DependencyService.Get<INotificationsInterface>().registeredForNotifications();
                        if (!isPushEnabled)
                        {
                            if (!isDisplayAlart)
                            {
                                isDisplayAlart = true;
                                var resualt = await DialogService.ShowConfirmMessageAsync(AppResource.PleaseEnableNotificationFromSetting, "", AppResource.Cancel, AppResource.proceed);
                                if (resualt)
                                {
                                    DependencyService.Get<IAppSettingsHelper>().OpenAppSettings();
                                }
                                isDisplayAlart = false;
                            }
                        }
                        else
                        {
                           
                        }
                    }
                    else
                    {
                        Application.Current.Properties.Add("BreakingNews", "false");
                        
                    }
                    await Application.Current.SavePropertiesAsync();
                }
            }
            catch (Exception exception)
            {
                await DialogService.ShowErrorAsync(exception.Message, exception);
            }
        }
        #endregion
        #region Methods

        public async void GetUserSetting()
        {
            try
            {
                if (Application.Current.Properties.ContainsKey("BreakingNews"))
                {
                    var BreakingNews = Application.Current.Properties["BreakingNews"] as string;
                    BreakingNewToggled = Convert.ToBoolean(BreakingNews);
                }
                else
                {
                    var isPushEnabled = DependencyService.Get<INotificationsInterface>().registeredForNotifications();
                    Application.Current.Properties.Add("BreakingNews", isPushEnabled.ToString());
                    BreakingNewToggled = isPushEnabled;
                    await Application.Current.SavePropertiesAsync();
                }
            }
            catch (Exception exception)
            {
                await DialogService.ShowErrorAsync(exception.Message, exception);
            }
        }
        #endregion
    }
}
