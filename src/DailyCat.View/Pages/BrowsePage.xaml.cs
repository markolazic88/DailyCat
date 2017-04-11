namespace DailyCat.View.Pages
{
    using System;

    using DailyCat.View.Resources;

    using Xamarin.Forms;

    public partial class BrowsePage : BaseContentPage
    {
        public BrowsePage()
        {
            this.InitializeComponent();

            this.LayoutChanged += (object sender, EventArgs e) =>
            {
                this.SwipeCardView.CardMoveDistance = (int)(this.Width * 0.20f);
            };


            if (Device.OS == TargetPlatform.iOS)
            {
                this.Icon = new FileImageSource { File = Images.TabBrowse };
            }
        }
    }
}
