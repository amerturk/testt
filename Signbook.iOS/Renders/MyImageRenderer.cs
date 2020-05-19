using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Foundation;
using Signbook.Controls;
using Signbook.iOS.Renders;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

//[assembly: ExportRenderer(typeof(Image), typeof(MyImageRenderer))]

namespace Signbook.iOS.Renders
{
    public class MyImageRenderer : ImageRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try
            {
                base.OnElementPropertyChanged(sender, e);

                if (e.PropertyName == "IsLoading")
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (Element != null && Element.Source != null)
                        { 
                        var handler = Xamarin.Forms.Internals.Registrar.Registered.GetHandlerForObject<IImageSourceHandler>(Element.Source);
                        if (!Element.IsLoading && Control.Image == null && handler is ImageLoaderSourceHandler)
                        {
                            var imageLoader = Element.Source as UriImageSource;
                            var imgPath = imageLoader.Uri.OriginalString;
                            NSUrlSession session = NSUrlSession.SharedSession;
                            var task = session.CreateDataTask(new NSUrl(imgPath), (data, response, error) =>
                            {

                                if (data != null)
                                {
                                    Control.Image = UIImage.LoadFromData(data);
                                }

                            });
                            task.Resume();
                        }
                    }
                    });
                }
            }
            catch (Exception ex)
            {             
            }
        
        }
    }
}