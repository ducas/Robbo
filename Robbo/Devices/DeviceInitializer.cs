using GHIElectronics.NETMF.FEZ;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo.Devices
{
    public static class DeviceInitializer
    {
        public static Piezo Piezo()
        {
            return new Piezo(PWM.Pin.PWM2);
        }

        public static UltrasonicDistanceSensor DistanceSensor()
        {
            return new UltrasonicDistanceSensor(AnalogIn.Pin.Ain5, (Cpu.Pin)FEZ_Pin.Digital.Di2);
        }

        public static MotorDriver MotorDriver()
        {
            return new MotorDriver(
                PWM.Pin.PWM1, (Cpu.Pin)FEZ_Pin.Digital.Di9, (Cpu.Pin)FEZ_Pin.Digital.Di8,
                PWM.Pin.PWM6, (Cpu.Pin)FEZ_Pin.Digital.Di5, (Cpu.Pin)FEZ_Pin.Digital.Di4,
                (Cpu.Pin)FEZ_Pin.Digital.Di7
                );
        }
    }
}
