namespace DailyCat.Common.Model.Xml
{
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName = "votes")]
    public class XmlVotes
    {
        [XmlElement(ElementName = "vote")]
        public List<XmlVote> Votes { get; set; }
    }
}
