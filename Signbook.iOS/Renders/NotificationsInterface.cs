using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Signbook.Dependency;
using Signbook.iOS.Renders;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(NotificationsInterface))]
namespace Signbook.iOS.Renders
{
    public class NotificationsInterface : INotificationsInterface
    {
        public bool registeredForNotifications()
        {
            try
            {
                UIUserNotificationType types = UIApplication.SharedApplication.CurrentUserNotificationSettings.Types;

                if (types == UIUserNotificationType.None)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception exception)
            {
                var properties = new Dictionary<string, string>
                    {
                           { "registeredForNotifications", "NotificationsInterfaceiOS" }
                    };
               // Crashes.TrackError(exception, properties);
                return false;
            }
        }
    }
}