using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using Signbook.Controls;
using Signbook.ViewModels;
using WhiteMvvm.Bases;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VideoWebPage : BaseContentPage
    {
        
        public VideoWebPage()
        {
            InitializeComponent();


            /*   Browser.OpenAsync(new Uri("https://storage.googleapis.com/signboo/vidcall/TokBox/tokbox.html"),new BrowserLaunchOptions()
              {
                    LaunchMode = BrowserLaunchMode.SystemPreferred,
                    TitleMode = BrowserTitleMode.Hide,
                    PreferredToolbarColor = System.Drawing.Color.Black,
                    PreferredControlColor = System.Drawing.Color.Black
               });
           */

            //Device.OpenUri("https://storage.googleapis.com/signboo/vidcall/TokBox/tokbox.html");


        }

        
        protected override void OnDisappearing()
        {
            ((VideoWebViewModel)ViewModel).EndSessionCommand.Execute(false);
            base.OnDisappearing();
        }
    }
}