using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using Robbo.Devices;

namespace Robbo.Bots
{
    /// <summary>
    /// A test bot that runs the motors in different directions.
    /// </summary>
    public class MotorTestBot : IBot
    {
        private readonly Motor left;
        private readonly Motor right;
        private readonly OutputPort control;

        public MotorTestBot(Motor left, Motor right, OutputPort control)
        {
            this.left = left;
            this.right = right;
            this.control = control;
        }

        public void Go()
        {
            while (true)
            {
                Debug.Print("Go.");
                control.Write(true);

                Debug.Print("Full forward.");
                left.Forward(100);
                right.Forward(100);

                Thread.Sleep(1000);

                Debug.Print("Half Right.");
                left.Forward(50);
                right.Reverse(50);

                Thread.Sleep(1000);

                Debug.Print("Half Left.");
                left.Reverse(50);
                right.Forward(50);

                Thread.Sleep(1000);

                Debug.Print("Full reverse.");
                left.Reverse(100);
                right.Reverse(100);

                Thread.Sleep(1000);

                Debug.Print("Stop.");
                control.Write(false);

                Thread.Sleep(1000);
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns
        public void Dispose()
        {
            left.Dispose();
            right.Dispose();
            control.Dispose();
        }
    }
}
