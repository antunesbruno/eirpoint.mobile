using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Shared.Interfaces
{
    public interface IMonitorService
    {
        void CreateUpdateDataMonitor();
        void StopService();
    }
}
