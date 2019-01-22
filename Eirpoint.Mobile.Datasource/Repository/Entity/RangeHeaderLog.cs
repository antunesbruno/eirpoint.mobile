using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("RangeHeaderLog")]
    public class RangeHeaderLog : EntityBase
    {
        public string DataItem { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public DateTime LogTime { get; set; }

        [Ignore]
        public override string Href { get; set; }
    }
}
