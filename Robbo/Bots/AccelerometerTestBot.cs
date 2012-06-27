using System;
using System.Threading;
using Microsoft.SPOT;
using Robbo.Devices;

namespace Robbo.Bots
{
    public class AccelerometerTestBot : IBot
    {
        private readonly Accelerometer accelerometer;

        public AccelerometerTestBot(Accelerometer accelerometer)
        {
            this.accelerometer = accelerometer;
        }

        public void Go()
        {
            while (true)
            {
                var start = DateTime.Now.Ticks;
                var data = accelerometer.GetData();
                var end = DateTime.Now.Ticks;
                var msTaken = (double)(end - start) / TimeSpan.TicksPerMillisecond;
                Debug.Print("X: " + data.X + ", Y: " + data.Y + ", Z: " + data.Z + ". " + msTaken + "ms");
                Thread.Sleep(1000);
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns

        public void Dispose() { }
    }
}
