using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Sanretsu.Dependencies;
using Sanretsu.Droid.Dependencies;

[assembly: Dependency(typeof (CopyToClipboard))]
namespace Sanretsu.Droid.Dependencies
{
    public class CopyToClipboard : ICopyToClipboard
    {
        public void Copy(string text)
        {
            var clipboardMananger = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);
            ClipData clip = ClipData.NewPlainText("Custom Title", text);
            clipboardMananger.PrimaryClip = clip;
        }
    }
}