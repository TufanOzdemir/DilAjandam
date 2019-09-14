using DataAccessLayer.Providers;
using Interfaces;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace DataAccessLayer.Services
{
    public class WordService : IService<Word>
    {
        IDBProvider _dataContext { get; set; }
        Dictionary<string, List<Word>> _wordDictionary;

        public WordService(IDBProvider dBProvider)
        {
            _dataContext = dBProvider;
            RefreshData();
        }

        public void Create(Word word)
        {
            try
            {
                _dataContext.Insert(word);
                RefreshData();
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

        public List<Word> GetAll(string prefixKey)
        {
            List<Word> result;
            try
            {
                var listCheck = _wordDictionary.TryGetValue(prefixKey, out result);
                if (!listCheck)
                {
                    result = new List<Word>();
                }
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

        public bool Delete(Word model)
        {
            bool result = _dataContext.Delete(model);
            if (result)
            {
                RefreshData();
            }
            return result;
        }

        private void RefreshData()
        {
            _wordDictionary = GetAll().GroupBy(c => c.PrefixKey).ToList().ToDictionary(c => c.Key, t => t.ToList());
        }
    }
}
