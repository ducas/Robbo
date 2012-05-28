using System;
using GHIElectronics.NETMF.Hardware;

namespace Robbo
{
    public class UltrasonicDistanceSensor : IDisposable
    {
        private readonly AnalogIn adc;
        private readonly double mVPerCm;

        public UltrasonicDistanceSensor(AnalogIn.Pin pin, double supplyVoltage = 3.3)
        {
            adc = new AnalogIn(pin);
            // mV/cm = 2.54 * mV/inch = 2.54 * (supply voltage / 512)
            mVPerCm = 2.54 * supplyVoltage / 512;
        }

        public double Distance
        {
            get { return adc.Read() / mVPerCm; }
        }

        public void Dispose()
        {
            adc.Dispose();
        }
    }
}
