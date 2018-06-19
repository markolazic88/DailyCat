namespace DailyCat.View.Pages
{
    using DailyCat.View.Resources;

    using Xamarin.Forms;

    public partial class LikedPage : BaseContentPage
    {
        public LikedPage()
        {
            this.InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
            {
                this.Icon = new FileImageSource { File = Images.TabLiked };
            }
        }
    }
}
