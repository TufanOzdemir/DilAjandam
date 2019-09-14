using Models.Interface;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDBProvider
    {
        int Insert(object model);

        List<T> GetAll<T>() where T : IEntity, new();

        T Get<T>(string id) where T : IEntity, new();

        bool Delete(object model);

        void CreateTable<T>();
    }
}
