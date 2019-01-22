using Eirpoint.Mobile.Shared.Barcode;
using System;

namespace Eirpoint.Mobile.Shared.NativeInterfaces
{
    public interface IBarCode : IDisposable
    {
        event BarCodeReadDelegate OnBarCodeRead;
        void RaiseBarCodeReadEvent(BarcodeReadArgs barcodeReadArgs);
    }
}
