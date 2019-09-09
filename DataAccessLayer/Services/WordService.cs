using DataAccessLayer.Providers;
using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace DataAccessLayer.Services
{
    public class WordService : IService<Word>
    {
        IDBProvider _dataContext { get; set; }

        public WordService(IDBProvider dBProvider)
        {
            _dataContext = dBProvider;
        }

        public void Create(Word word)
        {
            try
            {
                _dataContext.Insert(word);
            }
            catch (Exception ex)
            {
            }
        }

        public List<Word> GetAll()
        {
            List<Word> result;
            try
            {
                result = _dataContext.GetAll<Word>();
            }
            catch (Exception ex)
            {
                result = new List<Word>();
            }
            return result;
        }

        public Word GetById(string id)
        {
            Word result;
            try
            {
                result = _dataContext.Get<Word>(id);
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }

        public void Delete(Word model)
        {
            try
            {
                _dataContext.Delete(model);
            }
            catch (Exception ex)
            {
            }
        }
    }
}
