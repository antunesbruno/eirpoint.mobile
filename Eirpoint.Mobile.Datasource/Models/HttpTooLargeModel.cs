﻿namespace Eirpoint.Mobile.Datasource.Models
{
    public class HttpTooLargeModel
    {
        public string ErrorType { get; set; }
        public int MaximumRange { get; set; }
        public int TotalCount { get; set; }
        public string Description { get; set; }
    }
}
