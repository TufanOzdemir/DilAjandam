using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DilAjandam.Views
{
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            //InitializeComponent();
            Content = new Label() { Text = "Hakkımızda" };
        }
    }
}