namespace DailyCat.Droid.Activities
{
    using Android.App;
    using Android.Content;
    using Android.Content.PM;
    using Android.OS;

    [Activity(Label = "DailyCat", Icon = "@drawable/ic_launcher", Theme = "@style/SplashTheme", 
            ScreenOrientation = ScreenOrientation.Portrait, MainLauncher = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            var intent = new Intent(this, typeof(MainActivity));
            this.StartActivity(intent);
            this.Finish();
        }
    }
}