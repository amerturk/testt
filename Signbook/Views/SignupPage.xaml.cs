using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signbook.Controls;
using Signbook.ViewModels;
using WhiteMvvm.Bases;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;
using WhiteMvvm.Validations;
using System.Json;
using Xamarin.Forms.Internals;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Signbook.Transitions;
using WhiteMvvm.Utilities;

namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : BaseContentPage
    {
        
        public SignupPage()
        {
            InitializeComponent();

            /*var browser = new Xamarin.Forms.WebView();
            browser.Source= "https://mozilla.github.io/pdf.js/web/viewer.html";
            this.Content = browser;*/
            




    }
        
        

            async void RegisterUser(object sender, EventArgs args)
        {

            regbutton.IsEnabled = false;


            string Name = fName.Text;
            string Password = uPassword.Text;
            string UserEmail = uEmail.Text;
            string Phone = uPhone.Text;
            string CardIdNumber = IdNumber.Text;


            bool validfields = true;
            
            // validate form fields
            if (Name != null && Password != null && UserEmail != null && Phone != null && CardIdNumber != null)
            {



                if (Name.Length <= 0 || Password.Length <= 0 || UserEmail.Length <= 0 || Phone.Length <= 0 || CardIdNumber.Length <= 0)
                {
                    validfields = false;
                }
                if (Name.Length > 50 || Password.Length > 50 || UserEmail.Length > 50 || Phone.Length > 50 || CardIdNumber.Length > 50)
                {
                    validfields = false;
                }
                if (!IsValidEmail(UserEmail))
                {
                    validfields = false;
                }


                if (!validfields)
                {
                    // here we have to display the error msg before submission
                    Responceformlbl.Text = "الرجاء تعبئة الحقول بالشكل الصحيح";
                    regbutton.IsEnabled = true;
                }
                else
                {
                    // form fields is valid you can submit
                    //do the call
                    register(Name, Password, UserEmail, Phone, CardIdNumber);
                }

            }
            else {
                // here we have to display the error msg before submission
                if (CardIdNumber == null)
                {
                    Responceformlbl.Text = "الرجاء تعبئة رقم البطاقة";
                }
                if (Phone == null)
                {
                    Responceformlbl.Text = "الرجاء تعبئة رقم الهاتف";
                }
                if (Password == null)
                {
                    Responceformlbl.Text = "الرجاء تعبئة كلمة المرور";
                }
                if (Name == null)
                {
                    Responceformlbl.Text = "الرجاء تعبئة الاسم";
                }
                
                if (UserEmail == null)
                {
                    Responceformlbl.Text = "الرجاء تعبئة البريد الالكتروني";
                }
                
                

                regbutton.IsEnabled = true;
            }





        }

        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        protected override void OnDisappearing()
        {
            ((SignupViewModel)ViewModel).EndSessionCommand.Execute(false);
            base.OnDisappearing();
        }


        public void register(string nName, string pPassword,string eEmail, string pPhone, string idNumber)
        {
            
           
            var Registrationurl = string.Format("http://omnapp.signbook.co/api/SitesApis/Register?UserName={0}&Password={1}&Email={2}&FullName={0}&phone={3}&DisabilityType=0&Status=0&Balance=0&UserIDNumber={4}", nName, pPassword, eEmail, pPhone, idNumber);

            //var loginurl = "http://omnapp.signbook.co/api/SitesApis/Register?UserName=aaaa&Password=aaaa&Email=aaaa@www.cco&FullName=cccccc&DisabilityType=1&Status=0";
            // var RegistrationNew = "http://omnapp.signbook.co/api/SitesApis/Register?UserName=IDTest&Password=Test&Email=wwqwe@xxxsdd.com&FullName=%D8%AA%D8%AA%D8%AA&phone=88888&DisabilityType=0&Status=0&Balance=0&UserIDNumber=123456789  "


            // WebClient client = new WebClient();
            // client.UploadString(loginurl, inputJson);


            using (var clientt = new WebClient())
            {
                
                string result = clientt.DownloadString(Registrationurl);

                //cases data exist
                if (result.ToLower().Contains("exists"))
                {
                    // we have to update the user 
                    Responceformlbl.Text = "اسم المستخدم او البريد الالكتروني مستخدم مسبقا";
                    regbutton.IsEnabled = true;

                }
                // error
                else if (result.ToLower().Contains("error"))
                {
                    // we have to update the user 
                    Responceformlbl.Text = "حدث خطأ الرجاء المحاولة لاحقا";
                    regbutton.IsEnabled = true;

                }

                //sucess
                else {
                    Responceformlbl.Text = "";
                    ConfirmationLabel.Text = "تم التسجيل بنجاح عند الموافقة سيصلك بريد الكتروني";
                    
                }


            }

            


            //Console.ReadLine();
        }


       





    }
    }
