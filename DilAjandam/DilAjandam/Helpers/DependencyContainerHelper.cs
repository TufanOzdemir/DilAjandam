﻿using Autofac;
using DataAccessLayer.Providers;
using DataAccessLayer.Services;
using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DilAjandam.Helpers
{
    public class DependencyContainerHelper
    {
        private static IContainer _container;

        private static ContainerBuilder _builder;

        public static IDBProvider DbProvider { get { return _container.Resolve<IDBProvider>(); } }
        public static WordService WordService { get { return _container.Resolve<WordService>(); } }
        public static ContainerBuilder Builder { get { return _builder; } }

        public static void Initialize()
        {
            if (_container == null)
            {
                _builder = new ContainerBuilder();
                _builder.RegisterType<SQLiteProvider>().As<IDBProvider>().SingleInstance();
                var dbContext = DependencyService.Get<IDBProviderPlatform>().Connection();
                _builder.RegisterInstance(new WordService(dbContext)).SingleInstance();
                _container = _builder.Build();
                DatabaseInitialize(dbContext);
            }
        }

        private static void DatabaseInitialize(IDBProvider dbContext)
        {
            dbContext.CreateTable<Word>();
            //dbContext.Insert(new Word() { Key = "Attribute", PrefixKey = "A", Type = Common.Enums.WordType.Adjective, Description = "Özellik", Id = Guid.NewGuid().ToString() });
            //dbContext.Insert(new Word() { Key = "Beta", PrefixKey = "B", Type = Common.Enums.WordType.Verb, Description = "Test", Id = Guid.NewGuid().ToString() });
            //dbContext.Insert(new Word() { Key = "Case", PrefixKey = "C", Type = Common.Enums.WordType.Noun, Description = "Şart", Id = Guid.NewGuid().ToString() });
            //dbContext.Insert(new Word() { Key = "Akinon", PrefixKey = "A", Type = Common.Enums.WordType.Adverb, Description = "Şirket", Id = Guid.NewGuid().ToString() });
        }
    }
}
