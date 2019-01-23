using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Groups")]
    public class GroupsEntity : EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string ExternalGroupCode { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public bool? CanDeliverOrders { get; set; }
        public DateTime? LastModified { get; set; }

        [Ignore]
        public List<Self> GroupPermissions { get; set; }

        [Ignore]
        public List<Self> GroupUsers { get; set; }

        [Ignore]
        public List<Self> Self { get; set; }
    }
}
