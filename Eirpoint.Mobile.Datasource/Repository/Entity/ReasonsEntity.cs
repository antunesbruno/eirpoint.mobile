using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Reasons")]
    public class ReasonsEntity : EntityBase
    {
        public string Description { get; set; }
        public int? ReasonType { get; set; }
        public int? ExternalReasonCode { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public DateTime? LastModified { get; set; }

        [Ignore]
        public List<Self> Self { get; set; }
    }
}
