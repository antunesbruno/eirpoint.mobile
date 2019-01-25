

using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
   [Table("ReceiptTemplates")]
    public class ReceiptTemplatesEntity : EntityBase
    { 
        public string Description { get; set; }
        public bool? ShowVat { get; set; }
        public bool? ShowItemTotal { get; set; }
        public bool? ShowDatetime { get; set; }
        public bool? ShowSalesMessage { get; set; }
        public bool? ShowLoyaltyPoints { get; set; }
        public bool? ShowLoyaltyPromotions { get; set; }
        public bool? ShowTotalSavings { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public bool? Active { get; set; }

        //[Ignore]
        //public List<Self> Headers { get; set; }

        //[Ignore]
        //public List<Self> Footers { get; set; }

        //[Ignore]
        //public List<Self> Self { get; set; }
    }
}
