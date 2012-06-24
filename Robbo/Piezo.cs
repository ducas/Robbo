using System.Threading;
using GHIElectronics.NETMF.Hardware;

namespace Robbo
{
    public class Piezo
    {
        private PWM pwm;

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
    }
}