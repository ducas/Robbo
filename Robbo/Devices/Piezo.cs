using System;
using System.Threading;
using GHIElectronics.NETMF.Hardware;

namespace Robbo.Devices
{
    public class Piezo : IDisposable
    {
        private readonly PWM pwm;

        public Piezo(PWM.Pin pin)
        {
            pwm = new PWM(pin);
        }

        public void Play(int freq, int duration)
        {
            pwm.Set(freq, 50);
            Thread.Sleep(duration);
            pwm.Set(freq, 0);
        }

        public void Play(Tone tone)
        {
            Play(tone.Frequency, tone.Duration);
        }

        public void Play(Tone[] tune)
        {
            for (var i = 0; i < tune.Length; i++)
            {
                Play(tune[i]);
            }
        }

        public void Dispose()
        {
            pwm.Dispose();
        }
    }
}