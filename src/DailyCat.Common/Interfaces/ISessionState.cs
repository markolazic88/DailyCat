namespace DailyCat.Common.Interfaces
{
    using DailyCat.Common.Model;

    public interface ISessionState
    {
        ObservableRangeCollection<Cat> LikedCats { get; }

        Cat SelectedCat { get; }

        string DeviceId { get; }

        void SetSelectedCat(Cat cat);

        void SetDeviceId(string deviceId);
    }
}
