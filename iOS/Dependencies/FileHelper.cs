using System;
using System.IO;
using Sanretsu.Dependencies;
using Sanretsu.iOS.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Sanretsu.iOS.Dependencies
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}
