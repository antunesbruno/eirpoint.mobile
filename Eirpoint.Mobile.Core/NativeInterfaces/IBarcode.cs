using Eirpoint.Mobile.Core.Barcode;
using System;

namespace Eirpoint.Mobile.Core.NativeInterfaces
{
    public interface IBarCode : IDisposable
    {
        event BarCodeReadDelegate OnBarCodeRead;
        void RaiseBarCodeReadEvent(BarcodeReadArgs barcodeReadArgs);
    }
}
