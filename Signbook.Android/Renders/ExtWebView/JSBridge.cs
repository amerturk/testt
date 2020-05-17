using System;
using System.Collections.Generic;
using System;
using Android.Webkit;
using Java.Interop;


namespace Signbook.Droid.Renders.ExtWebView
{
    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<ExtWebViewRenderer> _weakReference;

        public JSBridge(ExtWebViewRenderer extWebViewRenderer)
        {
            _weakReference = new WeakReference<ExtWebViewRenderer>(extWebViewRenderer);
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            if (_weakReference != null && _weakReference.TryGetTarget(out var extWebViewRenderer))
            {
                if (extWebViewRenderer.Element is Controls.ExtWebView extWebView)
                {
                    extWebView.InvokeAction(data);
                }
            }
        }
    }
}
