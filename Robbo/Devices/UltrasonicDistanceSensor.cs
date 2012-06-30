using System;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo.Devices
{
    /// <summary>
    /// Represents an Ultrasonic Distance Sensor (Maxbotix LZ-EVx)
    /// </summary>
    public class UltrasonicDistanceSensor : IDisposable
    {
        private const int maximumRange = 480;

        private readonly AnalogIn adc;
        private readonly double scale;
        private readonly OutputPort control;

        /// <summary>
        /// Creates an instance of the distance sensor.
        /// </summary>
        /// <param name="analogPin">The pin connected to the analog output on the sensor (AN)</param>
        /// <param name="controlPin">The pin connected to the receive input on the sensor (RX)</param>
        /// <param name="supplyVoltage">The voltage supplied to the sensor in millivolts (usually 3300 or 5000)</param>
        public UltrasonicDistanceSensor(AnalogIn.Pin analogPin, Cpu.Pin controlPin, int supplyVoltage = 5000)
        {
            control = new OutputPort(controlPin, true);
            adc = new AnalogIn(analogPin);
            adc.SetLinearScale(0, supplyVoltage);
            // scale = (Vcc / 512) / 2.54 (cm per in) => mv/cm
            scale = supplyVoltage / 512D / 2.54;
        }

        /// <summary>
        /// The distance in centimeters.
        /// </summary>
        public double Distance
        {
            get { return adc.Read() / scale; }
        }

        /// <summary>
        /// The maximum range of the sensor in centimeters.
        /// </summary>
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
