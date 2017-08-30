using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace Sanretsu.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ScanPage : ContentPage
	{
		public ScanPage ()
		{
			InitializeComponent ();
            ShowScanner();
        }

        public async void ShowScanner()
        {
            var scanPage = new ZXingScannerPage();
            await Navigation.PushAsync(scanPage);
        }
    }
}