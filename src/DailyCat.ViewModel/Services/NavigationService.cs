namespace DailyCat.ViewModel.Services
{
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;

    using DailyCat.Common;
    using DailyCat.Common.Interfaces;

    using Xamarin.Forms;

    public class NavigationService : IExtendedNavigationService
    {
        public NavigationService(
            NavigationDictionary<Type> typeNavigationDictionary,
            PageKey rootPageKey)
        {
            this.TypeNavigationDictionary = typeNavigationDictionary;
            this.PageNavigationDictionary = new NavigationDictionary<Page>();

            this.RootPageKey = rootPageKey;
        }

        public event EventHandler NavigationStarted;

        public event EventHandler<INavigationCompletedEventArgs> NavigationCompleted;

        public PageKey RootPageKey { get; private set; }

        public PageKey CurrentPageKey
        {
            get
            {
                if (this.NavigationPage == null || this.NavigationPage.CurrentPage == null)
                {
                    return null;
                }

                var lastPage = this.NavigationPage.Navigation.ModalStack.LastOrDefault() ?? this.NavigationPage;

                var currentPage = lastPage is NavigationPage ? (lastPage as NavigationPage).CurrentPage : lastPage;
                var currentPageType = currentPage is MasterDetailPage ? ((MasterDetailPage)currentPage).Detail.GetType() : currentPage.GetType();
                var currentPageKey = this.TypeNavigationDictionary.FirstOrDefault(pair => pair.Value == currentPageType).Key;

                return currentPageKey;
            }
        }

        private NavigationDictionary<Type> TypeNavigationDictionary { get; set; }

        private NavigationDictionary<Page> PageNavigationDictionary { get; set; }

        private NavigationPage NavigationPage { get; set; }

        public void GoBack()
        {
            this.GoBack(false);
        }

        public void GoBack(bool isModal)
        {
            if (isModal)
            {
                this.NavigationPage.Navigation.PopModalAsync();
            }
            else
            {
                this.NavigationPage.Navigation.PopAsync();
            }
        }

        public async Task<object> GetPageAsync(PageKey pageKey)
        {
            return await Task.Run(() => this.GetPage(pageKey));
        }

        public void NavigateTo(PageKey pageKey)
        {
            this.NavigateTo(pageKey, null, false);
        }

        public void NavigateTo(PageKey pageKey, object parameter)
        {
            this.NavigateTo(pageKey, parameter, false);
        }

        public async Task NavigateTo(PageKey pageKey, object parameter, bool isModal)
        {
            var currentPageKey = this.CurrentPageKey;
            if (currentPageKey == null || currentPageKey == pageKey)
            {
                return;
            }

            var page = this.GetPage(pageKey, parameter) as Page;
            if (page == null)
            {
                return;
            }

            this.OnNavigationStarted();

            if (pageKey.IsDetailPage)
            {
                // Pop modals if they exist
                dynamic lastPage = this.NavigationPage.Navigation.ModalStack.LastOrDefault() ?? this.NavigationPage;

                while (lastPage != null &&
                        ((lastPage is NavigationPage && lastPage.CurrentPage.GetType() != this.NavigationPage.CurrentPage.GetType())
                        || (!(lastPage is NavigationPage) && lastPage.GetType() != this.NavigationPage.CurrentPage.GetType())))
                {
                    await this.NavigationPage.Navigation.PopModalAsync();
                    lastPage = this.NavigationPage.Navigation.ModalStack.LastOrDefault();
                }

                // Pop navigation pages if they exist
                if (!currentPageKey.IsDetailPage)
                {
                    await this.NavigationPage.Navigation.PopToRootAsync();
                }

                // TODO Use if needed for MasterDetailPage
                // Change detail page
                //var masterDetail = this.NavigationPage.CurrentPage as MainPage;
                //if (masterDetail != null)
                //{
                //    masterDetail.SetDetailPage(page);
                //}
            }
            else
            {
                if (page.Parent != null)
                {
                    page.Parent = null;
                }

                if (isModal)
                {
                    await this.NavigationPage.Navigation.PushModalAsync(page);
                }
                else
                {
                    await this.NavigationPage.Navigation.PushAsync(page);
                }
            }

            this.OnNavigationCompleted();
        }

        public object GetPage(PageKey pageKey)
        {
            return this.GetPage(pageKey, null);
        }

        public object GetPage(PageKey pageKey, object parameter)
        {
            if (!this.TypeNavigationDictionary.ContainsKey(pageKey))
            {
                throw new ArgumentException(
                    string.Format(
                        "NoPage",
                        pageKey.PageName),
                    "pageKey");
            }

            if (!this.PageNavigationDictionary.ContainsKey(pageKey))
            {
                ConstructorInfo pageConstructor;
                object[] pageParameters;

                var pageType = this.TypeNavigationDictionary[pageKey];
                if (parameter != null)
                {
                    pageConstructor = pageType.GetTypeInfo()
                        .DeclaredConstructors
                        .FirstOrDefault(constructor => constructor.GetParameters().Count() == 1 && constructor.GetParameters()[0].ParameterType == parameter.GetType());
                    pageParameters = new[] { parameter };
                }
                else
                {
                    pageConstructor = pageType.GetTypeInfo().DeclaredConstructors.FirstOrDefault(c => !c.GetParameters().Any());
                    pageParameters = new object[] { };
                }

                if (pageConstructor == null)
                {
                    throw new InvalidOperationException(string.Format("NoConstructor", pageKey));
                }

                var page = pageConstructor.Invoke(pageParameters) as Page;
                if (pageKey == this.RootPageKey)
                {
                    this.NavigationPage = new NavigationPage(page);
                }

                this.PageNavigationDictionary.Add(pageKey, page);
            }

            return this.PageNavigationDictionary[pageKey];
        }

        public object GetRootPage()
        {
            if (this.PageNavigationDictionary.ContainsKey(this.RootPageKey))
            {
                this.NavigateTo(ViewModelManager.NavigationPageKey.Main);
                var masterDetail = this.PageNavigationDictionary[this.RootPageKey] as MasterDetailPage;
                if (masterDetail != null)
                {
                    masterDetail.Detail = new ContentPage();
                    masterDetail.Master = new ContentPage { Title = "DailyCat" };
                }
                this.NavigationPage = null;

                this.PageNavigationDictionary.Remove(this.RootPageKey);
            }

            this.GetPage(this.RootPageKey);

            return this.NavigationPage;
        }

        protected void OnNavigationStarted()
        {
            if (this.NavigationStarted != null)
            {
                this.NavigationStarted(this, new NavigationCompletedEventArgs(this.CurrentPageKey));
            }
        }

        protected void OnNavigationCompleted()
        {
            if (this.NavigationCompleted != null)
            {
                this.NavigationCompleted(this, new NavigationCompletedEventArgs(this.CurrentPageKey));
            }
        }
    }
}
