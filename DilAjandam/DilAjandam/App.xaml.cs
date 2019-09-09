using System;
using Autofac;
using DataAccessLayer.Providers;
using DataAccessLayer.Services;
using DilAjandam.Helpers;
using DilAjandam.Views;
using Interfaces;
using Models;
using Xamarin.Forms;

namespace DilAjandam
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            DependencyContainerHelper.Initialize();
            //DatabaseInitialize(DependencyContainerHelper.Builder);
            MainPage = new MainPage();
        }

        //private void DatabaseInitialize(ContainerBuilder builder)
        //{
        //    var dbContext = DependencyService.Get<IDBProviderPlatform>().Connection();
        //    builder.RegisterInstance<WordService>(new WordService(dbContext)).SingleInstance();
        //    dbContext.CreateTable<Word>();
        //    builder.Build();
        //}

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
