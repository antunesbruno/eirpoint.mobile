using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
   [Table("PaymentsCategories")]
    public class PaymentsCategoriesEntity : EntityBase
    {
        public string Description { get; set; }
        public int? DisplayOrder { get; set; }
        public int? ExternalCategoryCode { get; set; }
        public bool? IsCash { get; set; }
        public bool? IsCheque { get; set; }
        public bool? IsPaymentCard { get; set; }
        public bool? IsCredit { get; set; }
        public bool? AllowOverTender { get; set; }
        public bool? UpliftApplicable { get; set; }
        public bool? IsVoucher { get; set; }
        public bool? IsCreditNote { get; set; }
        public bool? IsLoyaltyPoints { get; set; }
        public bool? IsOtherVoucher { get; set; }
        public bool? ZreadApplicable { get; set; }
        public bool? IsElectronicCash { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public bool? Active { get; set; }
        public bool? ShowOnHandheld { get; set; }

        //[Ignore]
        //public List<Self> PaymentTypes { get; set; }

        //[Ignore]
        //public List<Self> Self { get; set; }
    }
}
