using SQLite;

namespace Sanretsu.Models
{
    public class TodoItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        public string Address
        {
            get; set;
        }
    }
}
