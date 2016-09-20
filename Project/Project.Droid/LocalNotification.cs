using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Project.Droid;

[assembly: Dependency(typeof(LocalNotification))]
namespace Project.Droid
{
    public class LocalNotification : Java.Lang.Object, ILocalNotification
    {
        public void ShowNotif(string subject, string text)
        {
            // Instantiate the builder and set notification elements:
            Notification.Builder builder = new Notification.Builder(Android.App.Application.Context)
                .SetContentTitle(subject)
                .SetSmallIcon(Resource.Drawable.mapua_logo)
                .SetVibrate(new long[] { 1000, 1000, 1000, 10000 })
                .SetPriority((int)NotificationPriority.High)
                ;

            Notification.BigTextStyle textstyle = new Notification.BigTextStyle();
            textstyle.BigText(text);
            builder.SetStyle(textstyle);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                Android.App.Application.Context.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}