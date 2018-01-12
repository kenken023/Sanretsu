using Sanretsu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sanretsu.Services.Database
{
    public class EventDataStore : IDataStore<Event>
    {
        List<Event> items;

        public EventDataStore()
        {
            //items = new List<Event>
            //{
            //    new Event { Id = Guid.NewGuid().ToString(), Name = "First Event", Description = "This is First Event", DateTime = DateTime.Now },
            //    new Event { Id = Guid.NewGuid().ToString(), Name = "Second Event", Description = "This is Second Event", DateTime = DateTime.Now },
            //    new Event { Id = Guid.NewGuid().ToString(), Name = "Third Event", Description = "This is Third Event", DateTime = DateTime.Now },
            //    new Event { Id = Guid.NewGuid().ToString(), Name = "Fourth Event", Description = "This is Fourth Event", DateTime = DateTime.Now },
            //    new Event { Id = Guid.NewGuid().ToString(), Name = "Fifth Event", Description = "This is Fifth Event", DateTime = DateTime.Now }
            //};

            App.EventDb.SaveItemAsync(new Event() {
                Id = 1,
                Name = "WS",
                Description = "W.Service",
                DateTime = DateTime.Now
            });
        }

        public async Task<bool> AddItemAsync(Event item)
        {
            await App.EventDb.SaveItemAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Event item)
        {
            var _item = items.Where((Event arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Event arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            await App.AttendanceDb.DeleteItemsByEventAsync(_item.Id);
            await App.EventDb.DeleteItemAsync(_item);

            return await Task.FromResult(true);
        }
        public async Task<Event> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Event>> GetItemsAsync(bool forceRefresh = false)
        {
            items = await App.EventDb.GetItemsAsync();
            return await Task.FromResult(items);
        }

        public Task<IEnumerable<Event>> GetItemsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
