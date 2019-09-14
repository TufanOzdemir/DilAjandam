using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Common.Enums;

namespace DilAjandam.Views
{
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            Title = MenuItemType.About.ToString();
            Content = new Label()
            {
                Text = "Bu uygulama Bilge Tufan Özdemir tarafından E-Bird Software için yapılmıştır. " +
                "Uygulamayı beğendiyseniz Kumbaram isimli uygulamayı denemelisiniz."
            };
        }
    }
}