namespace Eirpoint.Mobile.Datasource.DTO
{
    public class HttpTooLargeDTO
    {
        public string ErrorType { get; set; }
        public int MaximumRange { get; set; }
        public int TotalCount { get; set; }
        public string Description { get; set; }
    }
}
