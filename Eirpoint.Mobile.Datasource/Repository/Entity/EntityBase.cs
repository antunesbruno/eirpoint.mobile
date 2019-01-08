using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    public class EntityBase
    {
        [PrimaryKey]
        public int Id { get; set; }

        public string Href { get; set; }
    }
}
