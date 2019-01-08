using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Eirpoint.Mobile.Hardware
{
    public interface IBarcodeHandler
    {
        /// <summary>
        /// Report status of barcode operations, connections and read
        /// </summary>
        Action<string> BarcodeStatusCallback { get; set; }

        /// <summary>
        /// Callback that get the barcode read
        /// </summary>
        Action<string> BarcodeReadCallback { get; set; }
        
        /// <summary>
        /// Init EMDK/SDK of barcodes
        /// Not apply of all barcodes, in some cases this method replace the initscanner method
        /// </summary>
        void InitEMDK();

        /// <summary>
        /// Init the scanner instance
        /// </summary>
        void InitScanner();

        /// <summary>
        /// Stop the scanner instance
        /// </summary>
        void StopScanner();
    }
}