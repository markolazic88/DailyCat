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


            if (Device.RuntimePlatform == Device.iOS)
            {
                this.Icon = new FileImageSource { File = Images.TabBrowse };
            }
        }

        private void OnSwipeLeftClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipeLeft(50, 20);
        }

        private void OnSwipeRightClicked(object sender, EventArgs e)
        {
            this.SwipeCardView.InvokeSwipeRight(1000, 1);
        }
    }
}
