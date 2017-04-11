namespace DailyCat.Common.Interfaces
{
    using System;
    using System.Threading.Tasks;

    public interface IExtendedNavigationService
    {
        event EventHandler<INavigationCompletedEventArgs> NavigationCompleted;

        PageKey RootPageKey { get; }

        PageKey CurrentPageKey { get; }

        void GoBack(bool isModal);

        void GoBack();

        Task<object> GetPageAsync(PageKey pageKey);

        void NavigateTo(PageKey pageKey);

        void NavigateTo(PageKey pageKey, object parameter);

        Task NavigateTo(PageKey pageKey, object parameter, bool isModal);

        object GetPage(PageKey pageKey);

        object GetRootPage();
    }
}
