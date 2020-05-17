using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Signbook.Controls;
using Signbook.Droid.Renders.ExtWebView;
using WhiteMvvm.Bases;
using WhiteMvvm.Services.Dialog;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtWebView), typeof(ExtWebViewRenderer))]
namespace Signbook.Droid.Renders.ExtWebView
{
    public class ExtWebViewRenderer : WebViewRenderer
    {
        private const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
        private readonly Context _context;
        private Signbook.Controls.ExtWebView _extWebView;
        private static IDialogService _dialogService;
        private static Android.Webkit.WebView _webView;

        public ExtWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }
        ///// <summary>
        ///// Initializes a new instance of the <see cref="FullScreenEnabledWebViewRenderer"/> class.
        ///// </summary>
        ///// <param name="context">An Android context.</param>

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            _dialogService = BaseLocator.Instance.Resolve<IDialogService>();
            if (e.OldElement is Controls.ExtWebView oldWebView && Control != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                oldWebView.Cleanup();
            }
            if (e.NewElement is Controls.ExtWebView newWebView)
            {
                if (Control == null)
                {
                    var webView = new Android.Webkit.WebView(_context);
                    webView.Settings.JavaScriptEnabled = true;
                    webView.SetWebViewClient(new ExtWebViewClient(newWebView, $"javascript: {JavascriptFunction}"));
                    //webView.SetWebChromeClient(new Android.Webkit.WebChromeClient());

                    SetNativeControl(webView);
                }
                else
                {
                    _extWebView = newWebView;
                    Control.Settings.JavaScriptEnabled = true;

                    Control.SetWebViewClient(new ExtWebViewClient(newWebView, $"javascript: {JavascriptFunction}"));
                    //Control.SetWebChromeClient(new Android.Webkit.WebChromeClient());

                    Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");
                    Control.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
            }
        }
        /// <summary>
        /// Creates a <see cref="FormsWebChromeClient"/> that implements the necessary callbacks to support
        /// full-screen operation.
        /// </summary>
        /// <returns>A <see cref="FullScreenEnabledWebChromeClient"/>.</returns>
        protected override FormsWebChromeClient GetFormsWebChromeClient()
        {
            var client = new FullScreenEnabledWebChromeClient();
            client.EnterFullscreenRequested += OnEnterFullscreenRequested;
            client.ExitFullscreenRequested += OnExitFullscreenRequested;
            return client;
        }
        /// <summary>
        /// Executes the full-screen command on the <see>
        ///     <cref>FullScreenEnabledWebView</cref>
        /// </see>
        /// if available. The
        /// Xamarin view to display in full-screen is sent as a command parameter.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="eventArgs">The event arguments.</param>
        private void OnEnterFullscreenRequested(
            object sender,
            EnterFullScreenRequestedEventArgs eventArgs)
        {
            if (_extWebView.EnterFullScreenCommand != null && _extWebView.EnterFullScreenCommand.CanExecute(null))
            {
                _extWebView.EnterFullScreenCommand.Execute(eventArgs.View.ToView());
            }
        }
        /// <summary>
        /// Executes the exit full-screen command on th e <see>
        ///     <cref>FullScreenEnabledWebView</cref>
        /// </see>
        /// if available.
        /// The command is passed no parameters.
        /// </summary>
        /// <param name="sender">The event sender.</param>
        /// <param name="eventArgs">The event arguments.</param>
        private void OnExitFullscreenRequested(object sender, EventArgs eventArgs)
        {
            if (_extWebView.ExitFullScreenCommand != null && _extWebView.ExitFullScreenCommand.CanExecute(null))
            {
                _extWebView.ExitFullScreenCommand.Execute(null);
            }
        }
        class ExtWebViewClient : Android.Webkit.WebViewClient
        {
            private readonly Signbook.Controls.ExtWebView _extWebView;
            private readonly string _javascript;

            public ExtWebViewClient(Signbook.Controls.ExtWebView element, string javascript)
            {
                _extWebView = element;
                _javascript = javascript;
            }
            public override async void OnPageFinished(Android.Webkit.WebView view, string url)
            {
                try
                {
                    _webView = view;
                    if (_extWebView != null)
                    {
                        view.Settings.JavaScriptEnabled = true;
                        await Task.Delay(100);
                        var result = await _extWebView.EvaluateJavaScriptAsync("(function(){return document.body.scrollHeight;})()");
                        if (int.TryParse(result, out var height))
                        {
                            _extWebView.HeightRequest = height + 30;
                        }
                        else
                        {
                            _extWebView.HeightRequest = view.ContentHeight;
                        }
                        view.EvaluateJavascript(_javascript, null);
                    }
                    base.OnPageFinished(view, url);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}