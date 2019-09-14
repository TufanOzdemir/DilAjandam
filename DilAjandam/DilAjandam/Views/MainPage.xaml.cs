using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using DilAjandam.Models;
using static Common.Enums;
using DilAjandam.Views.Words;
using System.Linq;
using Helpers.UI;

namespace DilAjandam.Views
{
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        static Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();
            BackgroundColor = UserSettings.PageColor;
            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.All, new NavigationPage(new WordPage()) { BarBackgroundColor = UserSettings.MainColor, BarTextColor = UserSettings.NavigationTextColor });
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                if (id != (int)MenuItemType.All && id != (int)MenuItemType.About)
                {
                    MenuPages.Add(id, new NavigationPage(new WordPage(((MenuItemType)id).ToString())) { BarBackgroundColor = UserSettings.MainColor, BarTextColor = UserSettings.NavigationTextColor });
                }
                else
                {
                    switch (id)
                    {
                        case (int)MenuItemType.About:
                            MenuPages.Add(id, new NavigationPage(new AboutPage()) { BarBackgroundColor = UserSettings.MainColor, BarTextColor = UserSettings.NavigationTextColor });
                            break;
                    }
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }

        public static void RefreshPages()
        {
            var pages = MenuPages.ToList();
            foreach (var item in pages)
            {
                var refp = (RefreshablePage)item.Value.RootPage;
                refp.Refresh();
            }
        }
    }
}