using Sanretsu.Dependencies;
using Sanretsu.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Sanretsu.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AttendancesPage : ContentPage
	{
        AttendancesViewModel viewModel;

		public AttendancesPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new AttendancesViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(null);
            }
        }

        public void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            ItemsListView.SelectedItem = null;
        }

        public void OnCopyClicked(object sender, EventArgs e)
        {
            var codes = viewModel.Items.Select(item =>
            {
                return item.Code;
            }).ToArray();

            DependencyService.Get<ICopyToClipboard>().Copy(String.Join(",", codes));
        }
    }
}