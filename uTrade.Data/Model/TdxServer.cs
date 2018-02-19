using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using uTrade.Data.Properties;

namespace uTrade.Data
{
    public class Server
    {
        public string Name { set; get; }
        public string IP { set; get; }
        public int Port { set; get; }
        public string Desc { set; get; }
    }
    public class TdxServer
    {
        List<Server> lstTdxServers = new List<Server>();

        Server oAvailServer;
        public TdxServer()
        {
            GetServersFromConfig();
        }

        public Server Available
        {
            get
            {
                return oAvailServer;
            }
        }

        private void GetServersFromConfig()
        {
            if (!File.Exists("./TdxConfig.xml"))
            {
                File.WriteAllText("./TdxConfig.xml", Resource.TdxConfig, Encoding.GetEncoding("GB2312"));
            }
            XmlDocument doc = new XmlDocument();
            doc.Load("./TdxConfig.xml");
            XmlElement Servers = doc.DocumentElement["Servers"];
            XmlNodeList nlist = Servers.ChildNodes;
            foreach (XmlNode server in nlist)
            {
 
                Server tdxServer = new Server();
                tdxServer.IP = server["IP"].InnerText;
                tdxServer.Name = server["Name"].InnerText;
                tdxServer.Port = int.Parse(server["Port"].InnerText);
                tdxServer.Desc = server["Desc"].InnerText;

                if (IsAvailableIP(tdxServer.IP))
                {
                    oAvailServer = tdxServer;
                    break;
                }
            }
        }

        private bool IsAvailableIP(string strIP)
        {
            Ping ping = new Ping();
            PingReply pingReply = ping.Send(strIP,100);
            return (pingReply.Status == IPStatus.Success);
        }
    }
}
