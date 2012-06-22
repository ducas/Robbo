using System;
using GHIElectronics.NETMF.Hardware;
using Microsoft.SPOT.Hardware;

namespace Robbo
{
    public class Motor : IDisposable
    {
        private const int freqHz = 1000;

        private readonly PWM pwm;
        private readonly OutputPort forwardPort;
        private readonly OutputPort reversePort;

        /// <summary>
        /// Represents a motor driver channel for a motor
        /// </summary>
        /// <param name="pwmPin">The pin connected to the PWM input on the driver</param>
        /// <param name="forwardPin">The pin connected to the forward direction pin on the driver (IN0)</param>
        /// <param name="reversePin">The pin connected to the reverse direction pin on the driver (IN1)</param>
        public Motor(PWM.Pin pwmPin, Cpu.Pin forwardPin, Cpu.Pin reversePin)
        {
            pwm = new PWM(pwmPin);
            forwardPort = new OutputPort(forwardPin, false);
            reversePort = new OutputPort(reversePin, false);
        }

        public int Pwm { set { pwm.Set(freqHz, (byte)value); } }
        public bool In0 { set { forwardPort.Write(value); } }
        public bool In1 { set { reversePort.Write(value); } }

        public void Forward(int speed)
        {
            forwardPort.Write(true);
            reversePort.Write(false);
            pwm.Set(freqHz, (byte)speed);
        }

        public void Stop()
        {
            forwardPort.Write(false);
            reversePort.Write(false);
            pwm.Set(false);
        }

        public void Reverse(int speed)
        {
            forwardPort.Write(false);
            reversePort.Write(true);
            pwm.Set(freqHz, (byte)speed);
        }

        public void Dispose()
        {
            pwm.Dispose();
            forwardPort.Dispose();
            reversePort.Dispose();
        }
    }
}
