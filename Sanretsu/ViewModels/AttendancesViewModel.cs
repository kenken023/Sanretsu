using Sanretsu.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Diagnostics;
using Sanretsu.Views;

namespace Sanretsu.ViewModels
{
    public class AttendancesViewModel : BaseViewModel<Attendance>
    {

        public ObservableRangeCollection<Attendance> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public AttendancesViewModel()
        {
            Title = "Attendance List";
            Items = new ObservableRangeCollection<Attendance>();

            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<AddEventPage, Attendance>(this, "AddItem", async (obj, item) =>
			{
                var _item = item as Attendance;

                await DataStore.AddItemAsync(_item);
			});
        }

        private async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync();
                Items.ReplaceRange(items);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                MessagingCenter.Send(new MessagingCenterAlert
                {
                    Title = "Error",
                    Message = "Unable to load items.",
                    Cancel = "OK"
                }, "message");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
