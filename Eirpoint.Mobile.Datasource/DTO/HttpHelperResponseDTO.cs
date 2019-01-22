using Eirpoint.Mobile.Datasource.Repository.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.DTO
{
    public class HttpHelperResponseDTO
    {
        public List<EntityBase> resultList { get; set; }
        public string MessageError { get; set; }
    }
}
