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
                Name = "Item name",
                Description = "This is a nice description"
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
