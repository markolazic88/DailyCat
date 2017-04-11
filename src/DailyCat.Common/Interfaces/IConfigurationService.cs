namespace DailyCat.Common.Interfaces
{
    using DailyCat.Common.Model;

    public interface IConfigurationService
    {
        NotificationFrequency NotificationFrequency { get; set; }
    }
}
