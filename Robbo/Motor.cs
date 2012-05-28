using System;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    public class Motor : IDisposable
    {
        private const int freqHz = 1000;

        private readonly bool inverted;
        private readonly PWM pwm;
        private readonly OutputPort directionPort;

        public Motor(PWM.Pin pwmPin, Cpu.Pin directionPin, bool inverted = false)
        {
            this.inverted = inverted;
            pwm = new PWM(pwmPin);
            directionPort = new OutputPort(directionPin, false);
        }

        public void Forward(int speed)
        {
            directionPort.Write(!inverted);
            pwm.Set(freqHz, (byte)speed);
        }

        public void Stop()
        {
            pwm.Set(false);
        }

        public void Reverse(int speed)
        {
            directionPort.Write(inverted);
            pwm.Set(freqHz, (byte)speed);
        }

        public void Dispose()
        {
            pwm.Dispose();
            directionPort.Dispose();
        }
    }
}
