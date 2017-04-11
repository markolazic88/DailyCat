namespace DailyCat.Droid
{
    using System;

    using Android.App;
    using Android.OS;
    using Android.Runtime;

    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Services;
    using DailyCat.Droid.Services;
    using DailyCat.View;
    using DailyCat.ViewModel;

    using GalaSoft.MvvmLight.Ioc;

    using Microsoft.Practices.ServiceLocation;

    using Plugin.CurrentActivity;
    using Plugin.Toasts;

    using Xamarin.Forms;

    [Application(Label = "DailyCat")]
    public class Application : Android.App.Application, Android.App.Application.IActivityLifecycleCallbacks
    {
        /// <summary>
        /// Must implement this constructor for subclassing the application class.
        /// Will act as a global application class throughout the app.
        /// </summary>
        /// <param name="javaReference">pointer to java</param>
        /// <param name="transfer">transfer enumeration</param>
        public Application(IntPtr javaReference, JniHandleOwnership transfer)
            : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            try
            {
                RegisterDependencyService();
                RegisterGlobalIoc();
                IocViewModel.RegisterViewModelSimpleIoc();
                IocView.RegisterViewSimpleIoc();
            }
            catch (Exception exception)
            {
                System.Diagnostics.Debug.WriteLine(exception);
            }

            base.OnCreate();
            this.RegisterActivityLifecycleCallbacks(this);
        }


        public override void OnTerminate()
        {
            base.OnTerminate();
            this.UnregisterActivityLifecycleCallbacks(this);
        }

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityDestroyed(Activity activity)
        {
        }

        public void OnActivityPaused(Activity activity)
        {
        }

        public void OnActivityResumed(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {
        }

        public void OnActivityStarted(Activity activity)
        {
            CrossCurrentActivity.Current.Activity = activity;
        }

        public void OnActivityStopped(Activity activity)
        {
        }

        private static void RegisterDependencyService()
        {
            DependencyService.Register<ToastNotification>();
        }

        private static void RegisterGlobalIoc()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SimpleIoc.Default.Register<IRestService, RestService>();
            SimpleIoc.Default.Register<IDataService, DataService>();
            SimpleIoc.Default.Register<IConfigurationService, ConfigurationService>();

            SimpleIoc.Default.Register<IApplicationService, ApplicationService>();
        }
    }
}