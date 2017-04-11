namespace DailyCat.View.Pages
{
    using Xamarin.Forms;

    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            this.InitializeComponent();

            Xamarin.Forms.PlatformConfiguration.AndroidSpecific.TabbedPage.DisableSwipePaging(this.On<Xamarin.Forms.PlatformConfiguration.Android>());
        }
    }
}
