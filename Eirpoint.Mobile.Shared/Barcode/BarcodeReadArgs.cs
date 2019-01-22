namespace Eirpoint.Mobile.Shared.Barcode
{
    public class BarcodeReadArgs
    {
        public string BarCodeData { get; set; }
        public string TimeStamp { get; set; }
        public string Message { get; set; }

        public BarcodeReadArgs()
        {
        }
    }
}
