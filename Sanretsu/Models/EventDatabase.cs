using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Sanretsu.Models
{
    public class EventDatabase
    {
        SQLiteAsyncConnection database;

        public EventDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Event>().Wait();
        }

        public Task<List<Event>> GetItemsAsync()
        {
            return database.Table<Event>().ToListAsync();
        }

        public Task<Event> GetItemAsync(int id)
        {
            return database.Table<Event>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Event item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }

            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Event item)
        {
            return database.DeleteAsync(item);
        }
    }
}
