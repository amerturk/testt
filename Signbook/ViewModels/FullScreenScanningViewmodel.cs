using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace Signbook.ViewModels
{
   
    public class FullScreenScanningViewmodel : ZXingScannerPage
    {
        public ICommand CloseCommand { get; set; }
        public FullScreenScanningViewmodel()
        {
            CloseCommand = new Command(OnClose);
        }
        private async void OnClose(object arg)
        {
            //await NavigationService.Navigation.PopAsync(false);
        }
    }
}
