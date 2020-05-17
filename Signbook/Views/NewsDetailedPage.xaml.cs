using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Plugin.Share;
using Plugin.Share.Abstractions;
using WhiteMvvm.Bases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsDetailedPage : BaseContentPage
    {
        public NewsDetailedPage()
        {
            InitializeComponent();
        }

        private async void SaherMsg(object sender, EventArgs e)
        {

            try
            {                
                    var client = new WebClient();
                    var content = await client.DownloadStringTaskAsync("https://storage.googleapis.com/signboo/applink.txt");

                CrossShare.Current.Share(new ShareMessage
                {
                    Title = ((Button)sender).BindingContext as string,
                    Text = ((Button)sender).BindingContext as string,
                    Url = content
                });

            }
            catch (Exception ex)
            {
                CrossShare.Current.Share(new ShareMessage
                {
                    Title = ((Button)sender).BindingContext as string,
                    Text = ((Button)sender).BindingContext as string,
                    Url = "https://applk.io/signbook"
                });


            }


        }
        
    }
}