using DataAccessLayer.Services;
using DilAjandam.Helpers;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using static Common.Enums;

namespace DilAjandam.Views.Words
{
    public class WordCreatePage : ContentPage
    {
        WordPage _mainPage;
        Entry keyEntry;
        string typeSelect;
        Entry descriptionEntry;
        Picker typePicker;
        Label label;
        WordService _wordService;
        public WordCreatePage(WordPage page)
        {
            _mainPage = page;
            Title = "Kelime Ekle";
            _wordService = DependencyContainerHelper.WordService;
            ComponentLoad();
        }

        public void ComponentLoad()
        {
            ScrollView scrollView = new ScrollView();
            StackLayout stackLayout = new StackLayout() { VerticalOptions = LayoutOptions.Center };

            Frame frame = new Frame() { BackgroundColor = Color.Beige };

            keyEntry = new Entry
            {
                Placeholder = "Key"
            };

            descriptionEntry = new Entry
            {
                Placeholder = "Description"
            };

            typePicker = new Picker
            {
                Title = "Kelime Tipi Seçiniz"
            };

            label = new Label
            {
                TextColor = Color.Red
            };

            var saveBtn = new Button
            {
                Text = "Kaydet",
                Command = new Command(this.SaveBtnClick),
                BackgroundColor = Color.Beige,
                TextColor = Color.Black
            };

            FillPicker();

            stackLayout.Children.Add(typePicker);
            stackLayout.Children.Add(keyEntry);
            stackLayout.Children.Add(descriptionEntry);
            stackLayout.Children.Add(saveBtn);
            stackLayout.Children.Add(label);

            frame.Content = stackLayout;

            scrollView.Content = frame;
            Content = scrollView;
        }

        private List<WordType> GetWordTypes()
        {
            return Enum.GetValues(typeof(WordType)).Cast<WordType>().ToList();
        }

        public void FillPicker()
        {
            typePicker.Items.Clear();
            var list = GetWordTypes();
            foreach (var item in list)
            {
                typePicker.Items.Add(item.ToString());
            }
        }

        private void SaveBtnClick(object obj)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(keyEntry.Text) && keyEntry.Text != "Yeni bir tip ekle")
                {
                    var type = GetWordTypes().FirstOrDefault(i => i.ToString() == typePicker.SelectedItem.ToString());
                    if (type != null)
                    {
                        _wordService.Create(new Word() { Key = keyEntry.Text, Description = descriptionEntry.Text, Id = Guid.NewGuid().ToString(), PrefixKey = keyEntry.Text[0].ToString().ToUpper(), Type = type });
                        _mainPage.Refresh();
                        Navigation.PopAsync();
                    }
                    else
                    {
                        label.Text = "Kelime Tipi bulunamadı!";
                    }
                }
                else
                {
                    label.Text = "Kelime tipi alanı boş geçilemez";
                }
            }
            catch (Exception ex)
            {
                label.Text = "İstenmeyen bir durum oluştu! Lütfen tüm alanları doğru doldurduğunuzdan emin olun!";
            }
        }
    }
}
