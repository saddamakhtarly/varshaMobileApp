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
using Firebase.Messaging;

namespace MobileApp.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnNewToken(string p0)
        {
            
        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            string header = message.GetNotification().Body;
            string title = message.GetNotification().Title;
            new NotificationHelper().CreateNotification(title, header);
        }
    }
}