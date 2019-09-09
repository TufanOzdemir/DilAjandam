using DataAccessLayer.Services;
using DilAjandam.Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DilAjandam.Views
{
    public class WordPage : ContentPage
    {
        WordService _wordService;

        public WordPage(string prefix)
        {
            _wordService = DependencyContainerHelper.WordService;
            Content = new StackLayout() { Children = { new Label() { Text = prefix } } };
        }
        public WordPage()
        {
            _wordService = DependencyContainerHelper.WordService;
            GetComponents();
        }

        private void GetComponents()
        {
            var list = _wordService.GetAll();
            Content = new StackLayout() { Children = { new Label() { Text = "asdasd" } } };
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as Word;
            if (item == null)
                return;

            //await Navigation.PushAsync(new ItemDetailPage(new ItemDetailViewModel(item)));

            // Manually deselect item.
            //ItemsListView.SelectedItem = null;
        }
    }
}
