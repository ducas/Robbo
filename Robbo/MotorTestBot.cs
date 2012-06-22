using System.Threading;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    /// <summary>
    /// A test bot that runs the motors in different directions.
    /// </summary>
    public class MotorTestBot
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
                control.Write(true);

                left.Forward(100);
                right.Forward(100);

                Thread.Sleep(1000);

                left.Forward(50);
                right.Reverse(50);

                Thread.Sleep(1000);

                left.Reverse(50);
                right.Forward(50);

                Thread.Sleep(1000);

                left.Reverse(100);
                right.Reverse(100);

                Thread.Sleep(1000);

                control.Write(false);

                Thread.Sleep(1000);
            }
            // ReSharper disable FunctionNeverReturns
        }
        // ReSharper restore FunctionNeverReturns
    }
}
