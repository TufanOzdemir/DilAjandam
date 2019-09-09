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

        List<T> GetAll<T>() where T : new();

        T Get<T>(string id) where T : IEntity;

        int Delete(object model);

        void CreateTable<T>();
    }
}
