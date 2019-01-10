using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Barcodes")]
    public class BarcodesEntity : EntityBase
    {
        public int? ProductId { get; set; }
        public string Barcode { get; set; }
        public bool IsDefault { get; set; }
        public int StockItemId { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public DateTime? LastModified { get; set; }

        //[Ignore]
        //public ProductsEntity[] Product { get; set; }

        //[Ignore]
        //public StockItemsEntity[] StockItem { get; set; }

        //[Ignore]
        //public SelfEntity[] Self { get; set; }

    }
}
