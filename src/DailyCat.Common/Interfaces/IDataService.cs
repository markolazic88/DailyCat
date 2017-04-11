namespace DailyCat.Common.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DailyCat.Common.Model;

    public interface IDataService
    {
        Task<List<Cat>> GetCats(int numOfItems);

        Task GetImageSources(List<Cat> cats);

        Task<List<Vote>> GetVotes();

        Task Vote(Vote vote);
    }
}
