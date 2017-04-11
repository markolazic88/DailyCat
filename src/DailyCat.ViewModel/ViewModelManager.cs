namespace DailyCat.ViewModel
{
    using DailyCat.Common;

    public static class ViewModelManager
    {
        public static class NavigationPageKey
        {
            public static readonly PageKey Main = new PageKey
            {
                PageName = "Main",
                IsDetailPage = false
            };

            public static readonly PageKey Browse = new PageKey
            {
                PageName = "Browse",
                IsDetailPage = false
            };

            public static readonly PageKey Liked = new PageKey
            {
                PageName = "Liked",
                IsDetailPage = false
            };

            public static readonly PageKey CatDetail = new PageKey
            {
                PageName = "CatDetail",
                IsDetailPage = false
            };

            public static readonly PageKey Settings = new PageKey
            {
                PageName = "Settings",
                IsDetailPage = false
            };
        }
    }
}
