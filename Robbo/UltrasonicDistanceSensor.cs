using System;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    public class UltrasonicDistanceSensor : IDisposable
    {
        private const int maximumRange = 480;

        private readonly AnalogIn adc;
        private readonly double scale;
        private readonly OutputPort control;

        public UltrasonicDistanceSensor(AnalogIn.Pin analogPin, Cpu.Pin controlPin, int supplyVoltage = 5000)
        {
            control = new OutputPort(controlPin, true);
            adc = new AnalogIn(analogPin);
            adc.SetLinearScale(0, supplyVoltage);
            // scale = (Vcc / 512) / 2.54 (cm per in) => mv/cm
            scale = supplyVoltage / 512D / 2.54;
        }

        public double Distance
        {
            get { return adc.Read() / scale; }
        }

        public int MaximumRange
        {
            get { return maximumRange; }
        }

        public void Dispose()
        {
            adc.Dispose();
            control.Dispose();
        }
    }
}
