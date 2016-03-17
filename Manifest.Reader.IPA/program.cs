using Ionic.Zip;
using PlistCS;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageProcessor;
using ImageProcessor.Imaging.Formats;

namespace Manifest.Reader.IPA
{
    class ProgramIPA
    {
        public class NamespaceIgnorantXmlTextReader : XmlTextReader
        {
            public NamespaceIgnorantXmlTextReader(Stream reader) : base(reader) { }

            public override String NamespaceURI
            {
                get { return String.Empty; }
            }
        }

        private static MemoryStream GenerateStreamFromString(string value)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
        }

        static void Main(string[] args)
        {
            string regularplist, path, ipaname;

            
            //App Name
            Console.WriteLine("Please read the first Readme.md/n");
            Console.WriteLine("Please \"XXXX\" in the place of complete Payload/XXXX.app");
            ipaname = Console.ReadLine() + ".app";

            //App Path
            Console.WriteLine("Enter the path ipa");
            path = Console.ReadLine();

            ZipEntry entry = new ZipEntry();
            using (ZipFile zip = ZipFile.Read(path))
            {
                entry = zip["Payload\\" + ipaname +"\\Info.plist"];
            }
            
            using (MemoryStream stream = new MemoryStream())
            {
                entry.Extract(stream);
                stream.Position = 0;
                var downPlist = Plist.readPlist(stream, plistType.Auto);
                regularplist = Plist.writeXml(downPlist);
            }

            var parsedPlist = XDocument.Parse(regularplist);

            String[] requiredKeys = new String[] { "DTCompiler", "CFBundleIdentifier", "CFBundleShortVersionString", "MinimumOSVersion", "CFBundleSupportedPlatforms"};
            var filtredXmlData = parsedPlist.Root.Element("dict").Elements("key").Where(p => requiredKeys.Contains(p.Value)).Select(p => new
            {
                key = p.Value,
                value = Regex.Replace(p.NextNode.ToString(), "<.*?>", string.Empty)
            }).ToDictionary(k => k.key, v => v.value);

            Console.WriteLine(filtredXmlData["DTCompiler"]);
            Console.WriteLine(filtredXmlData["CFBundleIdentifier"]);
            Console.WriteLine(filtredXmlData["CFBundleShortVersionString"]);
            Console.WriteLine(filtredXmlData["CFBundleSupportedPlatforms"]);
            Console.Read();
        }
    }
}
