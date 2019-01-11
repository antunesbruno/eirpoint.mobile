using Eirpoint.Mobile.Datasource.Interfaces;
using Eirpoint.Mobile.Datasource.Repository;
using Eirpoint.Mobile.Datasource.Repository.Entity;

namespace Eirpoint.Mobile.Datasource.Helpers
{
    public class DatabaseHelper : IDatabaseHelper
    {
        private readonly string DATABASE_FOLDER = "eirpoint";
        private readonly string DATABASE_NAME = "eirpoint-mobile.db3";

        public void CreateDatabase()
        {
            //create database
            PlatformDatabase.CreateDatabase(DATABASE_FOLDER, DATABASE_NAME);
        }

        public void CreateTables()
        {
            PlatformDatabase.CreateTable<ProductsEntity>();
            PlatformDatabase.CreateTable<BarcodesEntity>();
            PlatformDatabase.CreateTable<CreatedByUserEntity>();
            PlatformDatabase.CreateTable<DepartmentEntity>();
            PlatformDatabase.CreateTable<PacksEntity>();
            PlatformDatabase.CreateTable<SaleLocationsEntity>();
            PlatformDatabase.CreateTable<SeasonEntity>();
            PlatformDatabase.CreateTable<StockItemsEntity>();
            PlatformDatabase.CreateTable<TaxCategoryEntity>();
            PlatformDatabase.CreateTable<SelfEntity>();
            PlatformDatabase.CreateTable<SubDepartmentEntity>();            
        }
    }
}
