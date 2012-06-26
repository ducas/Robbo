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
        private const int forwardDuration = 100;
        private const int turnSpeed = 100;
        private const int turnDuration = 100;

        private readonly MotorDriver driver;
        private readonly UltrasonicDistanceSensor front;

        public DiscoveryBot(MotorDriver driver, UltrasonicDistanceSensor front)
        {
            this.driver = driver;
            this.front = front;
        }

        public void Go()
        {
            driver.Forward(fullSpeed);
            while (true)
            {
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
