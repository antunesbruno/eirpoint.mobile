using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
 
    [Table("StockLocations")]
    public class StockLocationsEntity : EntityBase
    {       
        public string Name { get; set; }
        public string LocationType { get; set; }
        public bool? AllowReceiving { get; set; }
        public bool? AllowSales { get; set; }
        public string Address { get; set; }
        public int? ExternalLocationCode { get; set; }
        public string GeneralReceiptHeader1 { get; set; }
        public string GeneralReceiptHeader2 { get; set; }
        public string GeneralReceiptHeader3 { get; set; }
        public int? QuotationReceiptTemplateId { get; set; }
        public int? OrderReceiptTemplateId { get; set; }
        public int? SaleReceiptTemplateId { get; set; }
        public int? SaleReturnReceiptTemplateId { get; set; }
        public int? PriceLevel { get; set; }
        public int? CountryId { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public DateTime? LastModified { get; set; }
        public int? TaxAreaId { get; set; }
        public bool? TaxOnReceipt { get; set; }
        public bool? Active { get; set; }
        public string Country { get; set; }

        [Ignore]
        public List<Self> OrderReceiptTemplate { get; set; }

        [Ignore]
        public List<Self> QuotationReceiptTemplate { get; set; }

        [Ignore]
        public List<Self> SaleReceiptTemplate { get; set; }

        [Ignore]
        public List<Self> SaleReturnReceiptTemplate { get; set; }

        [Ignore]
        public List<Self> TaxArea { get; set; }

        [Ignore]
        public List<Self> IntegrationSettings { get; set; }

        [Ignore]
        public List<Self> Self { get; set; }
    }
}
