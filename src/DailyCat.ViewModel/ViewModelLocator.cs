namespace DailyCat.ViewModel
{
    using Microsoft.Practices.ServiceLocation;

    public class ViewModelLocator
    {       
        public ViewModelLocator()
        {
        }

        public MainPageViewModel MainPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<MainPageViewModel>(); }
        }

        public BrowsePageViewModel BrowsePageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<BrowsePageViewModel>(); }
        }

        public LikedPageViewModel LikedPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<LikedPageViewModel>(); }
        }

        public CatDetailPageViewModel CatDetailPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<CatDetailPageViewModel>(); }
        }

        public SettingsPageViewModel SettingsPageViewModel
        {
            get { return ServiceLocator.Current.GetInstance<SettingsPageViewModel>(); }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}