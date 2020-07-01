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
    public partial class MainPage : BaseContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            if (!IsUserOmaniOrJordanianUser())
            {
                callBtnMain.IsVisible = false;
            }
            else
            {
                callBtnMain.IsVisible = true;
            }

            if (!IsDeafPerson())
            {
                newsBtnMain.IsVisible = false;
            }
            else
            {
                newsBtnMain.IsVisible = true;
            }

        }

        private bool IsUserOmaniOrJordanianUser()
        {
            //Application.Current.Properties.Add("SelectedCountry", "Egypt");
            if (Application.Current.Properties.ContainsKey("SelectedCountry"))
            {
                var Country = Application.Current.Properties["SelectedCountry"] as string;
                if (Country != null && !string.IsNullOrEmpty(Country.ToString()))
                {
                    switch (Country)
                    {
                        case "Omman":
                            return true;
                        case "Jordan":
                            return true;
                        default:
                            return false;
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        private bool IsDeafPerson()
        {

            if (Application.Current.Properties.ContainsKey("SelectedDisablitiyType"))
            {
                var DisabilityType = Application.Current.Properties["SelectedDisablitiyType"] as string;
                if (DisabilityType != null && !string.IsNullOrEmpty(DisabilityType.ToString()))
                {
                    switch (DisabilityType)
                    {
                        case "Blind":
                            return false;
                        case "Deaf":
                            return true;
                        default:
                            return true;
                    }
                }
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}