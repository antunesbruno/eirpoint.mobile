using Eirpoint.Mobile.Shared.Enumerators;
using System.Threading.Tasks;

namespace Eirpoint.Mobile.Shared.NativeInterfaces
{
    public interface IConnectivity
    {
        Task<bool> CanPing(string ip);
        Task<bool> CanConnect(string ip, int port);
        bool IsConnected();
        bool IsConnectedWifi();
        bool IsConnectedMobile();
        ConnectionType ConnectionType { get; }
    }
}
