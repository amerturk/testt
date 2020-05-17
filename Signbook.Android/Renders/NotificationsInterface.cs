using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Signbook.Dependency;
using Signbook.Droid.Renders;
using Xamarin.Forms;

[assembly: Dependency(typeof(NotificationsInterface))]
namespace Signbook.Droid.Renders
{
    public class NotificationsInterface : INotificationsInterface
    {
        public bool registeredForNotifications()
        {
            var nm = NotificationManagerCompat.From(Android.App.Application.Context);
            bool enabled = nm.AreNotificationsEnabled();
            return enabled;
        }
    }
}