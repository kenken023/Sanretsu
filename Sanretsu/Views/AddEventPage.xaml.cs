using System;
using System.Collections.Generic;
using Sanretsu.Models;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace Sanretsu.Views
{
    public partial class AddEventPage : ContentPage
    {
        ZXingScannerView zxing;
        ZXingDefaultOverlay overlay;
        bool _isScanning = false;

        public AddEventPage()
        {
            InitializeComponent();

			zxing = new ZXingScannerView
			{
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand
			};
			zxing.OnScanResult += (result) =>
				Device.BeginInvokeOnMainThread(() => {

					// Stop analysis until we navigate away so we don't keep reading barcodes
					//zxing.IsAnalyzing = false;

					// Show an alert
					//await DisplayAlert("Scanned Barcode", result.Text, "OK");

					//// Navigate away
					//await Navigation.PopAsync();

					this.Code.Text = result.Text;
					this.Type.Text = result.BarcodeFormat.ToString();

                    if (_isScanning)
                    {
						MessagingCenter.Send(this, "AddItem", new Attendance
						{
							Code = result.Text,
							Name = "",
							Description = result.BarcodeFormat.ToString()
						});
                    }
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
			//Content = grid;

			this.StackContainer.Children.Add(grid);
        }

        private void OnScanClicked(object sender, EventArgs e)
		{
            _isScanning = !_isScanning;

            if (_isScanning)
            {
                this.BtnScan.Text = "Stop Scan";
            }
            else
            {
                this.BtnScan.Text = "Start Scan";
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
    }
}