namespace DailyCat.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model;

    using Xamarin.Forms;

    public class DataService : IDataService
    {
        // TODO Replace
        private const string ApiKey = "YOUR_API_KEY";

        private const string ApiBaseUrl = "http://thecatapi.com/api/";

        private const string ApiImageType = "jpg,png";

        public DataService(IRestService restService)
        {
            this.RestService = restService;
        }

        private IRestService RestService { get; set; }

        public async Task<List<Cat>> GetCats(int numOfItems)
        {
            var catList = new List<Cat>();

            var url = $"{ApiBaseUrl}images/get?format=xml&type={ApiImageType}&results_per_page={numOfItems}";
            var xmlImages = await this.RestService.GetXmlImages(url);
            if (xmlImages == null)
            {
                return catList;
            }

            foreach (var xmlImage in xmlImages)
            {
                try
                {
                    catList.Add(
                        new Cat
                        {
                            Id = xmlImage.Id,
                            Url = xmlImage.Url,
                            SourceUrl = xmlImage.SourceUrl
                        });
                }
                catch (Exception exception)
                {
                    Debug.WriteLine(exception);
                }
            }

            return catList;
        }

        public async Task GetImageSources(List<Cat> cats)
        {
            foreach (var cat in cats)
            {
                var url = $"{ApiBaseUrl}images/get?image_id={cat.Id}&size=med";
                var imageBytes = await this.RestService.GetBinaryContent(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    cat.ImageBytes = imageBytes;
                    cat.ImageSource = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                }
            }
        }

        public async Task<List<Vote>> GetVotes()
        {
            var url = $"{ApiBaseUrl}images/getvotes?api_key={ApiKey}";
            var xmlImages = await this.RestService.GetXmlImages(url);
            if (xmlImages == null)
            {
                return null;
            }

            return xmlImages.Select(
                    xmlImage => new Vote { ImageId = xmlImage.Id, UserId = xmlImage.SubId, Score = xmlImage.Score })
                    .ToList();
        }

        public async Task Vote(Vote vote)
        {
            var url = $"{ApiBaseUrl}images/vote?api_key={ApiKey}&sub_id={vote.UserId}&image_id={vote.ImageId}&score={vote.Score}";
            var xmlVotes = await this.RestService.GetXmlVotes(url);
        }
    }
}
