using Sanretsu.Views;
using System.Collections.Generic;

using Xamarin.Forms;
using Sanretsu.Models;
using Sanretsu.Dependencies;
using Sanretsu.Services.Database;
using Sanretsu.Services.Cloud;
using Sanretsu.Services.Mock;

namespace Sanretsu
{
    public partial class App : Application
    {
        private const string DATABASE_NAME = "Sanretsu.db3";
        
        private static EventDatabase _eventDatabase;
        private static AttendanceDatabase _attendanceDatabase;

        public static bool UseSqliteDataStore = true;
        public static string BackendUrl = "https://localhost:5000";

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

            //AttendanceDb.SaveItemAsync(new Attendance {
            //    Code = "00806906",
            //    Description = "For event 1",
            //    EventId = 1,
            //    Name = "Fritz Salar"
            //});


            //AttendanceDb.SaveItemAsync(new Attendance
            //{
            //    Code = "SOG000026",
            //    Description = "For event 1",
            //    EventId = 1,
            //    Name = "Kenken Salar"
            //});

            //AttendanceDb.SaveItemAsync(new Attendance
            //{
            //    Code = "00806907",
            //    Description = "For event 2",
            //    EventId = 2,
            //    Name = "Lupoy"
            //});


            //AttendanceDb.SaveItemAsync(new Attendance
            //{
            //    Code = "SOG000027",
            //    Description = "For event 2",
            //    EventId = 2,
            //    Name = "Kigwa"
            //});

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

        public static EventDatabase EventDb
        {
            get
            {
                if (_eventDatabase == null)
                {
                    _eventDatabase = new EventDatabase(GetLocalDatabase());
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
                    _attendanceDatabase = new AttendanceDatabase(GetLocalDatabase());
                }

                return _attendanceDatabase;
            }
        }

        private static string GetLocalDatabase()
        {
            return DependencyService.Get<IFileHelper>().GetLocalFilePath((DATABASE_NAME));
        }

    }
}
