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
    public class RoomViewModel : BaseViewModel
    {
        private bool _isEnded = false;

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

        public string Room { get; set; }

        public Command EndSessionCommand { get; }
        public Command AudioMuteCommand { get; }
        public Command VideoMuteCommand { get; }
        public Command SpeakerCommand { get; }
        public Command SwitchCameraCommand { get; }
        public Command VideoTapCommand { get; }

        public RoomViewModel()
        {
            Room = "DesignTimeRoom";
            EndSessionCommand = new Command(OnEndSession);
            AudioMuteCommand = new Command(OnAudioMute);
            VideoMuteCommand = new Command(OnVideoMute);
            SpeakerCommand = new Command(OnSpeaker);
            SwitchCameraCommand = new Command(SwitchCamera);
            VideoTapCommand = new Command(TapVideo);
        }

        protected override Task InitializeAsync(object navigationData)
        {
            Init();
            return base.InitializeAsync(navigationData);
        }

        private void TapVideo(object param)
        {
           
        }

        private void SwitchCamera(object param)
        {
            
        }

        private void OnSpeaker(object param)
        {
            
        }

        private void OnAudioMute(object param)
        {
            
        }

        private void OnVideoMute(object param)
        {
          

        }

        private void OnEndSession(object param)
        {
            
        }

        public void Init()
        {
           
        }

        protected override Task OnDisappearing()
        {
            OnEndSession(false);
            return base.OnDisappearing();
        }

        private void OnNewStream(uint arg1, int arg2, int arg3)
        {
        }


        private void OnDisconnected(uint obj)
        {
        }
    }
}
