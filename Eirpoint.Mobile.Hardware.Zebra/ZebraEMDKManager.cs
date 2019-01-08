using Android.Util;
using Symbol.XamarinEMDK;
using Symbol.XamarinEMDK.Barcode;
using System;
using System.Collections.Generic;

namespace Eirpoint.Mobile.Hardware.Zebra
{
    public class ZebraEMDKManager : Java.Lang.Object, EMDKManager.IEMDKListener
    {
        #region Fields       

        EMDKManager emdkManager = null;
        BarcodeManager barcodeManager = null;
        Scanner scanner = null;

        #endregion

        #region Properties       

        public IntPtr Handle => throw new NotImplementedException();

        public string ScannerStatus { get; set; }

        #endregion

        #region IEMDKListener Methods

        public void OnClosed()
        {
            //debug
            Log.Debug("BarcodeOpenFailed", BarcodeMessages.EMDK_OBJECT_OPEN_FAILED);

            //callback message
            BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_OPEN_FAILED);         

            if (emdkManager != null)
            {
                emdkManager.Release();
                emdkManager = null;
            }
        }

        public void OnOpened(EMDKManager emdkManager)
        {
            //debug
            Log.Debug("BarcodeOpenSuccess", BarcodeMessages.EMDK_OBJECT_OPEN_SUCCESS);

            //callback message
            BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_OPEN_SUCCESS);

            this.emdkManager = emdkManager;

            InitScanner();
        }

        public void Dispose()
        {
            //Clean up the emdkManager
            if (emdkManager != null)
            {
                //EMDK: Release the EMDK manager object
                emdkManager.Release();
                emdkManager = null;
            }
        }

        #endregion

        #region Callbacks

        public Action<string> BarcodeStatusCallback { get; set; }
        public Action<string> BarcodeReadCallback { get; set; }

        #endregion

        #region Methods

        public void InitEMDK()
        {
            EMDKResults results = EMDKManager.GetEMDKManager(Android.App.Application.Context, this);

            if (results.StatusCode != EMDKResults.STATUS_CODE.Success)
            {
                //debug
                Log.Debug("BarcodeCreateFailed", BarcodeMessages.EMDK_OBJECT_CREATED_FAILED);
                
                //callback message
                BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_CREATED_FAILED);
               
            }
            else
            {
                //debug
                Log.Debug("BarcodeCreateSuccess", BarcodeMessages.EMDK_OBJECT_CREATED_SUCCESS);

                //callback message
                BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_CREATED_SUCCESS);               
            }
        }

        public void InitScanner()
        {
            if (emdkManager != null)
            {
                if (barcodeManager == null)
                {
                    try
                    {
                        //Get the feature object such as BarcodeManager object for accessing the feature.
                        barcodeManager = (BarcodeManager)emdkManager.GetInstance(EMDKManager.FEATURE_TYPE.Barcode);

                        scanner = barcodeManager.GetDevice(BarcodeManager.DeviceIdentifier.Default);

                        if (scanner != null)
                        {
                            //Attahch the Data Event handler to get the data callbacks.
                            scanner.Data += scanner_Data;

                            //Attach Scanner Status Event to get the status callbacks.
                            scanner.Status += scanner_Status;

                            scanner.Enable();

                            //EMDK: Configure the scanner settings
                            ScannerConfig config = scanner.GetConfig();
                            config.SkipOnUnsupported = ScannerConfig.SkipOnUnSupported.None;
                            config.ScanParams.DecodeLEDFeedback = true;
                            config.ReaderParams.ReaderSpecific.ImagerSpecific.PickList = ScannerConfig.PickList.Enabled;
                            config.DecoderParams.Code39.Enabled = true;
                            config.DecoderParams.Code128.Enabled = false;
                            scanner.SetConfig(config);

                        }
                        else
                        {
                            //debug
                            Log.Debug("BarcodeEnabled", BarcodeMessages.EMDK_OBJECT_ENABLED_FAILED);

                            //callback message
                            BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_ENABLED_FAILED);                          
                        }
                    }
                    catch (ScannerException e)
                    {
                        //debug
                        Log.Debug("BarcodeScannerException", "Error: " + e.Message);

                        //callback message
                        BarcodeStatusCallback?.Invoke("Error: " + e.Message);
                     
                    }
                    catch (Exception ex)
                    {
                        //debug
                        Log.Debug("BarcodeException", "Error: " + ex.Message);

                        //callback message
                        BarcodeStatusCallback?.Invoke("Error: " + ex.Message);
                    }
                }
            }
        }

        public void StopScanner()
        {
            if (emdkManager != null)
            {

                if (scanner != null)
                {
                    try
                    {

                        scanner.Data -= scanner_Data;
                        scanner.Status -= scanner_Status;

                        scanner.Disable();
                    }
                    catch (ScannerException e)
                    {
                        //debug
                        Log.Debug("DeinitScan", "Exception: " + e.Message);

                        //callback message
                        BarcodeStatusCallback?.Invoke("Exception: " + e.Message);                      
                    }
                }

                if (barcodeManager != null)
                {
                    emdkManager.Release(EMDKManager.FEATURE_TYPE.Barcode);
                }

                barcodeManager = null;
                scanner = null;
            }
        }

        #endregion

        #region Events
        
        /// <summary>
        /// Event to read data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanner_Data(object sender, Scanner.DataEventArgs e)
        {
            ScanDataCollection scanDataCollection = e.P0;

            if ((scanDataCollection != null) && (scanDataCollection.Result == ScannerResults.Success))
            {
                IList<ScanDataCollection.ScanData> scanData = scanDataCollection.GetScanData();

                foreach (ScanDataCollection.ScanData data in scanData)
                {
                    ScannerStatus = data.LabelType + " : " + data.Data;
                }
            }
        }

        /// <summary>
        /// Event to show status about read
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanner_Status(object sender, Scanner.StatusEventArgs e)
        {
            
            //EMDK: The status will be returned on multiple cases. Check the state and take the action.
            StatusData.ScannerStates state = e.P0.State;

            if (state == StatusData.ScannerStates.Idle)
            {
                //debug
                Log.Debug("BarcodeReadIdle", BarcodeMessages.EMDK_OBJECT_SCANNER_IDLE);

                //callback message
                BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_SCANNER_IDLE);

                try
                {
                    if (scanner.IsEnabled && !scanner.IsReadPending)
                    {
                        scanner.Read();
                    }
                }
                catch (ScannerException e1)
                {
                    //debug
                    Log.Debug("ScannerStatus", "Exception: " + e1.Message);

                    //callback message
                    BarcodeStatusCallback?.Invoke("Exception: " + e1.Message);
                }
            }
            if (state == StatusData.ScannerStates.Waiting)
            {
                //debug
                Log.Debug("BarcodeWaiting", BarcodeMessages.EMDK_OBJECT_SCANNER_WAITING);

                //callback message
                BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_SCANNER_WAITING);                
            }
            if (state == StatusData.ScannerStates.Scanning)
            {
                //debug
                Log.Debug("BarcodeScanning", BarcodeMessages.EMDK_OBJECT_SCANNER_SCANNING);

                //callback message
                BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_SCANNER_SCANNING);
            }
            if (state == StatusData.ScannerStates.Disabled)
            {
                //debug
                Log.Debug("BarcodeDisabled", BarcodeMessages.EMDK_OBJECT_SCANNER_DISABLED);

                //callback message
                BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_SCANNER_DISABLED);              
            }
            if (state == StatusData.ScannerStates.Error)
            {
                //debug
                Log.Debug("BarcodeError", BarcodeMessages.EMDK_OBJECT_SCANNER_ERROR);

                //callback message
                BarcodeStatusCallback?.Invoke(BarcodeMessages.EMDK_OBJECT_SCANNER_ERROR);  
            }          
        }

        #endregion
    }
}