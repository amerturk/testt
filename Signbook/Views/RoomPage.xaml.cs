using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Signbook.ViewModels;
using WhiteMvvm.Bases;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Signbook.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RoomPage : BaseContentPage
    {
        public RoomPage()
        {
            InitializeComponent();
        }

        protected override void OnDisappearing()
        {
            ((RoomViewModel)ViewModel).EndSessionCommand.Execute(false);
            base.OnDisappearing();
        }
    }
}