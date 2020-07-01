using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Signbook.Constants;
using Signbook.Views;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace Signbook.ViewModels
{
    public class MainCallJordanViewModel : BaseViewModel
    {



        private bool _isEnded = false;
        // private IAgoraService _agoraService;

        private bool _isAudioMute;
        public bool IsAudioMute
        {
            get => _isAudioMute;
            set
            {
                _isAudioMute = value;
                OnPropertyChanged();
            }
        }

        private bool _isVideoMute;
        public bool IsVideoMute
        {
            get => _isVideoMute;
            set
            {
                _isVideoMute = value;
                OnPropertyChanged();
            }
        }

        private bool _isSpeakerEnabled;
        public bool IsSpeakerEnabled
        {
            get => _isSpeakerEnabled;
            set
            {
                _isSpeakerEnabled = value;
                OnPropertyChanged();
            }
        }

        private bool _isCameraSwitched;
        public bool IsCameraSwitched
        {
            get => _isCameraSwitched;
            set
            {
                _isCameraSwitched = value;
                OnPropertyChanged();
            }
        }

        private Color _DirectCallBackgroundColor;
        private Color _DirectCallTextColor;
        private Color _SchedualeBackgroundColor;
        private Color _SchedualeTextColor;
        private Color _LogoutBackgroundColor;
        private Color _LogoutTextColor;
        public string Room { get; set; }

        public Command EndSessionCommand { get; }
        public Command AudioMuteCommand { get; }
        public Command VideoMuteCommand { get; }
        public Command SpeakerCommand { get; }
        public Command SwitchCameraCommand { get; }
        public Command VideoTapCommand { get; }
        public Command MainPagesClickCommand { get; set; }

        public Command GoToDoCallPage { get; set; }

        public Command GoToScheduleCallPage { get; set; }

        public Command LogoutCommand { get; set; }

        public Color DirectCallBackgroundColor
        {
            get { return _DirectCallBackgroundColor; }
            set { _DirectCallBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color DirectCallTextColor
        {
            get { return _DirectCallTextColor; }
            set { _DirectCallTextColor = value; OnPropertyChanged(); }
        }
        public Color SchedualeBackgroundColor
        {
            get { return _SchedualeBackgroundColor; }
            set { _SchedualeBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color SchedualeTextColor
        {
            get { return _SchedualeTextColor; }
            set { _SchedualeTextColor = value; OnPropertyChanged(); }
        }
        public Color LogoutBackgroundColor
        {
            get { return _LogoutBackgroundColor; }
            set { _LogoutBackgroundColor = value; OnPropertyChanged(); }
        }
        public Color LogoutTextColor
        {
            get { return _LogoutTextColor; }
            set { _LogoutTextColor = value; OnPropertyChanged(); }
        }

        public MainCallJordanViewModel()
        {
            Room = "DesignTimeRoom";
            EndSessionCommand = new Command(OnEndSession);
            AudioMuteCommand = new Command(OnAudioMute);
            VideoMuteCommand = new Command(OnVideoMute);
            SpeakerCommand = new Command(OnSpeaker);
            SwitchCameraCommand = new Command(SwitchCamera);
            VideoTapCommand = new Command(TapVideo);
            MainPagesClickCommand = new Command(backclicked);
            GoToDoCallPage = new Command(gotocallpage);
            GoToScheduleCallPage = new Command(gotoschedulecallpage);
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

            LogoutBackgroundColor = Color.FromHex("#384B6C");
            LogoutTextColor = Color.White;
            await Task.Delay(200);
            NavigationService.NavigateToAsync<MainViewModel>();
            LogoutBackgroundColor = Color.White;
            LogoutTextColor = Color.Gray;

        }
        protected override Task InitializeAsync(object navigationData)
        {
            Init();
            return base.InitializeAsync(navigationData);
        }
        private void backclicked(object param)
        {
            NavigationService.NavigateToAsync<MainViewModel>();
        }
        private async void gotocallpage(object param)
        {
            DirectCallBackgroundColor = Color.FromHex("#384B6C");
            DirectCallTextColor = Color.White;
            await Task.Delay(200);

            NavigationService.NavigateToAsync<DoCallJordanViewModel>();
            DirectCallBackgroundColor = Color.White;
            DirectCallTextColor = Color.Gray;
        }

        private async void gotoschedulecallpage(object param)
        {
            SchedualeBackgroundColor = Color.FromHex("#384B6C");
            SchedualeTextColor = Color.White;
            await Task.Delay(200);
            NavigationService.NavigateToAsync<ScheduleCallViewModel>();
            SchedualeBackgroundColor = Color.White;
            SchedualeTextColor = Color.Gray;
        }

        
        private void TapVideo(object param)
        {
            
        }

        public async void goToSignupPage()
        {
            NavigationService.NavigateToAsync<SignupViewModel>();
        }

            private void SwitchCamera(object param)
        {
            // IsCameraSwitched = !IsCameraSwitched;
            // _agoraService.ToggleCamera();
        }

        private void OnSpeaker(object param)
        {
            //IsSpeakerEnabled = !IsSpeakerEnabled;
            // _agoraService.SetSpeakerEnabled(IsSpeakerEnabled);
        }

        private void OnAudioMute(object param)
        {
            // IsAudioMute = !IsAudioMute;
            //_agoraService.SetAudioMute(IsAudioMute);
        }

        private void OnVideoMute(object param)
        {
            // IsVideoMute = !IsVideoMute;
            //_agoraService.SetVideoMute(IsVideoMute);

        }

        private void OnEndSession(object param)
        {
            if (_isEnded)
            {
                //return;
            }
            _isEnded = true;
            //_agoraService.EndSession();
            NavigationService.Navigation.PopAsync();
        }

        public void Init()
        {




            //_isEnded = false;
            /*var browser = new WebView();
            browser.Source = "https://xamarin.swappsdev.net";
            Content = browser;
            */
        }

        private void OnDisappearing()
        {
            //OnEndSession(false);
            //return base.OnDisappearing();
        }

        private void OnNewStream(uint arg1, int arg2, int arg3)
        {
        }


        private void OnDisconnected(uint obj)
        {
        }
    }
}
