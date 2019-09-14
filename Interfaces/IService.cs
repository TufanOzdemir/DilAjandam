using Models.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IService<T> where T : IEntity
    {
        void Create(T word);

        List<T> GetAll();

        T GetById(string id);

        bool Delete(T model);
    }
}
