using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    public class Program
    {
        public static void Main()
        {
            new RemoteBotSelector(new Transceiver("COM1"))
                .AwaitBot()
                .Go();
        }
    }
}
