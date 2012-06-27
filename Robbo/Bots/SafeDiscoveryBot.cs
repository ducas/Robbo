using System.Threading;
using Microsoft.SPOT;
using Robbo.Devices;

namespace Robbo.Bots
{
    /// <summary>
    /// A simple robot program that rolls around attempting to avoid obstacles.
    /// </summary>
    public class SafeDiscoveryBot : IBot
    {
        private const int interruptDistance = 30;
        private const int fullSpeed = 100;
        private const int forwardDuration = 100;
        private const int turnSpeed = 100;
        private const int turnDuration = 100;
        private const int breakDuration = 1000;

        private readonly MotorDriver driver;
        private readonly UltrasonicDistanceSensor front;
        private readonly Accelerometer accelerometer;

        public SafeDiscoveryBot(MotorDriver driver, UltrasonicDistanceSensor front, Accelerometer accelerometer)
        {
            this.driver = driver;
            this.front = front;
            this.accelerometer = accelerometer;
        }

        public void Go()
        {
            driver.Forward(fullSpeed);
            while (true)
            {
                var acceleration = accelerometer.GetData();
                if (acceleration.Z < 0 || acceleration.Y < -0.5 || 0.5 < acceleration.Y)
                {
                    Debug.Print(acceleration.Y + " " + acceleration.Z);
                    driver.Stop();
                    Thread.Sleep(breakDuration);
                    continue;
                }

                while (front.Distance < interruptDistance)
                {
                    driver.TurnLeft(turnSpeed);
                    Thread.Sleep(turnDuration);
                }

                driver.Forward(fullSpeed);
                Thread.Sleep(forwardDuration);
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns

        public void Dispose()
        {
            driver.Dispose();
            front.Dispose();
        }
    }
}
