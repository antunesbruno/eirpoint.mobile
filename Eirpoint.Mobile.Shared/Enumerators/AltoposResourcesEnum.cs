using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Eirpoint.Mobile.Shared.Enumerators
{
    public enum AltoposResourcesEnum
    {
        DEPARTMENT = 0,
        DISCOUNT = 1,
        GROUP_LIST = 2,
        PAYMENT_CATEGORY = 3,
        POS_STATION = 4,
        PRODUCT = 5,
        PRODUCT_BARCODE = 6,
        REASON = 7,
        RECEIPT = 8,
        STOCKING_ATTRIBUTE_TYPE = 9,
        STOCK_LOCATION = 10,
        USER_LIST = 11
    }

    public static class AltoposResources
    {
        public static Dictionary<AltoposResourcesEnum, string> GetValues()
        {
            Dictionary<AltoposResourcesEnum, string> dicResources = new Dictionary<AltoposResourcesEnum, string>();

            dicResources.Add(AltoposResourcesEnum.DEPARTMENT, "/departments");
            dicResources.Add(AltoposResourcesEnum.DISCOUNT, "/discounts");
            dicResources.Add(AltoposResourcesEnum.GROUP_LIST, "/groups");
            dicResources.Add(AltoposResourcesEnum.PAYMENT_CATEGORY, "/paymentcategories");
            dicResources.Add(AltoposResourcesEnum.POS_STATION, "/posstations");
            dicResources.Add(AltoposResourcesEnum.PRODUCT, "/products");
            dicResources.Add(AltoposResourcesEnum.PRODUCT_BARCODE, "/productbarcodes");
            dicResources.Add(AltoposResourcesEnum.REASON, "/reasons");
            dicResources.Add(AltoposResourcesEnum.RECEIPT, "/receipttemplates");
            dicResources.Add(AltoposResourcesEnum.STOCKING_ATTRIBUTE_TYPE, "/stockingattributetypes");
            dicResources.Add(AltoposResourcesEnum.STOCK_LOCATION, "/stocklocations");
            dicResources.Add(AltoposResourcesEnum.USER_LIST, "/users");

            return dicResources;
        }
    }
}
