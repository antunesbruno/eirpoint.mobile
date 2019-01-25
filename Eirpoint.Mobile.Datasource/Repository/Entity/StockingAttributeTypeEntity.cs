using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("StockingAttributeTypes")]
    public class StockingAttributeTypeEntity : EntityBase
    {
        public string Description { get; set; }
        public string ExternalStockingAttributeTypeCode { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public bool? Active { get; set; }

    //    [Ignore]
    //    public List<Self> StockingAttributes { get; set; }

    //    [Ignore]
    //    public List<Self> Self { get; set; }
    }
}

