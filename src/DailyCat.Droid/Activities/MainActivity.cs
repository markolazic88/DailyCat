namespace DailyCat.Droid.Activities
{
    using Android.App;
    using Android.Content.PM;
    using Android.Gms.Common;
    using Android.OS;
    using Android.Util;

    using DailyCat.Common.Interfaces;
    using DailyCat.View;

    using global::Firebase.Iid;

    using GalaSoft.MvvmLight.Ioc;

    using Plugin.Toasts;

    using Xamarin.Forms;

    using Resource = DailyCat.Droid.Resource;

    [Activity(Label = "DailyCat", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme",
            ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
            ScreenOrientation = ScreenOrientation.Portrait,
            LaunchMode = LaunchMode.SingleTask)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private const string Tag = "Firebase";

        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            ToastNotification.Init(this, new PlatformOptions { Style = NotificationStyle.Snackbar });
            this.LoadApplication(new App());

            if (this.Intent.Extras != null)
            {
                foreach (var key in this.Intent.Extras.KeySet())
                {
                    var value = this.Intent.Extras.GetString(key);
                    Log.Debug(Tag, "Key: {0} Value: {1}", key, value);
                }
            }
  
            this.IsPlayServicesAvailable();
            this.LogFirebaseParameters();

            var applicationService = SimpleIoc.Default.GetInstance<IApplicationService>();
            applicationService.UpdateFirebaseTopicSubscriptions();
        }


        private bool IsPlayServicesAvailable()
        {
            var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    var error = GoogleApiAvailability.Instance.GetErrorString(resultCode);
                    Log.Debug(Tag, "Google Api error: " + error);
                }
                else
                {
                    Log.Debug(Tag, "This device is not supported");
                }

                return false;
            }
            else
            {
                Log.Debug(Tag, "Google Play Services is available.");
                return true;
            }
        }

        private void LogFirebaseParameters()
        {
            Log.Debug(Tag, "Google App Id: " + this.GetString(Resource.String.google_app_id));

            Log.Debug(Tag, "InstanceID token: " + FirebaseInstanceId.Instance.Token);
        }
    }
}

