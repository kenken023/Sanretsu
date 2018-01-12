using Sanretsu.Models;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Sanretsu
{
    public class ItemsViewModel : BaseViewModel<Event>
    {
        public ObservableRangeCollection<Event> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        public Command DeleteItemCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Events";
            Items = new ObservableRangeCollection<Event>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            DeleteItemCommand = new Command<Event>(async (myEvent) => await ExecuteDeleteItemCommand(myEvent));

            MessagingCenter.Subscribe<NewItemPage, Event>(this, "AddItem", async (obj, item) =>
            {
                var _item = item as Event;
                await DataStore.AddItemAsync(_item);
                await ExecuteLoadItemsCommand();
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await DataStore.GetItemsAsync(true);
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

        async Task ExecuteDeleteItemCommand(Event myEvent)
        {
            await DataStore.DeleteItemAsync(myEvent.Id);
            await ExecuteLoadItemsCommand();
        }
    }
}
