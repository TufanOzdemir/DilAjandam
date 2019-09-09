using DilAjandam.iOS;
using Interfaces;
using System;

[assembly: Xamarin.Forms.Dependency(typeof(SQLiteIOS))]
namespace DilAjandam.iOS
{
    public class SQLiteIOS : IDBProviderPlatform
    {
        public IDBProvider Connection()
        {
            throw new NotImplementedException();
        }
    }
}