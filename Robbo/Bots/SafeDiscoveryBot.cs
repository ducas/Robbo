using System;
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
        private const int flipDuration = 500;

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
            while (true)
            {
                if (TryAvoidFlip())
                    Thread.Sleep(flipDuration);

                while (TryAvoidCollision())
                    Thread.Sleep(turnDuration);

                driver.Forward(fullSpeed);
                Thread.Sleep(forwardDuration);
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns

        private bool TryAvoidCollision()
        {
            if (front.Distance < interruptDistance)
            {
                driver.TurnLeft(turnSpeed);
                return true;
            }
            return false;
        }

        private bool TryAvoidFlip()
        {
            var acceleration = accelerometer.GetData();
            if (acceleration.Z <= 0)
            {
                driver.Stop();
                return true;
            }
            if (acceleration.Y < -0.5)
            {
                driver.TurnRight(turnSpeed);
                return true;
            }
            if (0.5 < acceleration.Y)
            {
                driver.TurnLeft(turnSpeed);
                return true;
            }
            if (acceleration.X < -0.5)
            {
                driver.Reverse(fullSpeed);
                return true;
            }
            return false;
        }

        public void Dispose()
        {
            driver.Dispose();
            front.Dispose();
        }
    }
}
