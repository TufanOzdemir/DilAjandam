using DataAccessLayer.Services;
using DilAjandam.Helpers;
using DilAjandam.Views.Words;
using ImageCircle.Forms.Plugin.Abstractions;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CustomViews.Enums;
using Xamarin.CustomViews.Views;
using Xamarin.Forms;

namespace DilAjandam.Views
{
    public class WordPage : ContentPage
    {
        WordService _wordService;
        List<Word> _wordList;
        string prefix;

        public WordPage(string prefix)
        {
            this.prefix = prefix;
            GetSettings();
            GetData(prefix);
            ComponentLoad();
        }
        public WordPage()
        {
            GetSettings();
            GetData("");
            ComponentLoad();
        }

        private void GetSettings()
        {
            _wordService = DependencyContainerHelper.WordService;
        }

        private void GetData(string prefix)
        {
            _wordList = string.IsNullOrWhiteSpace(prefix) ? _wordService.GetAll() : _wordService.GetAll(prefix);
        }

        private ScrollView GetTable()
        {
            ScrollView scrollView = new ScrollView() { Padding = 0, Margin = 0 };
            StackLayout tablestack = new StackLayout() { Padding = 0, Margin = 0, Spacing = -2 };
            foreach (var item in _wordList)
            {
                DynamicGrid dynamicGrid = new DynamicGrid(Xamarin.CustomViews.Enums.DynamicGridEnum.Custom, 20, 42,  38, 6) { Padding = 0, Margin = 0, RowSpacing = 0, ColumnSpacing = 0 };
                dynamicGrid.AddView(new Label() { VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, Text = item.Type.ToString(), Margin = 0 });
                dynamicGrid.AddView(new Label() { VerticalOptions = LayoutOptions.Center, Text = item.Key, Margin = new Thickness(5, 0, 0, 0) });
                dynamicGrid.AddView(new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, Text = item.Description, Margin = 0 });

                dynamicGrid.AddView(new CircleImage() { Source = "delete.png", GestureRecognizers = { new TapGestureRecognizer() { Command = new Command(DeleteButtonPressed), CommandParameter = item } } });
                tablestack.Children.Add(dynamicGrid);
                tablestack.Children.Add(new Line(LineEnum.Horizontal, Color.FromHex("#F1E6D6")) { Margin = 0 });
            }
            scrollView.Content = tablestack;
            return scrollView;
        }
        private async void PlusBarClicked()
        {
            await Navigation.PushAsync(new WordCreatePage(this));
        }

        public void ComponentLoad()
        {
            Padding = new Thickness(10, 0, 10, 5);
            AbsoluteLayout absoluteLayout = new AbsoluteLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand };
            StackLayout sl = new StackLayout
            {
                Children =
                {
                    GetTable()
                }
            };
            StackLayout mainStack = new StackLayout() { HorizontalOptions = LayoutOptions.FillAndExpand, VerticalOptions = LayoutOptions.FillAndExpand, Padding = new Thickness(0, 5, 0, 0), Margin = 0, Spacing = 10 };
            mainStack.Children.Add(new TitleComponent($"Planlarım ({_wordList.Count} adet)"));
            absoluteLayout.Children.Add(sl, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            absoluteLayout.Children.Add(new Button() { Text = "+", Command = new Command(PlusBarClicked), FontSize = 20, FontAttributes = FontAttributes.Bold, WidthRequest = 50, HeightRequest = 50, CornerRadius = 25, BackgroundColor = Color.Beige, TextColor = Color.Black }, new Rectangle(0.95, 0.95, 50, 50), AbsoluteLayoutFlags.PositionProportional);
            mainStack.Children.Add(absoluteLayout);
            Content = mainStack;
        }

        private async void DeleteButtonPressed(object obj)
        {
            Word model = (Word)obj;
            var select = await DisplayActionSheet("Uyarı", "Evet", "Hayır", "Silmek istediğinize emin misiniz?");
            if (string.IsNullOrWhiteSpace(select) || select == "Hayır")
            {
                return;
            }
            _wordService.Delete(model);
            Refresh();
        }

        public void Refresh()
        {
            GetData(prefix);
            ComponentLoad();
        }
    }
}
