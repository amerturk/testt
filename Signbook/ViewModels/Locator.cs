using Signbook.Services.News;
using Signbook.Services.NewsDetailed;
using System;
using System.Collections.Generic;
using System.Text;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.DeviceUtilities;

namespace Signbook.ViewModels
{
    public class Locator
    {
        public static void Init()
        {
            BaseLocator.Instance.Register<IBrowser, BrowserService>();
            BaseLocator.Instance.Register<INewsService, NewsService>();
            BaseLocator.Instance.Register<INewsDetailedService, NewsDetailedService>();
            BaseLocator.Instance.Register<NewsWindowViewModel>();
            BaseLocator.Instance.Register<MainViewModel>();
            BaseLocator.Instance.Register<NewsMainViewModel>();
            BaseLocator.Instance.Register<NewsViewModel>();
        }
    }
}
