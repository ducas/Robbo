using System;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT;
using Math = System.Math;

namespace Robbo.Devices
{
    public class InfraredDistanceSensor : IDisposable
    {
        private const double y0 = 10;
        private const double x0 = 315;
        private const double y1 = 80;
        private const double x1 = 30;
        private const double c = (y1 - y0) / (1 / x1 - 1 / x0);

        private readonly AnalogIn adc;

        public InfraredDistanceSensor(AnalogIn.Pin pin)
        {
            adc = new AnalogIn(pin);
        }

        public double Distance
        {
            get
            {
                var value = adc.Read();
                var original = c / (value + .001D) - (c / x0) + y0;
                var alternate1 = 12343.85 * Math.Pow(value, -1.15);
                Debug.Print("original: " + original + ", alternate1: " + alternate1);
                return alternate1;
            }
        }

        public void Dispose()
        {
            adc.Dispose();
        }
    }
}
