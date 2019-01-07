using Eirpoint.Mobile.Datasource.Interfaces;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Platform.Ioc.Injection;
using Platform.Shared.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string DATABASE_FOLDER = "eirpoint";
        private readonly string DATABASE_NAME = "eirpoint-mobile.db3";

        public void CreateDatabase()
        {
            //create database
            Injector.Resolver<IPlatformDatabase>().CreateDatabase(DATABASE_FOLDER, DATABASE_NAME);
        }

        public void CreateTables()
        {
            Injector.Resolver<IPlatformDatabase>().CreateTable<ProductsEntity>();
        }
    }
}
