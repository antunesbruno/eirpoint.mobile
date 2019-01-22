﻿using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Repository.Entity
{
    public class EntityBase
    {
        [PrimaryKey]
        public virtual int Id { get; set; }

        public virtual string Href { get; set; }
    }
}
