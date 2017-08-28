using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;
using Sanretsu.Dependencies;
using Sanretsu.iOS.Dependencies;
using Xamarin.Forms;

[assembly: Dependency(typeof(CopyToClipboard))]
namespace Sanretsu.iOS.Dependencies
{
    public class CopyToClipboard : ICopyToClipboard
    {
        public void Copy(string text)
        {
            UIPasteboard clipboard = UIPasteboard.General;
            clipboard.String = text;
        }
    }
}