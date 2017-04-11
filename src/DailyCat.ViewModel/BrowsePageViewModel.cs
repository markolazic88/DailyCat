namespace DailyCat.ViewModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Input;

    using DailyCat.Common;
    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model;
    using DailyCat.ViewModel.Resources;

    using Plugin.Connectivity;
    using Plugin.DeviceInfo;
    using Plugin.Share;
    using Plugin.Share.Abstractions;
    using Plugin.Toasts;

    using Xamarin.Forms;

    public class BrowsePageViewModel : BasePageViewModel
    {
        private bool isFetching;

        private ICommand refreshCommand;

        private ICommand shareCommand;

        private Command<int> swipedLeftCommand;

        private Command<int> swipedRightCommand;

        private Cat topCat;

        private ObservableRangeCollection<Cat> cats = new ObservableRangeCollection<Cat>();

        private ObservableRangeCollection<Vote> votes = new ObservableRangeCollection<Vote>();

        public BrowsePageViewModel(IExtendedNavigationService navigationService, ISessionState sessionState, IDataService dataService, IConfigurationService configurationService)
            : base(navigationService, sessionState, dataService, configurationService)
        {
        }

        public ObservableRangeCollection<Cat> Cats
        {
            get
            {
                return this.cats;
            }
            set
            {
                this.Set(() => this.Cats, ref this.cats, value);
            }
        }

        public ObservableRangeCollection<Vote> Votes
        {
            get
            {
                return this.votes;
            }
            set
            {
                this.Set(() => this.Votes, ref this.votes, value);
            }
        }

        public Cat TopCat
        {
            get
            {
                return this.topCat;
            }
            set
            {
                this.Set(() => this.TopCat, ref this.topCat, value);
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return this.refreshCommand ?? (this.refreshCommand = new Command(this.OnRefreshCommand));
            }
        }

        public ICommand ShareCommand
        {
            get
            {
                return this.shareCommand ?? (this.shareCommand = new Command(this.OnShareCommand));
            }
        }

        public Command<int> SwipedLeftCommand
        {
            get
            {
                return this.swipedLeftCommand ?? (this.swipedLeftCommand = new Command<int>(this.OnSwipedLeftCommand));
            }
        }

        public Command<int> SwipedRightCommand
        {
            get
            {
                return this.swipedRightCommand ?? (this.swipedRightCommand = new Command<int>(this.OnSwipedRightCommand));
            }
        }

        public override async Task OnInitialize()
        {
            await base.OnInitialize();
            this.IsBusy = true;

            var newCats = await this.GetCats(5);
            if (newCats != null)
            {
                this.Cats.AddRange(newCats);
            }

            this.SessionState.SetDeviceId(CrossDeviceInfo.Current.Id);

            var options = new NotificationOptions
            {
                Title = "Swipe right to like",
                Description = "Swipe left to dislike"
            };

            this.ToastNotificator.Notify(options);

            this.IsBusy = false;
        }

        private async void OnRefreshCommand()
        {
            this.IsBusy = true;
            var cardIndex = this.TopCat != null ? this.Cats.IndexOf(this.TopCat) : 0;
            await this.InvokeNextFetchIfNeeded(cardIndex, 10);
            this.IsBusy = false;
        }

        private void OnShareCommand()
        {
            if (this.TopCat == null)
            {
                return;
            }

            CrossShare.Current.Share(new ShareMessage
                                    {
                                        Title = ViewModelResources.ShareMessage_Title,
                                        Text = ViewModelResources.ShareMessage_Text,
                                        Url = this.TopCat.Url
                                    });
        }

        private void OnSwipedLeftCommand(int cardIndex)
        {
            this.InvokeNextFetchIfNeeded(cardIndex, 10);

            var cat = this.Cats[cardIndex];
            if (cat != null)
            {
                this.DataService.Vote(new Vote { ImageId = cat.Id, Score = 1, UserId = this.SessionState.DeviceId});
            }

        }

        private void OnSwipedRightCommand(int cardIndex)
        {
            this.InvokeNextFetchIfNeeded(cardIndex, 10);

            var cat = this.Cats[cardIndex];
            if (cat != null)
            {
                this.DataService.Vote(new Vote { ImageId = cat.Id, Score = 1, UserId = this.SessionState.DeviceId });
                cat.LikeCount++;
                this.SessionState.LikedCats.Insert(0, cat);
            }
        }

        private async Task InvokeNextFetchIfNeeded(int cardIndex, int numOfItems)
        {
            if (!this.isFetching && cardIndex + numOfItems >= this.Cats.Count)
            {
                this.isFetching = true;
                var newCats = await this.GetCats(numOfItems);
                if (newCats != null)
                {
                    this.Cats.AddRange(newCats);
                }
                this.isFetching = false;
            }
        }

        private async Task<List<Cat>> GetCats(int numOfItems)
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                var options = new NotificationOptions
                {
                    Title = "Not Connected",
                    Description = "Cound not fetch new cat pictures",
                };

                this.ToastNotificator.Notify(options);

                return null;
            }

            this.isFetching = true;
            await this.GetVotes();

            var catList = await this.DataService.GetCats(numOfItems);
            if (catList != null)
            {
                await this.DataService.GetImageSources(catList);
                catList.RemoveAll(cat => cat.ImageSource == null);

                this.SetVotes(catList);
            }

            this.isFetching = false;

            return catList;
        }

        private async Task GetVotes()
        {
            if (this.Votes.Count > 0)
            {
                return; // Votes already obtained
            } 

            var voteList = await this.DataService.GetVotes();
            if (voteList != null)
            {
                this.Votes.AddRange(voteList);
            }           
        }

        private void SetVotes(List<Cat> catList)
        {
            foreach (var cat in catList)
            {
                cat.LikeCount = this.Votes.Count(vote => vote.ImageId == cat.Id && vote.Score == 5);
                cat.DislikeCount = this.Votes.Count(vote => vote.ImageId == cat.Id && vote.Score == 1);
            }
        }
    }
}