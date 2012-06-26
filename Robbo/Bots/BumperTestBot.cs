using System;
using System.Threading;
using Microsoft.SPOT;
using Robbo.Devices;

namespace Robbo.Bots
{
    public class BumperTestBot : IBot
    {
        private readonly Bumper bumper;

        public BumperTestBot(Bumper bumper)
        {
            this.bumper = bumper;
        }

        public void Go()
        {
            bumper.Bumped += (sender, args) => Debug.Print("Bump " + DateTime.Now.Ticks);
            Thread.Sleep(Timeout.Infinite);
        }

        public void Dispose()
        {
            bumper.Dispose();
        }
    }
}