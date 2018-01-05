using Sanretsu.Views;
using System.Collections.Generic;

using Xamarin.Forms;
using Sanretsu.Models;
using Sanretsu.Dependencies;
using Sanretsu.Services;

namespace Sanretsu
{
    public partial class App : Application
    {
        private static EventDatabase _eventDatabase;
        private static AttendanceDatabase _attendanceDatabase;

        public static bool UseSqliteDataStore = true;
        public static string BackendUrl = "https://localhost:5000";
        static TodoItemDatabase database;

        public static IDictionary<string, string> LoginParameters => null;

        public App()
        {
            InitializeComponent();

            if (UseSqliteDataStore)
            {
                DependencyService.Register<MockDataStore>();
                DependencyService.Register<EventDataStore>();
                DependencyService.Register<AttendanceDataStore>();
            }
            else
            {
                DependencyService.Register<CloudDataStore>();
            }

            SetMainPage();
        }

        public static void SetMainPage()
        {
            if (!UseSqliteDataStore && !Settings.IsLoggedIn)
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

        public static EventDatabase EventDb
        {
            get
            {
                if (_eventDatabase == null)
                {
                    _eventDatabase = new EventDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("Sanretsu.db3"));
                }

                return _eventDatabase;
            }
        }

        public static AttendanceDatabase AttendanceDb
        {
            get
            {
                if (_attendanceDatabase == null)
                {
                    _attendanceDatabase = new AttendanceDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath(("Sanretsu.db3")));
                }

                return _attendanceDatabase;
            }
        }

    }
}
