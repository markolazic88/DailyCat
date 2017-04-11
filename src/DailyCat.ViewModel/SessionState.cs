namespace DailyCat.ViewModel
{
    using DailyCat.Common;
    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model;

    public class SessionState : ISessionState
    {
        public SessionState()
        {
            this.LikedCats = new ObservableRangeCollection<Cat>();
        }

        public ObservableRangeCollection<Cat> LikedCats { get; private set; }

        public Cat SelectedCat { get; private set; }

        public void SetSelectedCat(Cat cat)
        {
            this.SelectedCat = cat;
        }

        public string DeviceId { get; set; }

        public void SetDeviceId(string deviceId)
        {
            this.DeviceId = deviceId;
        }
    }
}
