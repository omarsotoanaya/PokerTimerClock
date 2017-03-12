using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PokerTimerClock
{
    class Round
    {
        #region Fields and Constants.
        private readonly string _xmlFile = @"XmlTimerConfig.xml";
        private const string _blindsTagName = "Blinds";
        private const string _roundTagName = "round";
        private const string _numberAttribute = "number";
        private const string _bigBlindAttribute = "bigBlind";
        private const string _smallBlindAttribute = "smallBlind";
        private const string _xPathRoundTime = "/root/roundTime[.='00:20:00']";
        private XmlDocument _doc;

        #endregion

        private List<Blind> GetBlinds()
        {
            // Read the XML File.
            _doc = new XmlDocument();
            _doc.Load(_xmlFile);

            // Get The elements in the XML.
            XmlNodeList blinds = _doc.GetElementsByTagName(_blindsTagName);
            XmlNodeList list = ((XmlElement)blinds[0]).GetElementsByTagName(_roundTagName);

            List<Blind> blindList = new List<Blind>();
            foreach (XmlElement node in list)
            {
                // Fill blind obj.
                var blindObj = new Blind
                {
                    RoundNumber = node.GetAttribute(_numberAttribute),
                    BigBlind = node.GetAttribute(_bigBlindAttribute),
                    SmallBlind = node.GetAttribute(_smallBlindAttribute)
                };

                blindList.Add(blindObj);

            }

            return blindList;

        }

        public Root GetRoundData()
        {
            // Read xml file.
            _doc = new XmlDocument();
            _doc.Load(_xmlFile);

            // Get Time Round.
            XmlNode roundTime = _doc.SelectSingleNode(_xPathRoundTime);

            // Fill round Data.
            Root round = new Root();
            round.RoundTime = TimeSpan.Parse(roundTime.InnerText);
            round.BlindList = GetBlinds();

            return round;

        }
    }
}
