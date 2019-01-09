using Eirpoint.Mobile.Core.Barcode;
using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Core.NativeInterfaces
{
    public interface IBarCode : IDisposable
    {
        event BarCodeReadDelegate OnBarCodeRead;
        void RaiseBarCodeReadEvent(BarcodeReadArgs barcodeReadArgs);
    }
}
