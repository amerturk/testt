using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using Xamarin.Forms;
using System.IO;
using System.Globalization;

namespace WhiteMvvm.Converters
{
    public class ImageSourceConverter : IValueConverter
    {
        static readonly WebClient Client = new WebClient();
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return null;
            var byteArray = Client.DownloadData(value.ToString());
            return ImageSource.FromStream(() => new MemoryStream(byteArray));
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
