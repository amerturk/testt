using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using Signbook.Controls;
using Signbook.Droid.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using WebView = Xamarin.Forms.WebView;



[assembly: ExportRenderer(typeof(VideoCallWebView), typeof(VideoCallWebViewDroid))]
namespace Signbook.Droid.Renders
{
    public class VideoCallWebViewDroid : WebViewRenderer
    {
        readonly Activity _context;
        
        public VideoCallWebViewDroid(Context context) : base(context)
        {
            _context = context as Activity;
        }



        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);
            Control.Settings.JavaScriptEnabled = true;
            Control.SetWebChromeClient(new VideoCallWebClient(_context));
            Control.ClearCache(true);
        }



        public class VideoCallWebClient : WebChromeClient
        {
            readonly Activity _context;



            public VideoCallWebClient(Activity context)
            {
                _context = context;
            }
            public override void OnPermissionRequest(PermissionRequest request)
            {
                _context.RunOnUiThread(() => {
                    request.Grant(request.GetResources());
                });
            }
        }
    }
}