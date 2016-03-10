using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ICSharpCode;
using Iteedee.ApkReader;
using System.IO;

namespace Manifest.Reader.APK
{
    class ProgramAPK
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter Path");
            string APKfilePath = Console.ReadLine();
            ApkInfo info = ReadApk.ReadApkFromPath(APKfilePath);
            Console.ReadKey();
        }
    }
}