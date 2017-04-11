namespace DailyCat.Common.Model.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "images")]
    public class XmlImages
    {
        [XmlElement(ElementName = "image")]
        public List<XmlImage> Images { get; set; }
    }
}