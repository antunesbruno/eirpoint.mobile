using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("ProductBarcodes")]
    public class ProductBarCodesEntity : EntityBase
    {
        public int? ProductId { get; set; }
        public string Barcode { get; set; }
        public bool? IsDefault { get; set; }
        public int? StockItemId { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }

        //[Ignore]
        //public List<Self> Product { get; set; }

        //[Ignore]
        //public List<Self> StockItem { get; set; }

        //[Ignore]
        //public List<Self> Self { get; set; }
    }
}
