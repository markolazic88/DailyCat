namespace DailyCat.Common.Services
{
    using System;

    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model;

    using Plugin.Settings;
    using Plugin.Settings.Abstractions;

    public class ConfigurationService : IConfigurationService
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        #region Setting Constants

        private const string NotificationFrequencyKey = "NotificationFrequencyKey";

        private static readonly string NotificationFrequencyDefault = NotificationFrequency.Daily.ToString();

        #endregion

        public NotificationFrequency NotificationFrequency
        {
            get
            {
                NotificationFrequency frequency;
                return Enum.TryParse(this.GetValue(NotificationFrequencyKey, NotificationFrequencyDefault), true, out frequency) ? frequency : NotificationFrequency.Daily;
            }
            set
            {
                this.SetValue(NotificationFrequencyKey, value.ToString());
            }
        }

        private string GetValue(string key, string defaultValue)
        {
            return AppSettings.GetValueOrDefault(key, defaultValue);
        }

        private bool SetValue(string key, string value)
        {
            return AppSettings.AddOrUpdateValue(key, value);
        }
    }
}
