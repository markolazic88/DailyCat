namespace DailyCat.View
{
    using DailyCat.Common.Interfaces;

    using DLToolkit.Forms.Controls;

    using GalaSoft.MvvmLight.Ioc;

    using Xamarin.Forms;

    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();

            this.NavigationService = SimpleIoc.Default.GetInstance<IExtendedNavigationService>();
            this.MainPage = this.NavigationService.GetRootPage() as NavigationPage;

            FlowListView.Init();
        }

        private IExtendedNavigationService NavigationService { get; set; }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
