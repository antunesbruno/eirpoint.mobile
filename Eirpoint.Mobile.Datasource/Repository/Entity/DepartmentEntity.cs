using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Department")]
    public class DepartmentEntity : EntityBase
    {
        public string Description { get; set; }
        public bool? Active { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }

        //[Ignore]
        //public List<SubDepartmentsEntity> SubDepartments { get; set; }

        //[Ignore]
        //public List<Self> Self { get; set; }
    }
}
