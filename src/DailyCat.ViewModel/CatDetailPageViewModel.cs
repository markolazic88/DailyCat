namespace DailyCat.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model;
    using DailyCat.ViewModel.Resources;

    using Plugin.Share;
    using Plugin.Share.Abstractions;

    using Xamarin.Forms;

    public class CatDetailPageViewModel : BasePageViewModel
    {
        private Cat cat;

        private ICommand shareCommand;

        public CatDetailPageViewModel(IExtendedNavigationService navigationService, ISessionState sessionState, IDataService dataService, IConfigurationService configurationService)
            : base(navigationService, sessionState, dataService, configurationService)
        {
        }

        public Cat Cat
        {
            get
            {
                return this.SessionState.SelectedCat;
            }
            set
            {
                this.Set(() => this.Cat, ref this.cat, value);
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                return this.shareCommand ?? (this.shareCommand = new Command(this.OnShareCommand));
            }
        }

        public override async Task OnRefresh()
        {
            await base.OnRefresh();
            this.Cat = this.SessionState.SelectedCat;
        }

        private void OnShareCommand()
        {
            if (this.Cat == null)
            {
                return;
            }

            CrossShare.Current.Share(new ShareMessage
            {
                Title = ViewModelResources.ShareMessage_Title,
                Text = ViewModelResources.ShareMessage_Text,
                Url = this.Cat.Url
            });
        }
    }
}
