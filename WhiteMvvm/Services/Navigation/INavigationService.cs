using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WhiteMvvm.Bases;
using Xamarin.Forms;

namespace WhiteMvvm.Services.Navigation
{
    public interface INavigationService
    {
        event PagePopupEventHandler PagePopup;
        INavigation Navigation { get; }
        Page GetCurrentPage { get; }
        BaseViewModel GetCurrentViewModel();
        Task PopModelAsync(object parameter = null);
        Task PopPageFromModelAsync<TViewModel>() where TViewModel : BaseViewModel;
        Task NavigateToAsync<TViewModel>(object parameter = null) where TViewModel : BaseViewModel;
        Task NavigateToModalAsync<TViewModel>(object parameter = null, bool isNavigationPage = false) where TViewModel : BaseViewModel;
        Task NavigateToTabbedAsync(IList<PageContainer> pageContainers, TabbedPage tabbedPage = null, bool hasNavBar = false);
        bool AddPageToTabbedPage(PageContainer pageContainer);
        Task NavigateToMasterDetailsAsync(PageContainer master, PageContainer detail, MasterDetailPage masterDetailPage = null, bool hasNavBar = false);
        bool ChangeDetailPage(PageContainer pageContainer, object parameter);
        void OnPagePopup(object sender, EventArgs e);

    }
}
