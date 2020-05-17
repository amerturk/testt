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
    public partial class ScheduleCallPage : BaseContentPage
    {
        Dictionary<string, string> valuess = new Dictionary<string, string>();
        Dictionary<string, string> CompaniesImages = new Dictionary<string, string>();
        Dictionary<string, string> CompaniesLocations = new Dictionary<string, string>();

        public ScheduleCallPage()
        {
            InitializeComponent();

            /* we have to build a dectionary and a list 
             where we will add the list items to the Xamarin picker and we will
             get the values from the dectionary 
             */


            FillcompaniesDD();



        }

        public void FillcompaniesDD()
        {


            var getCompaniesURL = "http://omnapp.signbook.co/api/SitesApis/APPGetAllCompaniesFull?PageNo=1&PageSize=10&SearchTerm=&Orderby=date";


            var companiesList = new List<string>();



            using (var clientt = new WebClient())
            {

                string result = clientt.DownloadString(getCompaniesURL);



                JObject SerialaizedJSON = JObject.Parse(result);
                string CompaniesString = SerialaizedJSON["Table"].ToString();


                int NumberoOfCompanies = Check.CheckOccurrences(CompaniesString.ToLower(), "companyid");

                for (int x = 0; x < NumberoOfCompanies; x++)
                {

                    valuess.Add(SerialaizedJSON["Table"][x]["CompanyID"].ToString(), SerialaizedJSON["Table"][x]["CompanyName"].ToString());
                    CompaniesImages.Add(SerialaizedJSON["Table"][x]["CompanyName"].ToString(), SerialaizedJSON["Table"][x]["ImageURL"].ToString());
                    CompaniesLocations.Add(SerialaizedJSON["Table"][x]["CompanyName"].ToString(), SerialaizedJSON["Table"][x]["OpenArea"].ToString());
                    companiesList.Add(SerialaizedJSON["Table"][x]["CompanyName"].ToString());
                }
                var Npicker = picker;
                Npicker.ItemsSource = companiesList;

            }

        }


        public static class Check
        {
            public static int CheckOccurrences(string str1, string pattern)
            {
                int count = 0;
                int a = 0;
                while ((a = str1.IndexOf(pattern, a)) != -1)
                {
                    a += pattern.Length;
                    count++;
                }
                return count;
            }
        }

        async void SelectionChanged(object sender, EventArgs args)
        { 
            var SelectedCompany = picker.SelectedIndex;
            var SelectedCompanyz = picker.SelectedItem;
            var SelectedCompanyImage = CompaniesImages.FirstOrDefault(x => x.Key == SelectedCompanyz).Value;
            var SelectedCompanyOpenArea = CompaniesLocations.FirstOrDefault(x => x.Key == SelectedCompanyz).Value;
            CompanyImage.Source =  new UriImageSource
            {
                Uri = new Uri(SelectedCompanyImage)
            };

            
            ScheduleBtn.IsEnabled = true;

        }
        async void DoCallSchedule(object sender, EventArgs args)
        {
            var SelectedCompany = picker.SelectedItem;
            var SelectedCompanyID = valuess.FirstOrDefault(x => x.Value == SelectedCompany).Key;          
            
            string ScheduleTime = cTime.Time.ToString();
            string ScheduleDate = cDate.Date.ToString();

            //here to open the video call page
            DoSchedule(SelectedCompanyID, ScheduleTime, ScheduleDate);
        }


        public void DoSchedule(string CompanyID, string Time, string Date)
        {
            // we have to get the logged in UserID
            
            string UserID = "";
            try
            {
                 UserID = Application.Current.Properties["UserID"].ToString();
            }
            catch (Exception e)
            { 
            
            }

            try {
                
              

                var Scheduleurl = string.Format("http://omnapp.signbook.co/api/SitesApis/InsertCallSchedule?CompanyID={0}&UserID={1}&Time={2}&Date={3}", CompanyID, UserID, Time, Date);

                
                using (var clientt = new WebClient())
                {

                    string result = clientt.DownloadString(Scheduleurl);

                    
                    if (result.ToLower().Contains("error"))
                    {
                        // we have to update the user 
                        Responceformlbl.Text = "حدث خطاء ما الرجاء المحاولة لاحقا";
                        ScheduleBtn.IsEnabled = false;

                    }

                    //sucess
                    else
                    {
                        Responceformlbl.Text = "";
                        ConfirmationLabel.Text = "تم حجز الموعد بنجاح";
                        ScheduleBtn.IsEnabled = false;

                    }


                }
            }
            catch (Exception e)
            {
                Responceformlbl.Text = "حدث خطاء ما الرجاء المحاولة لاحقا";
                ScheduleBtn.IsEnabled = false;
            }
            


        }
        protected override void OnDisappearing()
        {
           
        }
    }
}