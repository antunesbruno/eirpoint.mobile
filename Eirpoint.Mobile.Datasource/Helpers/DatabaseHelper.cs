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
            PlatformDatabase.CreateTable<CustomersEntity>();
            PlatformDatabase.CreateTable<DepartmentEntity>();
            PlatformDatabase.CreateTable<DiscountsEntity>();
            PlatformDatabase.CreateTable<PaymentsCategoriesEntity>();
            PlatformDatabase.CreateTable<GroupsEntity>();
            PlatformDatabase.CreateTable<PosStationsEntity>();
            PlatformDatabase.CreateTable<ProductsEntity>();
            PlatformDatabase.CreateTable<ProductBarCodesEntity>();
            PlatformDatabase.CreateTable<PromotionsEntity>();
            PlatformDatabase.CreateTable<ReasonsEntity>();
            PlatformDatabase.CreateTable<ReceiptTemplatesEntity>();
            PlatformDatabase.CreateTable<StockingAttributeTypeEntity>();
            PlatformDatabase.CreateTable<StockLocationsEntity>();
            PlatformDatabase.CreateTable<UsersEntity>();
            PlatformDatabase.CreateTable<BarcodesEntity>();
            PlatformDatabase.CreateTable<RangeHeaderLogEntity>();
            PlatformDatabase.CreateTable<ConfigurationEntity>();
        }
    }
}
