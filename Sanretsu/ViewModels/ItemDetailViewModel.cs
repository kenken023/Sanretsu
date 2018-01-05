using Sanretsu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Sanretsu
{
    public class ItemDetailViewModel : BaseViewModel<Attendance>
    {
        public Attendance Item { get; set; }
        public ItemDetailViewModel(Attendance item = null)
        {
            Title = item.Name;
            Item = item;
        }
    }
}
