using System;
using System.Collections.Generic;
using System.Text;

namespace Eirpoint.Mobile.Shared.Interfaces
{
    public interface IConfiguration
    {
        int Id { get; }
        bool HasBasicData { get; set; }
    }
}
