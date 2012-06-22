using System;
using System.Threading;

namespace Robbo
{
    /// <summary>
    /// A simple robot program that rolls around attempting to avoid obstacles.
    /// </summary>
    public class DiscoveryBot : IDisposable
    {
        private const int interruptDistance = 30;
        private const int fullSpeed = 100;
        private const int forwardDuration = 500;
        private const int stopDuration = 500;
        private const int backUpDuration = 1000;
        private const int turnSpeed = 50;
        private const int turnDuration = 500;

        private readonly MotorDriverBridge driver;
        private readonly UltrasonicDistanceSensor front;

        public DiscoveryBot(MotorDriverBridge driver, UltrasonicDistanceSensor front)
        {
            this.driver = driver;
            this.front = front;
        }

        public void Go()
        {
            driver.Forward(fullSpeed);
            while (true)
            {
                if (front.Distance < interruptDistance)
                {
                    driver.Stop();
                    Thread.Sleep(stopDuration);

                    driver.Reverse(fullSpeed);
                    Thread.Sleep(backUpDuration);

                    while (front.Distance < interruptDistance)
                    {
                        driver.TurnLeft(turnSpeed);
                        Thread.Sleep(turnDuration);
                    }

                    driver.Forward(fullSpeed);
                }

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
