using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.FirebasePushNotification;
using Firebase;
using Android.Support.V4.Content;
using Android;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Acr.UserDialogs.Infrastructure;
using Plugin.CurrentActivity;
using Acr.UserDialogs;
using Xamarin.Forms;
using Android.Content;
using static Signbook.App;
using Signbook.Dependency;
using Application = Android.App.Application;

namespace Signbook.Droid
{
    //[Activity(Label = "Signbook", Icon = "@drawable/SBLogo", Theme = "@style/MainTheme", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [Activity(Label = "Signbook", Icon = "@drawable/SBLogo", Theme = "@style/MyTheme.Splash", MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static readonly int RequestCameraPermissionCode = 0;
        private static readonly string[] RequiredCameraPermissions = { Manifest.Permission.Camera };
        bool isNotification = false;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
           // global::Xamarin.Agora.Full.Forms.AgoraServiceDroid.Init();
            base.OnCreate(savedInstanceState);
            UserDialogs.Init(this);
            FirebasePushNotificationManager.ProcessIntent(this, Intent);
            ZXing.Net.Mobile.Forms.Android.Platform.Init();
            ZXing.Mobile.MobileBarcodeScanner.Initialize(Application);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            SetTheme(Resource.Style.MainTheme);
            RequestCameraPermissionIfNecessary();
            


            LoadApplication(new App());

            

        }

        public void ClearNotifications()
        {
            var notificationManager = NotificationManagerCompat.From(Android.App.Application.Context);
            notificationManager.CancelAll();
        }
        protected override void OnNewIntent(Intent intent)
        {
            
            base.OnNewIntent(intent);
            FirebasePushNotificationManager.ProcessIntent(this, intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            //ZXing.Net.Mobile.Forms.Android.PermissionsHandler.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
        /// <summary>
        /// This is a workaround for the broken implementation of the QR code reader.
        /// </summary>
        private void RequestCameraPermissionIfNecessary()
        {
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera) == Permission.Granted)
            {
                //Log.Debug(LoggerTag, "Camera permission is already granted for app.");
                return;
            }

            if (ActivityCompat.ShouldShowRequestPermissionRationale(this, Manifest.Permission.Camera))
            {
                var layout = FindViewById(Android.Resource.Id.Content);
                Snackbar.Make(layout,
                        Resource.String.permission_camera_rationale,
                        Snackbar.LengthIndefinite)
                    .SetAction(Resource.String.ok,
                        delegate
                        {
                            ActivityCompat.RequestPermissions(
                                this,
                                RequiredCameraPermissions,
                                RequestCameraPermissionCode);
                        }
                    ).Show();
            }
            else
            {
                ActivityCompat.RequestPermissions(this, RequiredCameraPermissions, RequestCameraPermissionCode);
            }
        }


    }
}