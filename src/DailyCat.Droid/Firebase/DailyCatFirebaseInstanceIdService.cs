namespace DailyCat.Droid.Firebase
{
    using Android.App;
    using Android.Util;

    using global::Firebase.Iid;

    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class DailyCatFirebaseInstanceIdService : FirebaseInstanceIdService
    {
        private const string Tag = "DailyCatFirebaseInstanceIdService";

        public override void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(Tag, "Refreshed token: " + refreshedToken);
            this.SendRegistrationToServer(refreshedToken);
        }

        private void SendRegistrationToServer(string token)
        {
            // Add custom implementation, as needed.
        }
    }
}