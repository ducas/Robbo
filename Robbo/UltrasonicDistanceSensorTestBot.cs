using System.Threading;
using Microsoft.SPOT;

namespace Robbo
{
    public class UltrasonicDistanceSensorTestBot
    {
        private readonly UltrasonicDistanceSensor sensor;

        public UltrasonicDistanceSensorTestBot(UltrasonicDistanceSensor sensor)
        {
            this.sensor = sensor;
        }

        public void Go()
        {
            while (true)
            {
                Debug.Print(sensor.Distance + "cm");
                Thread.Sleep(1000);
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns
    }
}
