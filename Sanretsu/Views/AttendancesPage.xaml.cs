using Sanretsu.Dependencies;
using Sanretsu.Models;
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
        Event myEvent;

		public AttendancesPage ()
		{
			InitializeComponent ();

            BindingContext = viewModel = new AttendancesViewModel();
        }

        public AttendancesPage(Event myEvent) : this()
        {
            this.myEvent = myEvent;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (viewModel.Items.Count == 0)
            {
                viewModel.LoadItemsCommand.Execute(myEvent);
            }
        }
        public void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            Attendance item = args.SelectedItem as Attendance;
            if (item == null)
            {
                return;
            }

            ItemDetailViewModel model = new ItemDetailViewModel(item);

            Navigation.PushAsync(new ItemDetailPage(model));
            ItemsListView.SelectedItem = null;
        }

        public void OnCopyClicked(object sender, EventArgs e)
        {
            var codes = viewModel.Items.Select(item =>
            {
                return item.Code;
            }).ToArray();

            DependencyService.Get<ICopyToClipboard>().Copy(String.Join("\n", codes));
        }

        private void ScanItemClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddEventPage());
        }

        public void OnDelete(object sender, EventArgs e)
        {
            var mi = ((MenuItem)sender);
            viewModel.DeleteItemCommand.Execute(mi.CommandParameter);
        }
    }
}