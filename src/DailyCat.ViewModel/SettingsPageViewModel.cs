namespace DailyCat.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model;

    public class SettingsPageViewModel : BasePageViewModel
    {
        private NotificationFrequency selectedNotificationFrequency;

        private List<NotificationFrequency> notificationFrequencies;

        private string version;

        public SettingsPageViewModel(IExtendedNavigationService navigationService, ISessionState sessionState, IDataService dataService, IConfigurationService configurationService, IApplicationService applicationService)
            : base(navigationService, sessionState, dataService, configurationService)
        {
            this.NotificationFrequencies = Enum.GetValues(typeof(NotificationFrequency)).Cast<NotificationFrequency>().ToList();
            this.ApplicationService = applicationService;
        }

        public NotificationFrequency SelectedNotificationFrequency
        {
            get
            {
                return this.selectedNotificationFrequency;
            }
            set
            {
                if (this.Set(() => this.SelectedNotificationFrequency, ref this.selectedNotificationFrequency, value))
                {
                    this.ConfigurationService.NotificationFrequency = value;
                    this.ApplicationService.UpdateFirebaseTopicSubscriptions();
                }
            }
        }

        public List<NotificationFrequency> NotificationFrequencies
        {
            get
            {
                return this.notificationFrequencies;
            }
            set
            {
                this.Set(() => this.NotificationFrequencies, ref this.notificationFrequencies, value);
            }
        }

        public string Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.Set(() => this.Version, ref this.version, value);
            }
        }

        private IApplicationService ApplicationService { get; set; }

        public override async Task OnInitialize()
        {
            await base.OnInitialize();
            this.SelectedNotificationFrequency = this.ConfigurationService.NotificationFrequency;
            this.Version = this.ApplicationService.Version;
        }
    }
}
