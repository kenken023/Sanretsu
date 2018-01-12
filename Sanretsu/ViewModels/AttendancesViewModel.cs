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
        Event myEvent = null;

        public ObservableRangeCollection<Attendance> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command DeleteItemCommand { get; set; }

        public AttendancesViewModel()
        {
            Title = "Attendance List";
            Items = new ObservableRangeCollection<Attendance>();

            LoadItemsCommand = new Command<Event>(async (myEvent) => await ExecuteLoadItemsCommand(myEvent));
            DeleteItemCommand = new Command<Attendance>(async (attendance) => await ExecuteDeleteItemCommand(attendance));

            MessagingCenter.Subscribe<AddEventPage, Attendance>(this, "AddItem", async (obj, item) =>
			{
                var _item = item as Attendance;

                await DataStore.AddItemAsync(_item);
			});
        }

        async Task ExecuteLoadItemsCommand(Event myEvent)
        {
            if (IsBusy)
            {
                return;
            }

            IsBusy = true;
            this.myEvent = myEvent;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(myEvent.Id);
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

        async Task ExecuteDeleteItemCommand(Attendance attendance)
        {
            await DataStore.DeleteItemAsync(attendance.Id);
            await ExecuteLoadItemsCommand(myEvent);
        }
    }
}
