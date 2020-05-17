using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Signbook.Controls;
using Signbook.ViewModels;
using WhiteMvvm.Bases;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainCallPage : BaseContentPage
    {
        
        public MainCallPage()
        {
            InitializeComponent();

            

        }
        async void LoginUser(object sender, EventArgs args)
        {
          
           

        }

      
        public async void login(string Username, string Password)
        {
           


        }
        async void GoToSignup(object sender, EventArgs args)
        {
            

        }

        protected override void OnDisappearing()
        {
            
        }
    }
}