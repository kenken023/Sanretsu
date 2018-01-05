using Sanretsu.Models;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Sanretsu
{
    public partial class NewItemPage : ContentPage
    {
        public Event Item { get; set; }

        public NewItemPage()
        {
            InitializeComponent();

            Item = new Event
            {
                Name = "Event name",
                Description = "Description here",
                DateTime = DateTime.Now
            };

            BindingContext = this;
        }

        async void Save_Clicked(object sender, EventArgs e)
        {
            MessagingCenter.Send(this, "AddItem", Item);
            await Navigation.PopToRootAsync();
        }
    }
}
