using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    public class Program
    {
        public static void Main()
        {
            var bot = new DiscoveryBot(
                new MotorDriverBridge(
                    PWM.Pin.PWM1, (Cpu.Pin)FEZ_Pin.Digital.Di9, (Cpu.Pin)FEZ_Pin.Digital.Di8,
                    PWM.Pin.PWM6, (Cpu.Pin)FEZ_Pin.Digital.Di5, (Cpu.Pin)FEZ_Pin.Digital.Di4,
                    (Cpu.Pin)FEZ_Pin.Digital.Di7
                    ),
                new UltrasonicDistanceSensor(AnalogIn.Pin.Ain5, (Cpu.Pin)FEZ_Pin.Digital.Di13)
            );
            //var bot = new DistanceSensorTestBot(new UltrasonicDistanceSensor(AnalogIn.Pin.Ain5, (Cpu.Pin)FEZ_Pin.Digital.Di13));
            //var bot = new MotorTestBot(
            //    new Motor(PWM.Pin.PWM1, (Cpu.Pin)FEZ_Pin.Digital.Di9, (Cpu.Pin)FEZ_Pin.Digital.Di8),
            //    new Motor(PWM.Pin.PWM6, (Cpu.Pin)FEZ_Pin.Digital.Di5, (Cpu.Pin)FEZ_Pin.Digital.Di4),
            //    new OutputPort((Cpu.Pin)FEZ_Pin.Digital.Di7, false)
            //    );
            bot.Go();
        }
    }
}
