using DataAccessLayer.Providers;
using DilAjandam.Droid;
using Interfaces;
using System;
using System.IO;


[assembly: Xamarin.Forms.Dependency(typeof(SQLiteDroid))]
namespace DilAjandam.Droid
{
    public class SQLiteDroid : IDBProviderPlatform
    {
        public IDBProvider Connection()
        {
            var sqliteFilename = "mydb.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            Console.WriteLine(path);
            if (!File.Exists(path)) File.Create(path);
            var conn = new SQLiteProvider(path);
            return conn;
        }
    }
}