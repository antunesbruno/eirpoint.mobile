using Eirpoint.Mobile.Datasource.Repository.Urls;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Users")]
    public class UsersEntity : EntityBase
    {
        public string Name { get; set; }
        public string LoginName { get; set; }
        public string Password { get; set; }
        public bool? IsActive { get; set; }
        public bool? LogUser { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime? LastUnsuccessfulLogin { get; set; }
        public bool? AllowRestrictedSales { get; set; }
        public int? ExternalUserCode { get; set; }
        public string SyncUpdateTimestamp { get; set; }
        public string SyncInsertTimestamp { get; set; }
        public DateTime? LastModified { get; set; }

        [Ignore]
        public List<Self> UserGroups { get; set; }

        [Ignore]
        public List<Self> Self { get; set; }
    }
}
