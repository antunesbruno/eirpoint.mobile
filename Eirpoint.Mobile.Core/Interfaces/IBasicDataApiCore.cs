using System;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Core.Interfaces
{
    public interface IBasicDataApiCore
    {
        Task SynchronizeDataItems(Action<int> onProgressCallback);
    }
}
