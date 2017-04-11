namespace DailyCat.ViewModel
{
    using DailyCat.Common.Interfaces;

    using GalaSoft.MvvmLight.Ioc;

    public static class IocViewModel
    {
        public static void RegisterViewModelSimpleIoc()
        {
            SimpleIoc.Default.Register<ISessionState, SessionState>();

            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<BrowsePageViewModel>();
            SimpleIoc.Default.Register<LikedPageViewModel>();
            SimpleIoc.Default.Register<CatDetailPageViewModel>();
            SimpleIoc.Default.Register<SettingsPageViewModel>();
        }
    }
}
