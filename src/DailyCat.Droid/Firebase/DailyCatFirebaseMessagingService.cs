namespace DailyCat.Droid.Firebase
{
    using Android.App;
    using Android.Content;
    using Android.Util;

    using DailyCat.Droid.Activities;

    using global::Firebase.Messaging;

    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class DailyCatFirebaseMessagingService : FirebaseMessagingService
    {
        private const string Tag = "DailyCatFirebaseMessagingService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            Log.Debug(Tag, "From: " + message.From);
            Log.Debug(Tag, "Notification Message Body: " + message.GetNotification().Body);

            // Uncomment to enable add notification while app is running
            //this.SendNotification(message.GetNotification().Title, message.GetNotification().Body);
        }

        private void SendNotification(string title, string body)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var notificationBuilder = new Notification.Builder(this)
                   .SetSmallIcon(Resource.Drawable.ic_stat_logo)
                   .SetContentTitle(title)
                   .SetContentText(body)
                   .SetAutoCancel(true)
                   .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);
            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}