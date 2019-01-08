using Eirpoint.Mobile.Datasource.Interfaces;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using SQLite;
using System;
using System.IO;

namespace Eirpoint.Mobile.Datasource.Repository
{
    public static class PlatformDatabase
    {      
        #region Fields        

        private static string _databaseFolder;

        #endregion

        #region Properties        

        //database path
        public static string DatabasePath { get { return Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "/" + _databaseFolder; } }

        //database name
        public static string DatabaseName { get; private set; }

        //sqlite connection
        public static SQLiteAsyncConnection SQLiteConnection { get; private set; }

        #endregion

        #region Methods        

        /// <summary>
        /// Create database by path
        /// </summary>
        public static void CreateDatabase(string databaseFolder, string databaseName)
        {
            _databaseFolder = databaseFolder;
            DatabaseName = databaseName;

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
