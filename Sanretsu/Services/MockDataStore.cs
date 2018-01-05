using Sanretsu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sanretsu
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;
        List<Event> events;

        public MockDataStore()
        {
            events = new List<Event>
            {
                new Event { Id = 1, Name = "First Event", Description = "This is First Event", DateTime = DateTime.Now },
                new Event { Id = 2, Name = "Second Event", Description = "This is Second Event", DateTime = DateTime.Now },
                new Event { Id = 3, Name = "Third Event", Description = "This is Third Event", DateTime = DateTime.Now },
                new Event { Id = 4, Name = "Fourth Event", Description = "This is Fourth Event", DateTime = DateTime.Now },
                new Event { Id = 5, Name = "Fifth Event", Description = "This is Fifth Event", DateTime = DateTime.Now }
            };

            items = new List<Item>();
            var _items = new List<Item>
            {
                new Item { Id = 1, Text = "First item", Description="This is a nice description", Code="00806906"},
                new Item { Id = 2, Text = "Second item", Description="This is a nice description", Code="12345"},
                new Item { Id = 3, Text = "Third item", Description="This is a nice description", Code="09009"},
                new Item { Id = 4, Text = "Third item", Description="This is a nice description", Code="09009"},
                new Item { Id = 5, Text = "Third item", Description="This is a nice description", Code="09009"},
                new Item { Id = 6, Text = "Third item", Description="This is a nice description", Code="09009"},
                new Item { Id = 7, Text = "Third item", Description="This is a nice description", Code="09009"},
                new Item { Id = 8, Text = "Third item", Description="This is a nice description", Code="09009"},
                new Item { Id = 9, Text = "Third item", Description="This is a nice description", Code="09009"},
                new Item { Id = 10, Text = "Sixth item", Description="This is a nice description", Code="Codabar"},
            };

            foreach (Item item in _items)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var _item = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<Item>> GetAttendancesAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(events);
        }
    }
}
