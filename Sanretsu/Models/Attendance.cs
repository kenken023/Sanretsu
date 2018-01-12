using System;

using Newtonsoft.Json;
using SQLite;

namespace Sanretsu.Models
{
    public class Attendance : ObservableObject
    {
        int id;

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        string code = string.Empty;
        public string Code
        {
            get { return code;  }
            set { SetProperty(ref code, value); }
        }

        string text = string.Empty;
        public string Name
        {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        string description = string.Empty;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        int eventId;
        public int EventId
        {
            get { return eventId; }
            set { SetProperty(ref eventId, value); }
        }
    }
}
