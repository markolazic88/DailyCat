namespace DailyCat.iOS
{
    using System;

    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Services;
    using DailyCat.iOS.Services;
    using DailyCat.View;
    using DailyCat.ViewModel;

    using GalaSoft.MvvmLight.Ioc;

    using Microsoft.Practices.ServiceLocation;

    using Plugin.Toasts;

    using UIKit;

    using Xamarin.Forms;

    public class Application
    {
        // This is the main entry point of the application.
        static void Main(string[] args)
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

            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");
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