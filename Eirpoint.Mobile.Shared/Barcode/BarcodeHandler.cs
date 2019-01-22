using Eirpoint.Mobile.Shared.NativeInterfaces;
using System;

namespace Eirpoint.Mobile.Shared.Barcode
{
    public class BarcodeHandler : IBarCode
    {
        public event BarCodeReadDelegate OnBarCodeRead;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void RaiseBarCodeReadEvent(BarcodeReadArgs barCodeReadArgs)
        {
            if (OnBarCodeRead != null)
                OnBarCodeRead(barCodeReadArgs);
        }
    }
}
