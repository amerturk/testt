using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.DeviceUtilities;
using Xamarin.Forms;

namespace Signbook.ViewModels
{
    public class DisabilityTypeViewModel : BaseViewModel
    {
        #region Feilds
        private ImageSource _deafImageSource;
        private ImageSource _blindImageSource;
        private Color _blindBackground;
        private Color _deafBackground;
        private ImageSource _personImage;
        #endregion
        #region Properties
        public ImageSource DeafImageSource
        {
            get { return _deafImageSource; }
            set { _deafImageSource = value; OnPropertyChanged(); }
        }
        public ImageSource BlindImageSource
        {
            get { return _blindImageSource; }
            set { _blindImageSource = value; OnPropertyChanged(); }
        }
        public Color DeafBackground
        {
            get { return _deafBackground; }
            set { _deafBackground = value; OnPropertyChanged(); }
        }
        public Color BlindBackground
        {
            get { return _blindBackground; }
            set { _blindBackground = value; OnPropertyChanged(); }
        }
        public ImageSource PersonImage
        {
            get { return _personImage; }
            set { _personImage = value; OnPropertyChanged(); }
        }
        #endregion
        #region Commands
        public ICommand DeafSelectedCommand { get; set; }
        public ICommand BlindSelectedCommand { get; set; }

        #endregion
        #region Constractor
        public DisabilityTypeViewModel()
        {
            DeafSelectedCommand = new Command(DeafSelected);
            BlindSelectedCommand = new Command(BlindSelected);
        }
        #endregion
        protected override Task OnAppearing()
        {
            SetPageSetting();
            return base.OnAppearing();
        }
        protected override Task OnDisappearing()
        {
            return base.OnDisappearing();
        }
        #region Commands Implementation
        private async void DeafSelected(object obj)
        {
            //DeafBackground = Color.FromHex("#384B6C");
            // will change to  whiteBlindImage
            //DeafImageSource = "BlueDeafImage.png";
            await Task.Delay(200);
            if (Application.Current.Properties.ContainsKey("SelectedDisablitiyType"))
            {
                Application.Current.Properties.Remove("SelectedDisablitiyType");
                Application.Current.Properties.Add("SelectedDisablitiyType", "Deaf");
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                Application.Current.Properties.Add("SelectedDisablitiyType", "Deaf");
                await Application.Current.SavePropertiesAsync();
            }
            var preferences = BaseLocator.Instance.Resolve<IPreferences>();
            preferences.Set("IsFirstTime", "True");
            WhiteApplication.SetHomePage<MainViewModel>();
        }
        private async void BlindSelected(object obj)
        {
           // BlindBackground = Color.FromHex("#384B6C");
           // BlindImageSource = "WhiteBlindImage.png";
            await Task.Delay(200);
            if (Application.Current.Properties.ContainsKey("SelectedDisablitiyType"))
            {
                Application.Current.Properties.Remove("SelectedDisablitiyType");
                Application.Current.Properties.Add("SelectedDisablitiyType", "Blind");
                await Application.Current.SavePropertiesAsync();
            }
            else
            {
                Application.Current.Properties.Add("SelectedDisablitiyType", "Blind");
                await Application.Current.SavePropertiesAsync();
            }
            var preferences = BaseLocator.Instance.Resolve<IPreferences>();
            preferences.Set("IsFirstTime", "True");
            WhiteApplication.SetHomePage<MainViewModel>();
        }
        #endregion
        #region Methods
        public void SetPageSetting()
        {
            BlindBackground = Color.White;
            DeafBackground = Color.White;
            DeafImageSource = "BlueDeafImage.png";
            BlindImageSource = "BlueBlindImage.png";
            if (Application.Current.Properties.ContainsKey("SelectedCountry"))
            {
                var SelectedCountry = Application.Current.Properties["SelectedCountry"];
                if (SelectedCountry != null && !string.IsNullOrEmpty(SelectedCountry.ToString()))
                {
                    switch (SelectedCountry)
                    {
                        case "Omman":
                            PersonImage = "OmanPerson.png";
                            break;
                        case "Jordan":
                            PersonImage = "JordanPerson.png";
                            break;
                        case "Egypt":
                            PersonImage = "EgyptPerson.png";
                            break;
                    }
                }
            }   
        }
        #endregion
     
    }
}
