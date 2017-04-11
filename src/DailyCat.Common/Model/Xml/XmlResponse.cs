namespace DailyCat.Common.Model.Xml
{
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "response")]
    public class XmlResponse
    {
        [XmlElement(ElementName = "data")]
        public XmlData XmlData { get; set; }
    }
}
