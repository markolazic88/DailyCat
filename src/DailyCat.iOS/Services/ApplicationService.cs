namespace DailyCat.iOS.Services
{
    using DailyCat.Common.Interfaces;

    using Foundation;

    public class ApplicationService : IApplicationService
    {
        public ApplicationService(IConfigurationService configurationService)
        {
            this.ConfigurationService = configurationService;

            this.Version = NSBundle.MainBundle.ObjectForInfoDictionary("CFBundleShortVersionString").ToString();
        }

        public string Version { get; set; }

        private IConfigurationService ConfigurationService { get; set; }

        public void UpdateFirebaseTopicSubscriptions()
        {
            
        }
    }
}