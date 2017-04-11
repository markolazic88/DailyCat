namespace DailyCat.Common.Model.Xml
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "image")]
    public class XmlImage
    {
        [XmlElement(ElementName = "id")]
        public string Id { get; set; }

        [XmlElement(ElementName = "url")]
        public string Url { get; set; }

        [XmlElement(ElementName = "source_url")]
        public string SourceUrl { get; set; }

        [XmlElement(ElementName = "sub_id")]
        public string SubId { get; set; }

        [XmlElement(ElementName = "created")]
        public string Created { get; set; }

        [XmlElement(ElementName = "score")]
        public short Score { get; set; }
    }
}

