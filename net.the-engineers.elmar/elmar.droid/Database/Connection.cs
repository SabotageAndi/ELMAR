using System;
using System.IO;
using elmar.droid.Common;
using SQLite.Net;
using SQLite.Net.Interop;

namespace elmar.droid.Database
{
    class Connection
    {
        private readonly Lazy<SQLiteConnection> _connection;

        public Connection()
        {
            //var databasePath = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.AbsolutePath,  "elmar.db");
            var databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),  "elmar.db");
            _connection = new Lazy<SQLiteConnection>(() => new SQLiteConnection(new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid(), databasePath));
        }

        public SQLiteConnection Current => _connection.Value;


        public void CreateOrUpdateSchema()
        {
            var table = Current.CreateTable<Event>(CreateFlags.None);
        }
    }
}