using System;
using WhiteMvvm.Services.Resolve;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;


namespace WhiteMvvm.Bases

{
    public class BaseContentPage : ContentPage
    {
        private static IReflectionResolve _resolve;
        protected BaseContentPage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            _resolve = BaseLocator.Instance.Resolve<IReflectionResolve>();
            Xamarin.Forms.NavigationPage.SetHasNavigationBar(this, false);
        }
        protected BaseViewModel ViewModel => BindingContext as BaseViewModel;

        private static readonly Random RandomGen = new Random();

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.InternalOnAppeared();
            ViewModel?.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel?.OnDisappearing();
            ViewModel?.OnPagePopup();
        }
        protected override bool OnBackButtonPressed()
        {
            var result = ViewModel?.HandleBackButton();
            return result ?? false;
        }

        public static readonly BindableProperty IsColoringDesignProperty = BindableProperty.CreateAttached("IsColoringDesign",
            typeof(bool), typeof(BaseContentPage),
            default(bool), propertyChanged: OnIsColoringDesignChanged);

        private static void OnIsColoringDesignChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            if ((!(bindable is BaseContentPage page)))
            {
                return;
            }
            if ((bool)newvalue)
            {
                page.Appearing += Page_Appearing;
            }
            else
            {
                page.Appearing -= Page_Appearing;
            }
        }

        private static void Page_Appearing(object sender, EventArgs e)
        {
#if DEBUG
            if (sender.GetType().IsSubclassOf(typeof(ContentPage)))
            {
                IterateChildren((sender as ContentPage)?.Content);
            }
            else
            {
                if (!(sender is IViewContainer<Xamarin.Forms.Page> container)) return;
                foreach (var item in container.Children)
                {
                    if (item is ContentPage page)
                    {
                        IterateChildren(page.Content);
                    }
                }
            }
#endif
        }
        public static void SetIsColoringDesign(BindableObject bindable, bool value)
        {
            bindable.SetValue(IsColoringDesignProperty, value);
        }
        public static bool GetIsColoringDesign(BindableObject bindable)
        {
            return (bool)bindable.GetValue(IsColoringDesignProperty);
        }
        private static Color GetRandomColor()
        {
            var color = Color.FromRgb((byte)RandomGen.Next(0, 255), (byte)RandomGen.Next(0, 255), (byte)RandomGen.Next(0, 255));
            return color;
        }
        private static void IterateChildren(Element content)
        {
            if (content == null)
                return;
            if (content.GetType().IsSubclassOf(typeof(Layout)))
            {
                ((Layout)content).BackgroundColor = GetRandomColor();

                foreach (var item in ((Layout)content).Children)
                {
                    IterateChildren(item);
                }
            }
            else if (content.GetType().IsSubclassOf(typeof(View)))
            {
                ((View)content).BackgroundColor = GetRandomColor();
            }
        }
        public static readonly BindableProperty AutoWireViewModelProperty =
            BindableProperty.CreateAttached("AutoWireViewModel", typeof(bool), typeof(BaseContentPage), default(bool),
                propertyChanged: OnAutoWireViewModelChanged);
        private static void OnAutoWireViewModelChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if ((!(bindable is BaseContentPage page)))
            {
                return;
            }

            page.BindingContext = _resolve.CreateViewModel(page.GetType());
        }
        public static bool GetAutoWireViewModel(BindableObject bindable)
        {
            return (bool)bindable.GetValue(AutoWireViewModelProperty);
        }
        public static void SetAutoWireViewModel(BindableObject bindable, bool value)
        {
            bindable.SetValue(AutoWireViewModelProperty, value);
        }
       
    }
}
