using Sanretsu.Views;
using System.Collections.Generic;

using Xamarin.Forms;
using Sanretsu.Models;
using Sanretsu.Dependencies;

namespace Sanretsu
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";
        static TodoItemDatabase database;

        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
            {
                DependencyService.Register<MockDataStore>();
                DependencyService.Register<EventDataStore>();
            }
            else
            {
                DependencyService.Register<CloudDataStore>();
            }

            Database.DeleteItemAsync(new TodoItem()
            {
                Name = "Koken",
                Address = "Bontoc"
            });

            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (!UseMockDataStore && !Settings.IsLoggedIn)
            {
                Current.MainPage = new NavigationPage(new LoginPage())
                {
                    BarBackgroundColor = (Color)Current.Resources["Primary"],
                    BarTextColor = Color.White
                };
            }
            else
            {
                GoToMainPage();
            }
        }

        public static void GoToMainPage()
        {
            Current.MainPage = new TabbedPage
            {
                Children = {
                    new NavigationPage(new ItemsPage())
                    {
                        Title = "Events",
                        Icon = "tab_feed.png"

                    },
                    new NavigationPage(new ScanPage()) {
                        Title = "Test Scan",
                        Icon = "tab_feed.png"
                    },
                    new NavigationPage(new SettingsPage())
                    {
                        Title = "Settings",
                        Icon = "tab_feed.png"
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = "tab_about.png"
                    }
                }
            };
        }

        public static TodoItemDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new TodoItemDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("TodoSQLite.db3"));
                }
                return database;
            }
        }
    }
}
