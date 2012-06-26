using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;

namespace Robbo.Devices
{
    public class Bumper : IDisposable
    {
        private readonly InterruptPort interruptPort;

        public event EventHandler Bumped;

        public Bumper(Cpu.Pin interruptPin)
        {
            interruptPort = new InterruptPort(interruptPin, true, Port.ResistorMode.PullDown, Port.InterruptMode.InterruptEdgeHigh);
            interruptPort.OnInterrupt += InterruptPortOnInterrupt;
            interruptPort.EnableInterrupt();
        }

        private void InterruptPortOnInterrupt(uint data1, uint data2, DateTime time)
        {
            OnBumped();
        }

        protected virtual void OnBumped()
        {
            if (Bumped != null) Bumped(this, EventArgs.Empty);
        }

        public void Dispose()
        {
            interruptPort.OnInterrupt -= InterruptPortOnInterrupt;
            interruptPort.Dispose();
        }

    }
}