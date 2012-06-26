using System;
using System.Threading;
using Robbo.Devices;

namespace Robbo.Bots
{
    /// <summary>
    /// A simple vacuum robot that spins around trying to avoid obstacles.
    /// </summary>
    public class VacuumBot : IBot
    {
        private const int interruptDistance = 30;
        private const int fullSpeed = 100;

        private const int maxForwardMultiplier = 100;
        private const int forwardDuration = 100;

        private const int turnSpeed = fullSpeed;
        private const int minTurnDuration = 500;
        private const int maxTurnMultiplier = 3;

        private const int leftSpinStartSpeed = 5;
        private const int leftSpinSpeedIncrement = 1;
        private const int rightSpinSpeed = 100;
        private const int spinSleepDuration = 100;
        private const int maxSpinCount = 10;

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
            var spinCount = 0;
            var left = leftSpinStartSpeed;

            driver.Forward(fullSpeed);
            while (true)
            {
                if (front.Distance < interruptDistance)
                {
                    driver.TurnLeft(turnSpeed);
                    Thread.Sleep(random.Next(maxTurnMultiplier) * minTurnDuration);

                    driver.Forward(fullSpeed);
                    discoverCount = random.Next(maxForwardMultiplier);
                    left = leftSpinStartSpeed;
                    continue;
                }

                if (discoverCount > 0)
                {
                    Thread.Sleep(forwardDuration);
                    discoverCount--;
                    if (discoverCount == 0) spinCount = 0;
                    continue;
                }

                if (left < fullSpeed && spinCount < maxSpinCount)
                {
                    driver.PwmA = left;
                    driver.PwmB = rightSpinSpeed;
                    spinCount++;
                }
                else if (left < fullSpeed && spinCount == maxSpinCount)
                {
                    left += leftSpinSpeedIncrement;
                    spinCount = 0;
                }

                Thread.Sleep(spinSleepDuration);
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
