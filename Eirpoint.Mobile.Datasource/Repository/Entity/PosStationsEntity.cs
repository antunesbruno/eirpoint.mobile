using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("PosStations")]
    public class PosStationsEntity : EntityBase
    {
        public int? StationCode { get; set; }
        public string LocationDescription { get; set; }
        public string MachineName { get; set; }
        public int? StockLocationId { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public bool? IsOnline { get; set; }
        public int? Hwid { get; set; }
        public int? StationType { get; set; }
        public bool? Active { get; set; }

        //[Ignore]
        //public List<Self> StockLocation { get; set; }

        //[Ignore]
        //public List<Self> Self { get; set; }
    }   

}
