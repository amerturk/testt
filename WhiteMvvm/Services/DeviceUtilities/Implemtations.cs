using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace WhiteMvvm.Services.DeviceUtilities
{
   
    public class BrowserService : IBrowser
    {
        Task IBrowser.OpenAsync(string uri)
             => Xamarin.Essentials.Browser.OpenAsync(uri);

        Task IBrowser.OpenAsync(string uri, Xamarin.Essentials.BrowserLaunchMode launchMode)
             => Xamarin.Essentials.Browser.OpenAsync(uri, launchMode);

        Task IBrowser.OpenAsync(Uri uri)
             => Xamarin.Essentials.Browser.OpenAsync(uri);

        Task IBrowser.OpenAsync(Uri uri, Xamarin.Essentials.BrowserLaunchMode launchMode)
             => Xamarin.Essentials.Browser.OpenAsync(uri, launchMode);
        Task IBrowser.OpenAsync(Uri uri, Xamarin.Essentials.BrowserLaunchOptions launchOptions)
            => Xamarin.Essentials.Browser.OpenAsync(uri, launchOptions);
    }
   
    public class ConnectivityService : IConnectivity
    {

        bool IConnectivity.IsConnected
        {
            get
            {
                if (!CheckInternetConnection())
                    return false;
                return true;
            }
        }

        Xamarin.Essentials.NetworkAccess IConnectivity.NetworkAccess => Xamarin.Essentials.Connectivity.NetworkAccess;

        IEnumerable<Xamarin.Essentials.ConnectionProfile> IConnectivity.ConnectionProfiles
             => Xamarin.Essentials.Connectivity.ConnectionProfiles;

        event EventHandler<Xamarin.Essentials.ConnectivityChangedEventArgs> IConnectivity.ConnectivityChanged
        {
            add => Xamarin.Essentials.Connectivity.ConnectivityChanged += value;
            remove => Xamarin.Essentials.Connectivity.ConnectivityChanged -= value;
        }
        public bool CheckInternetConnection()
        {
            var checkUrl = "http://google.com";
            try
            {
                var iNetRequest = (HttpWebRequest)WebRequest.Create(checkUrl);
                iNetRequest.Timeout = 5000;
                var iNetResponse = iNetRequest.GetResponse();
                iNetResponse.Close();
                return true;
            }
            catch (WebException)
            {
                return false;
            }
        }
    }

    public class FileSystemService : IFileSystem
    {

        Task<Stream> IFileSystem.OpenAppPackageFileAsync(string filename)
             => Xamarin.Essentials.FileSystem.OpenAppPackageFileAsync(filename);

        string IFileSystem.CacheDirectory
             => Xamarin.Essentials.FileSystem.CacheDirectory;

        string IFileSystem.AppDataDirectory
             => Xamarin.Essentials.FileSystem.AppDataDirectory;
    }
   
    public class PreferencesService : IPreferences
    {

        bool IPreferences.ContainsKey(string key)
             => Xamarin.Essentials.Preferences.ContainsKey(key);

        void IPreferences.Remove(string key)
             => Xamarin.Essentials.Preferences.Remove(key);

        void IPreferences.Clear()
             => Xamarin.Essentials.Preferences.Clear();

        string IPreferences.Get(string key, string defaultValue)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        bool IPreferences.Get(string key, bool defaultValue)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        int IPreferences.Get(string key, int defaultValue)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        double IPreferences.Get(string key, double defaultValue)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        float IPreferences.Get(string key, float defaultValue)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        long IPreferences.Get(string key, long defaultValue)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        void IPreferences.Set(string key, string value)
             => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, bool value)
             => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, int value)
             => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, double value)
             => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, float value)
             => Xamarin.Essentials.Preferences.Set(key, value);

        void IPreferences.Set(string key, long value)
             => Xamarin.Essentials.Preferences.Set(key, value);

        bool IPreferences.ContainsKey(string key, string sharedName)
             => Xamarin.Essentials.Preferences.ContainsKey(key, sharedName);

        void IPreferences.Remove(string key, string sharedName)
             => Xamarin.Essentials.Preferences.Remove(key, sharedName);

        void IPreferences.Clear(string sharedName)
             => Xamarin.Essentials.Preferences.Clear(sharedName);

        string IPreferences.Get(string key, string defaultValue, string sharedName)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        bool IPreferences.Get(string key, bool defaultValue, string sharedName)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        int IPreferences.Get(string key, int defaultValue, string sharedName)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        double IPreferences.Get(string key, double defaultValue, string sharedName)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        float IPreferences.Get(string key, float defaultValue, string sharedName)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        long IPreferences.Get(string key, long defaultValue, string sharedName)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        void IPreferences.Set(string key, string value, string sharedName)
             => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, bool value, string sharedName)
             => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, int value, string sharedName)
             => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, double value, string sharedName)
             => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, float value, string sharedName)
             => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        void IPreferences.Set(string key, long value, string sharedName)
             => Xamarin.Essentials.Preferences.Set(key, value, sharedName);

        DateTime IPreferences.Get(string key, DateTime defaultValue)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue);

        void IPreferences.Set(string key, DateTime value)
             => Xamarin.Essentials.Preferences.Set(key, value);

        DateTime IPreferences.Get(string key, DateTime defaultValue, string sharedName)
             => Xamarin.Essentials.Preferences.Get(key, defaultValue, sharedName);

        void IPreferences.Set(string key, DateTime value, string sharedName)
             => Xamarin.Essentials.Preferences.Set(key, value, sharedName);
    }
   
}
