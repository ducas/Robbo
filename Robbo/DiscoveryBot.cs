using System;
using System.Threading;

namespace Robbo
{
    /// <summary>
    /// A simple robot program that rolls around attempting to avoid obstacles.
    /// </summary>
    public class DiscoveryBot : IBot
    {
        private const int interruptDistance = 30;
        private const int fullSpeed = 100;
        private const int forwardDuration = 500;
        private const int stopDuration = 500;
        private const int backUpDuration = 1000;
        private const int turnSpeed = 50;
        private const int turnDuration = 500;

        private readonly MotorDriver driver;
        private readonly UltrasonicDistanceSensor front;
        private readonly Piezo piezo;

        public DiscoveryBot(MotorDriver driver, UltrasonicDistanceSensor front, Piezo piezo)
        {
            this.driver = driver;
            this.front = front;
            this.piezo = piezo;
        }

        public void Go()
        {
            driver.Forward(fullSpeed);
            while (true)
            {
                var frontDistance = front.Distance;

                piezo.Play((int)((front.MaximumRange - frontDistance) * 10), 100);

                if (frontDistance < interruptDistance)
                {
                    driver.Stop();
                    Thread.Sleep(stopDuration);

                    //driver.Reverse(fullSpeed);
                    //Thread.Sleep(backUpDuration);

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
