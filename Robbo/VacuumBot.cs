using System;
using System.Threading;
using Microsoft.SPOT;

namespace Robbo
{
    public class VacuumBot : IBot
    {
        private const int interruptDistance = 30;
        private const int fullSpeed = 100;
        private const int forwardDuration = 500;
        private const int stopDuration = 500;
        private const int turnSpeed = 50;
        private const int maxTurnDuration = 3000;

        private readonly MotorDriver driver;
        private readonly UltrasonicDistanceSensor front;
        private readonly Random random = new Random();

        public VacuumBot(MotorDriver driver, UltrasonicDistanceSensor front)
        {
            this.driver = driver;
            this.front = front;
        }

        public void Go()
        {
            var discoverCount = 0;
            var left = 0;
            var right = 100;

            driver.Forward(fullSpeed);
            while (true)
            {
                if (front.Distance < interruptDistance)
                {
                    driver.Stop();
                    Thread.Sleep(stopDuration);

                    while (front.Distance < interruptDistance)
                    {
                        driver.TurnLeft(turnSpeed);
                        Thread.Sleep((int)(random.NextDouble() * maxTurnDuration));
                    }

                    driver.Forward(fullSpeed);
                    discoverCount = random.Next(10);
                    left = 0;
                    continue;
                }

                if (discoverCount > 0)
                {
                    Thread.Sleep(forwardDuration);
                    discoverCount--;
                    continue;
                }

                driver.PwmA = (left < 100 ? ++left : left);
                driver.PwmB = right;

                Thread.Sleep(500);
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
