using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Mobile;
using ZXing.Net.Mobile.Forms;

namespace Sanretsu.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
    {

        public AddEventPage()
		{
			InitializeComponent();

        }

        private void OnScanClicked()
        {
            this.InitScanner();
        }

        private async Task InitScanner()
        {
            var options = new MobileBarcodeScanningOptions
            {
                AutoRotate = false,
                TryInverted = true,
                TryHarder = true
            };
            var scanPage = new ZXingScannerPage(options)
            {
                DefaultOverlayTopText = "Align the barcode within the frame",
                DefaultOverlayBottomText = string.Empty,
                DefaultOverlayShowFlashButton = true
            };
            // Navigate to our scanner page
            
            scanPage.OnScanResult += (result) => 
            {
                // Stop scanning
                scanPage.IsScanning = false;
 
                // Pop the page and show the result
                Device.BeginInvokeOnMainThread (async () => 
                {
                    await Navigation.PopAsync();        
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");
                });
            };
            await Navigation.PushAsync(scanPage);
        }

        private void OnScanResult(Result result)
        {
            this.Code.Text = result.Text;
            this.Type.Text = result.BarcodeFormat.ToString();
        }
    }
}