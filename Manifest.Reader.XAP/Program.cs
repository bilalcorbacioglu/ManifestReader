using System;
using Ionic.Zip;
using System.IO;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Manifest.Reader.XAP
{
    [Serializable(), XmlRoot("Deployment")]
    public class Deployment
    {
        [XmlElement("App")]
        public App App { get; set; }

        [XmlAttribute("AppPlatformVersion")]
        public String AppPlatformVersion { get; set; }
    }

    public class App
    {
        [XmlAttribute("Version")]
        public String Version { get; set; }

        [XmlAttribute("Description")]
        public String Description { get; set; }

        //[XmlElement("IconPath")]
        //public IconPath IconPath { get; set; }

        [XmlArray("Capabilities")]
        public Capability[] Capabilities { get; set; }

        //[XmlElement("Tasks")]
        //public Tasks Tasks { get; set; }
    }

    public class Capability
    {
        [XmlAttribute("Name")]
        public String Name { get; set; }
    }

    /*
    public class IconPath
    {
        [XmlAttribute("IsRelative")]
        public Boolean IsRelative { get; set; }

        [XmlAttribute("IsResource")]
        public Boolean IsResource { get; set; }
    }

    public class Tasks
    {
        [XmlElement("DefaultTask")]
        public DefaultTask DefaultTask { get; set; }

    }

    public class DefaultTask
    {
        [XmlAttribute("Name")]
        public String Name { get; set; }

        [XmlAttribute("NavigationPage")]
        public String NavigationPage { get; set; }

    }*/

    public class NamespaceIgnorantXmlTextReader : XmlTextReader
    {
        public NamespaceIgnorantXmlTextReader(Stream reader) : base(reader) { }

        public override String NamespaceURI
        {
            get { return String.Empty; }
        }
    }

    public class ProgramXAP
    {
        public string path;
        public ProgramXAP() {}
        public void xap(string path)
        {
            ZipEntry entry = new ZipEntry();
            using (ZipFile zip = ZipFile.Read(path))
            {
                entry = zip["WMAppManifest.xml"];
            }

            using (MemoryStream stream = new MemoryStream())
            {
                entry.Extract(stream);
                stream.Position = 0;
                var serializer = new XmlSerializer(typeof(Deployment));
                var deployment = serializer.Deserialize(new NamespaceIgnorantXmlTextReader(stream)) as Deployment;
                System.Console.WriteLine(deployment.App.Version);
                System.Console.WriteLine(deployment.App.Description);
                System.Console.WriteLine(deployment.App.Capabilities);
                System.Console.Read();
            }

        }

        static void Main ( string[] args )
        {
            ZipEntry entry = new ZipEntry();
            System.Console.WriteLine("Enter Path");

            string path = System.Console.ReadLine();
            
            using (ZipFile zip = ZipFile.Read(path))                        
            {
                entry = zip["WMAppManifest.xml"];
            }

            using (MemoryStream stream = new MemoryStream())
            {
                entry.Extract(stream);
                stream.Position = 0;
                var serializer = new XmlSerializer(typeof(Deployment));
                var deployment = serializer.Deserialize(new NamespaceIgnorantXmlTextReader(stream)) as Deployment;
                System.Console.WriteLine(deployment.App.Version);
                System.Console.WriteLine(deployment.App.Description);
                System.Console.WriteLine(deployment.App.Capabilities);
                System.Console.Read();
            }
        }
    }
}