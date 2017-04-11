namespace DailyCat.View
{
    using System;

    using DailyCat.Common;
    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Services;
    using DailyCat.ViewModel;
    using DailyCat.ViewModel.Services;

    using GalaSoft.MvvmLight.Ioc;

    public class IocView
    {
        public static void RegisterViewSimpleIoc()
        {
            SimpleIoc.Default.Register<NavigationDictionary<Type>>(
                () =>
                    new NavigationDictionary<Type>
                    {
                        { ViewModelManager.NavigationPageKey.Main, typeof(Pages.MainPage) },
                        { ViewModelManager.NavigationPageKey.Browse, typeof(Pages.BrowsePage) },
                        { ViewModelManager.NavigationPageKey.Liked, typeof(Pages.LikedPage) },
                        { ViewModelManager.NavigationPageKey.CatDetail, typeof(Pages.CatDetailPage) },
                        { ViewModelManager.NavigationPageKey.Settings, typeof(Pages.SettingsPage) }
                    },
                true);

            // Navigation
            SimpleIoc.Default.Register<IExtendedNavigationService>(
                () =>
                    new NavigationService(
                        SimpleIoc.Default.GetInstance<NavigationDictionary<Type>>(),
                        ViewModelManager.NavigationPageKey.Main),
                true);
        }
    }
}
