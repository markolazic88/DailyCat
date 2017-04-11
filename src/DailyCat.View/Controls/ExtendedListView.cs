namespace DailyCat.View.Controls
{
    using System.Windows.Input;

    using MLToolkit.Forms.SwipeCardView;

    using Xamarin.Forms;

    public class ExtendedListView : ListView
    {
        public static readonly BindableProperty ItemSelectedCommandProperty =
            BindableProperty.Create(
            nameof(ItemSelectedCommand),
            typeof(ICommand),
            typeof(ExtendedListView));

        public ExtendedListView()
        {
            this.ItemSelected += this.OnItemSelected;
        }
        
        public ICommand ItemSelectedCommand
        {
            get { return (ICommand)this.GetValue(ItemSelectedCommandProperty); }
            set { this.SetValue(ItemSelectedCommandProperty, value); }
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem != null && this.ItemSelectedCommand != null && this.ItemSelectedCommand.CanExecute(args))
            {
                this.ItemSelectedCommand.Execute(args.SelectedItem);
            }
        }
    }
}
