using Acr.UserDialogs;
using Eirpoint.Mobile.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Shared
{
    public class MonitorService : IMonitorService
    {
        private UpdateDataMonitor _updateMonitor;

        /// <summary>
        /// Create monitoring service
        /// </summary>
        public virtual void CreateUpdateDataMonitor()
        {
            //log output
            Debug.WriteLine("<<< Creating UpdateDataMonitor");

            //create monitor
            _updateMonitor = new UpdateDataMonitor();

            //set callback
            _updateMonitor.UpdateCallback(UpdateCallback);

            //create task monitor
            Task.Factory.StartNew(() => _updateMonitor.UpdateMonitorAction(), TaskCreationOptions.LongRunning);

            //set flag to true
            _updateMonitor.IsRunning = true;
        }

        /// <summary>
        /// Stop monitoring service
        /// </summary>
        public virtual void StopService()
        {
            //log
            Debug.WriteLine("<<< Stopping UpdateDataMonitor");

            //set flag
            _updateMonitor.IsRunning = false;
        }

        private void UpdateCallback(string entityName)
        {
            UserDialogs.Instance.Toast(entityName, new TimeSpan(2));
        }
    }
}
