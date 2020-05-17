using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Org.Apache.Http.Message;
using Signbook.Controls;
using Signbook.ViewModels;
using WhiteMvvm.Bases;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;



namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : BaseContentPage
    {
        
        public LoginPage()
        {
            InitializeComponent();

            

        }
        async void LoginUser(object sender, EventArgs args)
        {
            lognbtn.IsEnabled = false;
            string UserNAme = uName.Text;
            string Password = uPass.Text;
            

            bool validfields = true;

            // validate form fields
            if (UserNAme != null && Password != null )
            {



                if (UserNAme.Length <= 0 || Password.Length <= 0 )
                {
                    validfields = false;
                }
                if (UserNAme.Length > 50 || Password.Length > 50)
                {
                    validfields = false;
                }
                


                if (!validfields)
                {
                    // here we have to display the error msg before submission
                    Responceformlbl.Text = "الرجاء تعبئة الحقول بالشكل الصحيح";
                    lognbtn.IsEnabled = true;
                }
                else
                {
                    // form fields is valid you can submit
                    //do the call
                    login(UserNAme, Password);
                }

            }
            else
            {
                // here we have to display the error msg before submission
                Responceformlbl.Text = "الرجاء تعبئة الحقول بالشكل الصحيح";
                lognbtn.IsEnabled = true;
            }

        }

      
        public async void login(string Username, string Password)
        {
            try
            {
                var loginurl = string.Format("http://omnapp.signbook.co/api/SitesApis/login?UserName={0}&Password={1}", Username, Password);

                //var loginurl = "http://omnapp.signbook.co/api/SitesApis/login?UserName=Hisham&Password=Aa1234567";
                //in active user "http://omnapp.signbook.co/api/SitesApis/login?UserName=Ashraf&Password=Ashraf";
                //active user  "http://omnapp.signbook.co/api/SitesApis/login?UserName=Hisham&Password=Aa1234567";


                // WebClient client = new WebClient();
                // client.UploadString(loginurl, inputJson);
                string UserName = "";
                string UsedID = "";
                


                using (var clientt = new WebClient())
                {

                    string result = clientt.DownloadString(loginurl);



                    string[] FirstPart = result.Split('[');

                    string resultsett = FirstPart[1];

                    string[] FinalJSON = resultsett.Split(']');

                    string FinalJSONstring = FinalJSON[0];

                    JObject SerialaizedJSON = JObject.Parse(FinalJSONstring);
                    string isActiveUser = SerialaizedJSON["Status"].ToString();

                    if (isActiveUser == "1")
                    {
                        // activated user 
                        UserName = SerialaizedJSON["UserName"].ToString();
                        UsedID = SerialaizedJSON["UserID"].ToString();

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

                        if (Application.Current.Properties.ContainsKey("UserName") && Application.Current.Properties.ContainsKey("UserID"))
                        {

                            Application.Current.Properties["UserName"]= UserName;
                            Application.Current.Properties["UserID"]= UsedID;
                        }
                        else {
                            Application.Current.Properties.Add("UserName", UserName);
                            Application.Current.Properties.Add("UserID", UsedID);
                        }
                        //now we have to update the user Token
                        if (Application.Current.Properties.ContainsKey("UserToken"))
                        {
                            string UserId = Application.Current.Properties["UserID"].ToString();
                            string UsrTkn = Application.Current.Properties["UserToken"].ToString();
                            string UpdateUserToken = string.Format("http://omnapp.signbook.co/api/SitesApis/InsertUserToken?UserID={0}&UserToken={1}", UserId, UsrTkn);


                            using (var clienttl = new WebClient())
                            {

                                string resultl = clientt.DownloadString(UpdateUserToken);

                                //cases data exist
                                if (result.ToLower().Contains("error"))
                                {
                                    // we have to update the user 

                                }


                            }
                        }


                        //redirect to the 
                            var LoginViewModelobject = new LoginViewModel();
                        LoginViewModelobject.goToMainVideoPage();
                        

                    }
                    else {
                        // in active user
                        Responceformlbl.Text = "المستخدم غير مفعل يرجى المحاولة لاحقا";
                        lognbtn.IsEnabled = false;

                    }
                }

            }
            catch (Exception e)
            {
                //error happened
                Responceformlbl.Text = "كلمة المرور او اسم المستخدم خاطىء";
                lognbtn.IsEnabled = true;

            }


        }
        async void GoToSignup(object sender, EventArgs args)
        {
            //go to signup page
            LoginViewModel LVModel = new LoginViewModel();
            LVModel.goToSignupPage();

        }

        async void GoToForgetPass(object sender, EventArgs args)
        {
            //go to signup page
            LoginViewModel LVModel = new LoginViewModel();
            LVModel.GoToForgetPass();

        }



        protected override void OnDisappearing()
        {
            
        }

        
    }
}