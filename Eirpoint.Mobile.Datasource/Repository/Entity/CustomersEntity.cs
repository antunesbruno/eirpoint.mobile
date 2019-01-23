using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Customers")]
    public class CustomersEntity : EntityBase
    {
        public bool? Active { get; set; }
        public string Name { get; set; }
        public DateTime? CustomerSince { get; set; }
        public string AreaCode { get; set; }
        public string CustomerReferenceCode { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Telephone1 { get; set; }
        public string Telephone1Extension { get; set; }
        public string Telephone2 { get; set; }
        public string Telephone2Extension { get; set; }
        public string Fax1 { get; set; }
        public string Fax2 { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public double? CreditLimit { get; set; }
        public int? SalesMessage { get; set; }
        public int? DiscountId { get; set; }
        public bool? PrintSignLine { get; set; }
        public bool? PrintBalance { get; set; }
        public int? PriceLevel { get; set; }
        public string ExternalCustomerCode { get; set; }
        public double? AccountBalance { get; set; }
        public bool? ExcludeVat { get; set; }
        public bool? IncludeInStatementRun { get; set; }
        public double? AverageSpend { get; set; }
        public DateTime? LastPurchaseDatetime { get; set; }
        public DateTime? LastMarketingDatetime { get; set; }
        public bool? AcceptsMarketing { get; set; }
        public int? LoyaltyPoints { get; set; }
        public bool? PONumberMandatory { get; set; }
        public int? HomeLocationId { get; set; }
        public int? LoyaltySchemeId { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public string Note { get; set; }
        public bool? CreditIssues { get; set; }
        public bool? IsMale { get; set; }
        public DateTime? LastModified { get; set; }
        public double? Discount { get; set; }
        public string LoyaltyScheme { get; set; }
        public string HomeLocation { get; set; }

        //[Ignore]
        //public List<IdentificationCodesEntity> IdentificationCodes { get; set; }
        
        //[Ignore]
        //public Self Self { get; set; }        
    }
}
