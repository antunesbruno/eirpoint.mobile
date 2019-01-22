using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("RangeHeaderLog")]
    public class RangeHeaderLogEntity : EntityBase
    {
        public string DataItem { get; set; }
        public int StartRange { get; set; }
        public int EndRange { get; set; }
        public DateTime LogTime { get; set; }
        public string ErrorMessage { get; set; }

        [Ignore]
        public override string Href { get; set; }
    }
}
