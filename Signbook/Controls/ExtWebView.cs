using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Signbook.Controls
{
    public class ExtWebView : WebView
    {
        Action<string> action;
        /// <summary>
        /// Bindable property for <see cref="UnScrollable"/>.
        /// </summary>
        public static readonly BindableProperty UnScrollableProperty =
            BindableProperty.Create(
                nameof(UnScrollable),
                typeof(bool),
                typeof(ExtWebView),
                defaultValue: false);
        /// <summary>
        /// Gets or sets the command executed when the web view content requests entering full-screen.
        /// The command is passed a <see cref="View"/> containing the content to display.
        /// The default command displays the content as a modal page.
        /// </summary>
        public bool UnScrollable
        {
            get => (bool)GetValue(UnScrollableProperty);
            set => SetValue(UnScrollableProperty, value);
        }
        /// <summary>
        /// Bindable property for <see cref="EnterFullScreenCommand"/>.
        /// </summary>
        public static readonly BindableProperty EnterFullScreenCommandProperty =
            BindableProperty.Create(
                nameof(EnterFullScreenCommand),
                typeof(ICommand),
                typeof(ExtWebView),
                defaultValue: new Command(async (view) => await DefaultEnterAsync((View)view)));
        /// <summary>
        /// Bindable property for <see cref="ExitFullScreenCommand"/>.
        /// </summary>
        public static readonly BindableProperty ExitFullScreenCommandProperty =
            BindableProperty.Create(
                nameof(ExitFullScreenCommand),
                typeof(ICommand),
                typeof(ExtWebView),
                defaultValue: new Command(async (view) => await DefaultExitAsync()));
        /// <summary>
        /// Gets or sets the command executed when the web view content requests entering full-screen.
        /// The command is passed a <see cref="View"/> containing the content to display.
        /// The default command displays the content as a modal page.
        /// </summary>
        public ICommand EnterFullScreenCommand
        {
            get => (ICommand)GetValue(EnterFullScreenCommandProperty);
            set => SetValue(EnterFullScreenCommandProperty, value);
        }
        /// <summary>
        /// Bindable property for <see cref="LinkClickCommand"/>.
        /// </summary>
        public static readonly BindableProperty LinkClickCommandProperty =
            BindableProperty.Create(
                nameof(LinkClickCommand),
                typeof(ICommand),
                typeof(ExtWebView),
                defaultValue: new Command(async (view) => await DefaultEnterAsync((View)view)));
        /// <summary>
        /// Gets or sets the command executed when the web view content requests entering full-screen.
        /// The command is passed a <see cref="View"/> containing the content to display.
        /// The default command displays the content as a modal page.
        /// </summary>
        public ICommand LinkClickCommand
        {
            get => (ICommand)GetValue(LinkClickCommandProperty);
            set => SetValue(LinkClickCommandProperty, value);
        }
        /// <summary>
        /// Gets or sets the command executed when the web view content requests exiting full-screen.
        /// The command is passed no parameters.
        /// The default command pops a modal page off the navigation stack.
        /// </summary>
        public ICommand ExitFullScreenCommand
        {
            get => (ICommand)GetValue(ExitFullScreenCommandProperty);
            set => SetValue(ExitFullScreenCommandProperty, value);
        }
        private static async Task DefaultEnterAsync(View view)
        {
            var page = new ContentPage
            {
                Content = view,

            };
            NavigationPage.SetHasNavigationBar(page, false);
            await Application.Current.MainPage.Navigation.PushAsync(page, false);
        }
        private static async Task DefaultExitAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync(false);
        }
        public void Cleanup()
        {
            action = null;
            LinkClickCommand = null;
        }
        public void InvokeAction(string data)
        {
            if (data == null)
            {
                return;
            }
            LinkClickCommand?.Execute(data);
        }
    }
}
