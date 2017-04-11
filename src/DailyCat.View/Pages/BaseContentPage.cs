namespace DailyCat.View.Pages
{
    using DailyCat.ViewModel;

    using Xamarin.Forms;

    public class BaseContentPage : ContentPage
    {
        protected override void OnAppearing()
        {
            base.OnAppearing();

            var viewModel = this.BindingContext as BasePageViewModel;
            if (viewModel != null)
            {
                viewModel.OnLoad();
            }
        }
    }
}
