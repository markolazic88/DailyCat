namespace DailyCat.View.Pages
{
    using DailyCat.View.Resources;

    using Xamarin.Forms;

    public partial class LikedPage : BaseContentPage
    {
        public LikedPage()
        {
            this.InitializeComponent();

            if (Device.OS == TargetPlatform.iOS)
            {
                this.Icon = new FileImageSource { File = Images.TabLiked };
            }
        }
    }
}
