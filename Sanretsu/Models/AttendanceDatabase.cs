using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace Sanretsu.Models
{
    public class AttendanceDatabase
    {
        SQLiteAsyncConnection database;

        public AttendanceDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Attendance>().Wait();
        }

        public Task<List<Attendance>> GetItemsAsync()
        {
            return database.Table<Attendance>().ToListAsync();
        }

        public Task<Attendance> GetItemAsync(int id)
        {
            return database.Table<Attendance>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveItemAsync(Attendance item)
        {
            if (item.Id != 0)
            {
                return database.UpdateAsync(item);
            }

            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(Attendance item)
        {
            return database.DeleteAsync(item);
        }
    }
}
