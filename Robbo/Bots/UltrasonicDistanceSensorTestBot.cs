using System.Threading;
using Microsoft.SPOT;
using Robbo.Devices;

namespace Robbo.Bots
{
    /// <summary>
    /// A test bot that outputs the distance the sensor is detecting.
    /// </summary>
    public class UltrasonicDistanceSensorTestBot : IBot
    {
        private readonly UltrasonicDistanceSensor sensor;
        private readonly Piezo piezo;

        public UltrasonicDistanceSensorTestBot(UltrasonicDistanceSensor sensor, Piezo piezo)
        {
            this.sensor = sensor;
            this.piezo = piezo;
        }

        public void Go()
        {
            while (true)
            {
                var distance = sensor.Distance;
                Debug.Print(distance + "cm");
                piezo.Play((int)((sensor.MaximumRange - distance) * 10), 100);
                Thread.Sleep(1000);
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns
        public void Dispose()
        {
            sensor.Dispose();
            piezo.Dispose();
        }
    }
}
