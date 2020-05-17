using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingPage : BaseContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        /*
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
         */

    }
}