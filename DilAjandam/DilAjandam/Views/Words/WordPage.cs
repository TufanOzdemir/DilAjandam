using DataAccessLayer.Services;
using DilAjandam.Helpers;
using DilAjandam.Views.Words;
using Helpers.UI;
using ImageCircle.Forms.Plugin.Abstractions;
using Models;
using System.Collections.Generic;
using Xamarin.CustomViews.Enums;
using Xamarin.CustomViews.Views;
using Xamarin.Forms;
using static Common.Enums;

namespace DilAjandam.Views
{
    public class WordPage : RefreshablePage
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
            this.BackgroundColor = UserSettings.PageColor;
            this.Title = string.IsNullOrWhiteSpace(prefix) ? MenuItemType.All.ToString() : prefix;
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
                DynamicGrid dynamicGrid = new DynamicGrid(Xamarin.CustomViews.Enums.DynamicGridEnum.Custom, 20, 34, 40, 6) { Padding = 0, Margin = 0, RowSpacing = 0, ColumnSpacing = 0 };
                dynamicGrid.AddView(new Label() { VerticalOptions = LayoutOptions.Center, FontAttributes = FontAttributes.Bold, TextColor = TextExtensions.GetTextColor(item.Type), Text = item.Type.ToString(), Margin = 0 });
                dynamicGrid.AddView(new Label() { VerticalOptions = LayoutOptions.Center, Text = item.Key, TextColor = UserSettings.TextColor, Margin = new Thickness(5, 0, 0, 0) });
                dynamicGrid.AddView(new Label() { HorizontalTextAlignment = TextAlignment.Center, VerticalTextAlignment = TextAlignment.Center, TextColor = UserSettings.TextColor, Text = item.Description, Margin = 0 });

                dynamicGrid.AddView(new CircleImage() { Source = "delete.png", GestureRecognizers = { new TapGestureRecognizer() { Command = new Command(DeleteButtonPressed), CommandParameter = item } } });
                tablestack.Children.Add(dynamicGrid);
                tablestack.Children.Add(new Line(LineEnum.Horizontal, UserSettings.MainColor) { Margin = 0 });
            }
            scrollView.Content = tablestack;
            return scrollView;
        }
        private async void PlusBarClicked()
        {
            await Navigation.PushAsync(new WordCreatePage());
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
            mainStack.Children.Add(new TitleComponent($"Kelimeler ({_wordList.Count} adet)"));
            absoluteLayout.Children.Add(sl, new Rectangle(0, 0, 1, 1), AbsoluteLayoutFlags.All);
            absoluteLayout.Children.Add(new Button() { Text = "+", Command = new Command(PlusBarClicked), FontSize = 20, FontAttributes = FontAttributes.Bold, WidthRequest = 50, HeightRequest = 50, CornerRadius = 25, BackgroundColor = UserSettings.ButtonColor, TextColor = UserSettings.NavigationTextColor }, new Rectangle(0.95, 0.95, 50, 50), AbsoluteLayoutFlags.PositionProportional);
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
            if (_wordService.Delete(model))
            {
                MainPage.RefreshPages();
            }
        }

        public override void Refresh()
        {
            GetData(prefix);
            ComponentLoad();
        }
    }
}
