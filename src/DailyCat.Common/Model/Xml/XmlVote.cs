namespace DailyCat.Common.Model.Xml
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "vote")]
    public class XmlVote
    {
        [XmlElement(ElementName = "score")]

        public short Score { get; set; }

        [XmlElement(ElementName = "image_id")]
        public string ImageId { get; set; }

        [XmlElement(ElementName = "sub_id")]
        public string SubId { get; set; }

        [XmlElement(ElementName = "action")]
        public string Action { get; set; }
    }
}
