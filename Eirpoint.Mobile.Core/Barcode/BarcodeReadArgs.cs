﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Core.Barcode
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
