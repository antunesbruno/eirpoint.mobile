using Eirpoint.Mobile.Datasource.Api;
using Eirpoint.Mobile.Datasource.Repository.Entity;
using Eirpoint.Mobile.Shared.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Helpers
{
    public static class EntityResourcesHelper
    {
        public static Dictionary<System.Type, string> GetEntityTypes()
        {
            Dictionary<System.Type, string> dicResources = new Dictionary<System.Type, string>();

            dicResources.Add(typeof(CustomersEntity), Endpoints.CUSTOMERS);
            dicResources.Add(typeof(DepartmentEntity), Endpoints.DEPARTMENT);
            dicResources.Add(typeof(DiscountsEntity), Endpoints.DISCOUNT);
            dicResources.Add(typeof(PaymentsCategoriesEntity), Endpoints.PAYMENT_CATEGORY);
            dicResources.Add(typeof(GroupsEntity), Endpoints.GROUP_LIST);
            dicResources.Add(typeof(PosStationsEntity), Endpoints.POS_STATION);
            dicResources.Add(typeof(ProductsEntity), Endpoints.PRODUCT);
            dicResources.Add(typeof(ProductBarCodesEntity), Endpoints.PRODUCT_BARCODE);
            dicResources.Add(typeof(PromotionsEntity), Endpoints.PROMOTIONS);
            dicResources.Add(typeof(ReasonsEntity), Endpoints.REASON);
            dicResources.Add(typeof(ReceiptTemplatesEntity), Endpoints.RECEIPT);
            dicResources.Add(typeof(StockingAttributeTypeEntity), Endpoints.STOCKING_ATTRIBUTE_TYPE);
            dicResources.Add(typeof(StockLocationsEntity), Endpoints.STOCK_LOCATION);
            dicResources.Add(typeof(UsersEntity), Endpoints.USER_LIST);

            return dicResources;
        }

        public static Dictionary<AltoposResourcesEnum, string> GetValues()
        {
            Dictionary<AltoposResourcesEnum, string> dicResources = new Dictionary<AltoposResourcesEnum, string>();

            dicResources.Add(AltoposResourcesEnum.CUSTOMER, Endpoints.CUSTOMERS);
            dicResources.Add(AltoposResourcesEnum.DEPARTMENT, Endpoints.DEPARTMENT);
            dicResources.Add(AltoposResourcesEnum.DISCOUNT, Endpoints.DISCOUNT);
            dicResources.Add(AltoposResourcesEnum.PAYMENT_CATEGORY, Endpoints.PAYMENT_CATEGORY);
            dicResources.Add(AltoposResourcesEnum.GROUP_LIST, Endpoints.GROUP_LIST);
            dicResources.Add(AltoposResourcesEnum.POS_STATION, Endpoints.POS_STATION);
            dicResources.Add(AltoposResourcesEnum.PRODUCT, Endpoints.PRODUCT);
            dicResources.Add(AltoposResourcesEnum.PRODUCT_BARCODE, Endpoints.PRODUCT_BARCODE);
            dicResources.Add(AltoposResourcesEnum.PROMOTIONS, Endpoints.PROMOTIONS);
            dicResources.Add(AltoposResourcesEnum.REASON, Endpoints.REASON);
            dicResources.Add(AltoposResourcesEnum.RECEIPT, Endpoints.RECEIPT);
            dicResources.Add(AltoposResourcesEnum.STOCKING_ATTRIBUTE_TYPE, Endpoints.STOCKING_ATTRIBUTE_TYPE);
            dicResources.Add(AltoposResourcesEnum.STOCK_LOCATION, Endpoints.STOCK_LOCATION);
            dicResources.Add(AltoposResourcesEnum.USER_LIST, Endpoints.USER_LIST);

            return dicResources;
        }
    }
}
