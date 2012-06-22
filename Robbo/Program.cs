using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    public class Program
    {
        public static void Main()
        {
            var driver = new MotorDriver(
                PWM.Pin.PWM1, (Cpu.Pin)FEZ_Pin.Digital.Di9, (Cpu.Pin)FEZ_Pin.Digital.Di8,
                PWM.Pin.PWM6, (Cpu.Pin)FEZ_Pin.Digital.Di5, (Cpu.Pin)FEZ_Pin.Digital.Di4,
                (Cpu.Pin)FEZ_Pin.Digital.Di7
                );
            var bot =
                new DiscoveryBot(
                    driver,
                    new UltrasonicDistanceSensor(AnalogIn.Pin.Ain5, (Cpu.Pin)FEZ_Pin.Digital.Di13)
                    );
                //new MotorDriverTestBot(driver);
                //new UltrasonicDistanceSensorTestBot(new UltrasonicDistanceSensor(AnalogIn.Pin.Ain5, (Cpu.Pin)FEZ_Pin.Digital.Di13));
                //new MotorTestBot(
                //    new Motor(PWM.Pin.PWM1, (Cpu.Pin) FEZ_Pin.Digital.Di9, (Cpu.Pin) FEZ_Pin.Digital.Di8),
                //    new Motor(PWM.Pin.PWM6, (Cpu.Pin) FEZ_Pin.Digital.Di5, (Cpu.Pin) FEZ_Pin.Digital.Di4),
                //    new OutputPort((Cpu.Pin) FEZ_Pin.Digital.Di7, false)
                //    );
            bot.Go();
        }
    }
}
