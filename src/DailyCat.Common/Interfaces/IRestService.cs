namespace DailyCat.Common.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DailyCat.Common.Model.Xml;

    public interface IRestService
    {
        Task<byte[]> GetBinaryContent(string url);

        Task<List<XmlImage>> GetXmlImages(string url);

        Task<List<XmlVote>> GetXmlVotes(string url);
    }
}
