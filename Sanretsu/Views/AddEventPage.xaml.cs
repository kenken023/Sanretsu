using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace Sanretsu.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddEventPage : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        private Grid _grid;

        public AddEventPage ()
		{
			InitializeComponent ();

            //zxing = new ZXingScannerView
            //{
            //    HorizontalOptions = LayoutOptions.FillAndExpand,
            //    VerticalOptions = LayoutOptions.FillAndExpand
            //};

            //overlay = new ZXingDefaultOverlay
            //{
            //    TopText = "Hold your phone up to the barcode",
            //    BottomText = "Scanning will happen automatically",
            //    ShowFlashButton = zxing.HasTorch,
            //};
            //zxing.IsScanning = false;

            //zxing.OnScanResult += this.OnScanResult;



            zxing = new ZXingScannerView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            zxing.OnScanResult += (result) =>
                Device.BeginInvokeOnMainThread(async () => {

                    // Stop analysis until we navigate away so we don't keep reading barcodes
                    zxing.IsAnalyzing = false;

                    // Show an alert
                    await DisplayAlert("Scanned Barcode", result.Text, "OK");

                    // Navigate away
                    await Navigation.PopAsync();
                });

            overlay = new ZXingDefaultOverlay
            {
                TopText = "Hold your phone up to the barcode",
                BottomText = "Scanning will happen automatically",
                ShowFlashButton = zxing.HasTorch,
            };
            overlay.FlashButtonClicked += (sender, e) => {
                zxing.IsTorchOn = !zxing.IsTorchOn;
            };
            var grid = new Grid
            {
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };
            grid.Children.Add(zxing);
            grid.Children.Add(overlay);

            // The root page of your application
            Content = grid;
        }

        private void OnScanResult(Result result)
        {
            this.Code.Text = result.Text;
            this.Type.Text = result.BarcodeFormat.ToString();
        }

        private void OnScanClicked(object sender, EventArgs e)
        {
            if (!zxing.IsScanning)
            {
                if (_grid == null)
                {
                    _grid = new Grid
                    {
                        VerticalOptions = LayoutOptions.FillAndExpand,
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                    };
                    _grid.Children.Add(zxing);
                    _grid.Children.Add(overlay);
                    StackContainer.Children.Add(_grid);
                }

                StackContainer.Children.Add(_grid);
                zxing.IsScanning = true;
            }
            else
            {
                StackContainer.Children.RemoveAt(StackContainer.Children.Count - 1);
                zxing.IsScanning = false;
            }

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            zxing.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            zxing.IsScanning = false;

            base.OnDisappearing();
        }

        ~AddEventPage()
        {
            //zxing.OnScanResult -= this.OnScanResult;
        }
    }
}