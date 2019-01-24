using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Product")]
    public class ProductsEntity : EntityBase
    {
        public bool Active { get; set; }
        public bool Assembly { get; set; }
        public bool Restricted { get; set; }
        public DateTime? LiveDate { get; set; }
        public string LongDescription { get; set; }
        public string PosDescription { get; set; }
        public double? RetailPrice { get; set; }
        public double? RetailPrice1 { get; set; }
        public double? RetailPrice2 { get; set; }
        public double? RetailPrice3 { get; set; }
        public double? RetailPrice4 { get; set; }
        public bool OpenItem { get; set; }
        public bool SellByPrice { get; set; }
        public bool SellByValue { get; set; }
        public int? DepartmentId { get; set; }
        public int? SubDepartmentId { get; set; }
        public string Note { get; set; }
        public bool PromptForQuantity { get; set; }
        public bool AllowSales { get; set; }
        public bool AllowOrders { get; set; }
        public bool AllowSupplierReturns { get; set; }
        public bool AllowSalesReturns { get; set; }
        public bool AllowDiscount { get; set; }
        public string SalesMessage { get; set; }
        public bool SalesMessageOnScreen { get; set; }
        public bool SalesMessageOnPrinter { get; set; }
        public bool SalesMessageConfirmation { get; set; }
        public int? DefaultTransferQuantity { get; set; }
        public bool EligibleForLoyalty { get; set; }
        public string ProductUnit { get; set; }
        public bool PromptForIdentity { get; set; }
        public string VendorCode { get; set; }
        public int? StockingAttributeTypeAId { get; set; }
        public int? StockingAttributeTypeBId { get; set; }
        public int? StockingAttributeTypeCId { get; set; }
        public int? StockingAttributeTypeDId { get; set; }
        public int? StockingAttributeTypeEId { get; set; }
        public int? StockingAttributeAId { get; set; }
        public int? StockingAttributeBId { get; set; }
        public int? StockingAttributeCId { get; set; }
        public int? StockingAttributeDId { get; set; }
        public int? StockingAttributeEId { get; set; }
        public string ExternalProductCode { get; set; }
        public string DefaultBarcode { get; set; }
        public string ImageName { get; set; }
        public bool QuickLookup { get; set; }
        public bool VariableMeasure { get; set; }
        public double? WeeeCharge { get; set; }
        public bool PrintCollectionReceipt { get; set; }
        public bool IgnoreOpenItemPermission { get; set; }
        public string SyncUpDateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public bool MultiUseProduct { get; set; }
        public string PermittedUses { get; set; }
        public string EposDisplayPriority { get; set; }
        public DateTime? LastModified { get; set; }
        public bool AreShelfEdgeLabelsRequired { get; set; }
        public bool AreProductLabelsRequired { get; set; }
        public bool Transient { get; set; }
        public bool ContributeToSales { get; set; }
        public bool AutoGenerateStockItemBarcode { get; set; }
        public bool UsesISSNBarcodeScheme { get; set; }
        public bool AllowMobileSales { get; set; }
        public bool AllowStoreSales { get; set; }
        public bool AllowWebSales { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool PromptForDate { get; set; }
        public double? NetInvoicePrice { get; set; }
        public double? RetailPrice1ExVat { get; set; }
        public double? RetailPriceExVat { get; set; }
        public double? RetailPrice2ExVat { get; set; }
        public double? RetailPrice3ExVat { get; set; }
        public double? RetailPrice4ExVat { get; set; }
        public double? LastCostPrice { get; set; }
        public int? SeasonId { get; set; }
        public DateTime? BestBeforeDate { get; set; }
        public int? TaxCategoryId { get; set; }
        public double? Weight { get; set; }
        public int? CreatedByUserId { get; set; }
        public int? NumberShelfEdgeLabelsRequired { get; set; }
        public int? ShelfEdgeLabelTemplateCode { get; set; }
        public int? MeasurementUnitCode { get; set; }
        public bool RestrictedSaleLocations { get; set; }
        public int? ShelflifeInDays { get; set; }        

        //[Ignore]
        //public List<Self> SubDepartment { get; set; }

        //[Ignore]
        //public List<Self> Department { get; set; }

        //[Ignore]
        //public List<Self> Barcodes { get; set; }

        //[Ignore]
        //public List<Self> StockItems { get; set; }

        //[Ignore]
        //public List<Self> Packs { get; set; }

        //[Ignore]
        //public List<Self> Season { get; set; }

        //[Ignore]
        //public List<Self> TaxCategory { get; set; }

        //[Ignore]
        //public List<Self> CreatedByUser { get; set; }

        //[Ignore]
        //public List<Self> SaleLocations { get; set; }

        //[Ignore]
        //public List<Self> Self { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeA { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeB { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeC { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeD { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeE { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeTypeA { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeTypeB { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeTypeC { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeTypeD { get; set; }

        //[Ignore]
        //public List<Self> StockingAttributeTypeE { get; set; }
    }
}
