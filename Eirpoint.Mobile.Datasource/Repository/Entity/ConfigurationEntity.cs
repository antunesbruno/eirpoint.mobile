using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Configuration")]
    public class ConfigurationEntity
    {
        [PrimaryKey, AutoIncrement]
        public int IdentityId { get; set; }

        /// <summary>
        /// Indicates if basic data was done
        /// </summary>
        public bool HasBasicData { get; set; }
    }
}
