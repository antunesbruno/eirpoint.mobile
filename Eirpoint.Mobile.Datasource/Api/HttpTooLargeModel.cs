using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Datasource.Api
{
    public class HttpTooLargeModel
    {
        public string ErrorType { get; set; }
        public int MaximumRange { get; set; }
        public int TotalCount { get; set; }
        public string Description { get; set; }
    }
}
