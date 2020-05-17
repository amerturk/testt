using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace Signbook.ViewModels
{
    public class SuggestionViewModel : BaseViewModel
    {
        #region Feilds
        private string _name;
        private string _email;
        private string _message;
        #endregion
        #region Properties

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }
        public string Message
        {
            get { return _message; }
            set { _message = value; OnPropertyChanged(); }
        }
        #endregion
        #region Command
        public ICommand SendSelectedCommand { get; set; }
        public ICommand OnCloseTap { get; set; }
        #endregion
        #region Page Event
        public SuggestionViewModel()
        {
            SendSelectedCommand = new Command(SendSelected);

            OnCloseTap = new Command(CloseTap);
        }

        private async void CloseTap(object obj)
        {
            await NavigationService.Navigation.PopAsync();
        }
        #endregion
        #region Command Implementation
        private async void SendSelected(object obj)
        {
            if (!string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Message))
            {
                IsBusy = true;
                var client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential("ssignbook@gmail.com", "Adm!n123"),
                    EnableSsl = true
                };
                //client.Send("ssignbook@gmail.com", "Amer.Turk@iHorizons.com", "Signbook Suggestion", Message.ToString());
                client.Send("ssignbook@gmail.com", "hayat.signbook@gmail.com", "Signbook Suggestion",
                    "Name: " + Name.ToString() + "\n" +
                    "Email: " + Email.ToString() + "\n" +
                    "Message: " + Message.ToString());
                await NavigationService.Navigation.PopAsync();
                IsBusy = false;
            }
        }

        #endregion
        #region Methods
        protected override Task OnDisappearing()
        {
            Name = "";
            Email = "";
            Message = "";

            return base.OnDisappearing();
        }
        #endregion

    }
}
