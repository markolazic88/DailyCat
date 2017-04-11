namespace DailyCat.Droid.Services
{
    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model;

    using global::Firebase.Messaging;

    public class ApplicationService : IApplicationService
    {
        public ApplicationService(IConfigurationService configurationService)
        {
            this.ConfigurationService = configurationService;
            this.Version = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0).VersionName;
        }

        public string Version { get; set; }

        private IConfigurationService ConfigurationService { get; set; }

        public void UpdateFirebaseTopicSubscriptions()
        {
            var notificationFrequency = this.ConfigurationService.NotificationFrequency;

            switch (notificationFrequency)
            {
                case NotificationFrequency.None:
                    FirebaseMessaging.Instance.UnsubscribeFromTopic(NotificationFrequency.Daily.ToString());
                    FirebaseMessaging.Instance.UnsubscribeFromTopic(NotificationFrequency.Weekly.ToString());
                    break;
                case NotificationFrequency.Daily:
                    FirebaseMessaging.Instance.SubscribeToTopic(NotificationFrequency.Daily.ToString());
                    FirebaseMessaging.Instance.UnsubscribeFromTopic(NotificationFrequency.Weekly.ToString());
                    break;
                case NotificationFrequency.Weekly:
                    FirebaseMessaging.Instance.UnsubscribeFromTopic(NotificationFrequency.Daily.ToString());
                    FirebaseMessaging.Instance.SubscribeToTopic(NotificationFrequency.Weekly.ToString());
                    break;
            }
        }
    }
}