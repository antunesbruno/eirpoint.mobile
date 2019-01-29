using SQLite;
using System;
using System.IO;

namespace Eirpoint.Mobile.Datasource.Repository
{
    public static class PlatformDatabase
    {
        #region Fields        

        private static string _absolutePath;
        private static string _databaseFolder;
        private static string _databaseName;
        

        #endregion

        #region Properties        

        //absolute path setted by OS
        public static string AbsolutePathOS { get { return _absolutePath; } }

        //database path (Production: Environment.GetFolderPath(Environment.SpecialFolder.Personal)
        public static string DatabasePath { get { return AbsolutePathOS + "/" + _databaseFolder; } }

        //database name
        public static string DatabaseName { get { return _databaseName; } }

        //sqlite connection
        public static SQLiteAsyncConnection SQLiteConnection { get; private set; }

        #endregion

        #region Methods        

        /// <summary>
        /// Create database by path
        /// </summary>
        public static void CreateDatabase(string absolutePath = null, string databaseFolder = null, string databaseName = null)
        {
            _absolutePath = absolutePath;
            _databaseFolder = databaseFolder;
            _databaseName = databaseName;

            if (!string.IsNullOrEmpty(databaseFolder) && !string.IsNullOrEmpty(databaseName))
            {
                if (!Directory.Exists(DatabasePath))
                    Directory.CreateDirectory(DatabasePath);

                SQLiteConnection = new SQLiteAsyncConnection(Path.Combine(DatabasePath + "/" + DatabaseName));
            }
        }

        /// <summary>
        /// Create tables by entity parameter
        /// </summary>
        /// <typeparam name="T">Entity / Table</typeparam>
        public static void CreateTable<T>() where T : class, new()
        {
            SQLiteConnection.CreateTableAsync<T>(CreateFlags.None);
        }

        /// <summary>
        /// Dispose database connection
        /// </summary>
        public static void Dispose()
        {
            SQLiteConnection.CloseAsync();
        }

        #endregion
    }
}
