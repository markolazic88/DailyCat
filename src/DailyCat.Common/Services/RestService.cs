namespace DailyCat.Common.Services
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using System.Xml.Serialization;

    using DailyCat.Common.Interfaces;
    using DailyCat.Common.Model.Xml;

    public class RestService : IRestService
    {
        private HttpClient httpClient;

        public RestService()
        {
            this.InitializeHttpClient();
        }

        public async Task<byte[]> GetBinaryContent(string url)
        {
            var uri = new Uri(url);

            try
            {
                var response = await this.httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsByteArrayAsync();
                    return content;
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return null;
        }

        public async Task<List<XmlImage>> GetXmlImages(string url)
        {
            var uri = new Uri(url);

            try
            {
                var response = await this.httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    var serializer = new XmlSerializer(typeof(XmlResponse));
                    using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {
                        var xmlResponse = (XmlResponse)serializer.Deserialize(reader);
                        return xmlResponse.XmlData.XmlImages.Images;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return null;
        }

        public async Task<List<XmlVote>> GetXmlVotes(string url)
        {
            var uri = new Uri(url);

            try
            {
                var response = await this.httpClient.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {

                    var serializer = new XmlSerializer(typeof(XmlResponse));
                    using (var reader = new StreamReader(await response.Content.ReadAsStreamAsync()))
                    {

                        var xmlResponse = (XmlResponse)serializer.Deserialize(reader);
                        return xmlResponse.XmlData.XmlVotes.Votes;
                    }
                }
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception);
            }

            return null;
        }

        private void InitializeHttpClient()
        {
            this.httpClient = new HttpClient();
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
        }
    }
}
