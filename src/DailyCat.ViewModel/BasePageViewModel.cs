namespace DailyCat.ViewModel
{
    using System.Threading.Tasks;
    using System.Windows.Input;

    using DailyCat.Common.Interfaces;

    using GalaSoft.MvvmLight;

    using Plugin.Toasts;

    using Xamarin.Forms;

    public class BasePageViewModel : ViewModelBase
    {
        private bool isInitialized;

        private bool isBusy;

        private ICommand settingsCommand;

        public BasePageViewModel(IExtendedNavigationService navigationService, ISessionState sessionState, IDataService dataService, IConfigurationService configurationService)
        {
            this.NavigationService = navigationService;
            this.SessionState = sessionState;
            this.DataService = dataService;
            this.ConfigurationService = configurationService;
            this.ToastNotificator = DependencyService.Get<IToastNotificator>();
        }

        public bool IsInitialized
        {
            get
            {
                return this.isInitialized;
            }
            set
            {
                this.Set(() => this.IsInitialized, ref this.isInitialized, value);
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            set
            {
                this.Set(() => this.IsBusy, ref this.isBusy, value);
            }
        }

        public ICommand SettingsCommand
        {
            get
            {
                return this.settingsCommand ?? (this.settingsCommand = new Command(this.OnSettingsCommand));
            }
        }

        protected IExtendedNavigationService NavigationService { get; set; }

        protected ISessionState SessionState { get; set; }

        protected IDataService DataService { get; set; }

        protected IConfigurationService ConfigurationService { get; set; }

        protected IToastNotificator ToastNotificator { get; set; }

        public virtual async Task OnLoad()
        {
            if (!this.IsInitialized)
            {
                await this.OnInitialize();
            }

            if (this.IsInitialized)
            {
                await this.OnRefresh();
            }
        }

        public virtual async Task OnInitialize()
        {
            this.IsInitialized = true;
        }

        public virtual async Task OnRefresh()
        {
        }

        private void OnSettingsCommand()
        {
            this.NavigationService.NavigateTo(ViewModelManager.NavigationPageKey.Settings);
        }
    }
}
