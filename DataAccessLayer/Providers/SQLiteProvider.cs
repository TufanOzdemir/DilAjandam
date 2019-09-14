using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Models.Interface;
using SQLite;

namespace DataAccessLayer.Providers
{
    public class SQLiteProvider : SQLiteConnection, IDBProvider
    {
        public SQLiteProvider(string databasePath, SQLiteOpenFlags openFlags, bool storeDateTimeAsTicks = true, object key = null)
            : base(databasePath, openFlags, storeDateTimeAsTicks, key)
        {

        }

        public SQLiteProvider(string databasePath, bool storeDateTimeAsTicks = true, object key = null)
            : base(databasePath, storeDateTimeAsTicks, key)
        {

        }

        public List<T> GetAll<T>() where T : IEntity, new()
        {
            return base.Table<T>().ToList();
        }

        public new T Get<T>(string id) where T : IEntity, new()
        {
            return this.GetAll<T>().FirstOrDefault(c => c.Id == id);
        }

        public new void CreateTable<T>()
        {
            base.CreateTable<T>();
        }

        public bool Delete(object model)
        {
            bool result;
            try
            {
                base.Delete(model);
                result = true;
            }
            catch (System.Exception ex)
            {
                result = false;
            }
            return result;
        }
    }
}
