using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PokerTimerClock
{
    class XmlReadConfig
    {
        public List<Blind> ReadXml()
        {
            var blind = new List<Blind>();
            // Read the XML File.
            XmlDocument doc = new XmlDocument();
            doc.Load(@"XmlTimerConfig.xml");
            XmlNode root = doc.DocumentElement;

            XmlNodeList list = root.SelectNodes("/descendant::Blinds");

            var t = root.SelectNodes("//Blinds/round");

            List<Blind> blindList = new List<Blind>();
            foreach (XmlNode node in root.SelectNodes("//Blinds/round"))
            {
                
                var blindObj = new Blind
                {
                    RoundNumber = node.InnerText
                };

                blindList.Add(blindObj);

                //var objetoNuevo = new
                //{
                //    round = node.Attributes["round"],
                //    otraVariable = node.Attributes["otraariable"]
                //};

            }
            
            return blindList;

        }
    }
}
