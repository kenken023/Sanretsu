using Sanretsu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sanretsu.Services
{
    public class AttendanceDataStore : IDataStore<Attendance>
    {
        List<Attendance> items;

        public AttendanceDataStore()
        {
            items = new List<Attendance>();
        }

        public async Task<bool> AddItemAsync(Attendance item)
        {
            await App.AttendanceDb.SaveItemAsync(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Attendance item)
        {
            var _item = items.Where((Attendance arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Attendance arg) => arg.Id == id).FirstOrDefault();
            items.Remove(_item);

            await App.AttendanceDb.DeleteItemAsync(_item);

            return await Task.FromResult(true);
        }
        public async Task<Attendance> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Attendance>> GetItemsAsync(bool forceRefresh = false)
        {
            items = await App.AttendanceDb.GetItemsAsync();
            return await Task.FromResult(items);
        }



        public async Task<bool> DeleteItemAsync(string id)
        {
            throw new Exception("Not implemented.");
        }

        public async Task<Attendance> GetItemAsync(string id)
        {
            throw new Exception("Not implemented.");
        }

        public async Task<List<Attendance>> GetItemsAsync(string id)
        {
            throw new Exception("Not implemented.");
        }

    }
}
