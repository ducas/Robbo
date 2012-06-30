using System;
using System.Threading;
using GHIElectronics.NETMF.Hardware;

namespace Robbo.Devices
{
    /// <summary>
    /// Represents a Piezo buzzer.
    /// </summary>
    public class Piezo : IDisposable
    {
        private readonly PWM pwm;

        /// <summary>
        /// Creates an instance of the Piezo buzzer.
        /// </summary>
        /// <param name="pin">The PWM pin connected to the buzzer.</param>
        public Piezo(PWM.Pin pin)
        {
            pwm = new PWM(pin);
        }

        /// <summary>
        /// Plays a tone.
        /// </summary>
        /// <param name="freq">The desired frequency.</param>
        /// <param name="duration">The duration of the tone.</param>
        public void Play(int freq, int duration)
        {
            pwm.Set(freq, 50);
            Thread.Sleep(duration);
            pwm.Set(freq, 0);
        }

        /// <summary>
        /// Plays a Tone
        /// </summary>
        /// <param name="tone">The desired tone.</param>
        public void Play(Tone tone)
        {
            Play(tone.Frequency, tone.Duration);
        }

        /// <summary>
        /// Plays a tune (collection of tones).
        /// </summary>
        /// <param name="tune">The collection of tones to play.</param>
        public void Play(Tone[] tune)
        {
            for (var i = 0; i < tune.Length; i++)
            {
                Play(tune[i]);
            }
        }

        /// <summary>
        /// Disposes the Piezo's PWM channel.
        /// </summary>
        public void Dispose()
        {
            pwm.Dispose();
        }
    }
}