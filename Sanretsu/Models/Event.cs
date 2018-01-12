using System;

using Newtonsoft.Json;
using SQLite;

namespace Sanretsu.Models
{
    public class Event: ObservableObject
    {
        int id = 0;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        string name = string.Empty;
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        DateTime? dateTime = null;
        public DateTime? DateTime
        {
            get { return dateTime; }
            set { SetProperty(ref dateTime, value); }
        }
    }
}
