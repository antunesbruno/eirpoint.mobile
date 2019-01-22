﻿using SQLite;
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
        public DateTime? LastModified { get; set; }

        [Ignore]
        public List<SubDepartmentEntity> SubDepartments { get; set; }
    }
}
