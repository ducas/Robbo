using Robbo.Bots;
using Robbo.Devices;

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
