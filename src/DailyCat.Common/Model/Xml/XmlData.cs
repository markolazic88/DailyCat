namespace DailyCat.Common.Model.Xml
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "data")]
    public class XmlData
    {
        [XmlElement(ElementName = "images")]
        public XmlImages XmlImages { get; set; }

        [XmlElement(ElementName = "votes")]
        public XmlVotes XmlVotes { get; set; }
    }
}