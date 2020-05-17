using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SplashscreenPage : ContentPage
    {
        public SplashscreenPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {

            base.OnAppearing(); 
        }
    }
}