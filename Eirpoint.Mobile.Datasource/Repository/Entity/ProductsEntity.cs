using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    [Table("Product")]
    public class ProductsEntity
    {
        [PrimaryKey]
        public int Id { get; set; }

        public bool Active { get; set; }

        public DateTime LiveDate { get; set; }
    }
}
