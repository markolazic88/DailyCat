namespace DailyCat.ViewModel
{
    using System.Windows.Input;

    using DailyCat.Common;
    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model;

    using Xamarin.Forms;

    public class LikedPageViewModel : BasePageViewModel
    {
        private bool isGrid;

        private Cat gridLastTappedItem;

        private ICommand gridItemTapped;

        public LikedPageViewModel(IExtendedNavigationService navigationService, ISessionState sessionState, IDataService dataService, IConfigurationService configurationService)
            : base(navigationService, sessionState, dataService, configurationService)
        {
        }

        public ObservableRangeCollection<Cat> LikedCats
        {
            get { return this.SessionState.LikedCats; }
        }

        public bool IsGrid
        {
            get { return this.isGrid; }
            set { this.Set(() => this.IsGrid, ref this.isGrid, value); }
        }

        public Cat GridLastTappedItem
        {
            get
            {
                return this.gridLastTappedItem;
            }
            set
            {
                this.Set(() => this.GridLastTappedItem, ref this.gridLastTappedItem, value);
            }
        }

        public ICommand GridItemTappedCommand
        {
            get { return this.gridItemTapped ?? (this.gridItemTapped = new Command(this.OnGridItemTappedCommand)); }
        }

        private void OnGridItemTappedCommand()
        {
            this.SessionState.SetSelectedCat(this.GridLastTappedItem);
            this.NavigationService.NavigateTo(ViewModelManager.NavigationPageKey.CatDetail);
        }
    }
}
