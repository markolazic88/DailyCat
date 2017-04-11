namespace DailyCat.Common.Interfaces
{
    public interface IApplicationService
    {
        string Version { get; }

        void UpdateFirebaseTopicSubscriptions();
    }
}
