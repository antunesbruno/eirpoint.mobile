using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
  
    [Table("Discounts")]
    public class DiscountsEntity : EntityBase
    {
        public string Description { get; set; }
        public double? PercentageOff { get; set; }
        public double? PriceOff { get; set; }
        public int? ExternalDiscountCode { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public bool? Active { get; set; }

        //[Ignore]
        //public List<Self> Self { get; set; }
    }
}
