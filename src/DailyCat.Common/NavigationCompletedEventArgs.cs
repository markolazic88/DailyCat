namespace DailyCat.Common
{
    using System;

    using DailyCat.Common.Interfaces;

    public class NavigationCompletedEventArgs : EventArgs, INavigationCompletedEventArgs
    {
        public NavigationCompletedEventArgs(PageKey pageKey)
        {
            this.NavigatedPageKey = pageKey;
        }

        public PageKey NavigatedPageKey { get; private set; }
    }

}
