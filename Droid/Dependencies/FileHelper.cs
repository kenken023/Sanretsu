using System;
using System.IO;
using Sanretsu.Dependencies;
using Sanretsu.Droid.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace Sanretsu.Droid.Dependencies
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}
