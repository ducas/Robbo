using System.Threading;
using Robbo.Devices;

namespace Robbo.Bots
{
    public class PiezoTestBot
    {
        private readonly Piezo piezo;

        public PiezoTestBot(Piezo piezo)
        {
            this.piezo = piezo;
        }

        public void Go()
        {
            int t = 125;
            int t2 = 250;
            int t3 = 375;
            PlayScale(t);
            Thread.Sleep(1000);
            PlaySimpsons(t2, t, t3);
        }

        private void PlaySimpsons(int t2, int t, int t3)
        {
            piezo.Play(new[]
                           {
                               new Tone(Tone.C4, t3),
                               new Tone(Tone.E4, t),
                               new Tone(Tone.Breathe, t),
                               new Tone(Tone.FSharp4, t),
                               new Tone(Tone.Breathe, t),
                               new Tone(Tone.A4, t),
                               new Tone(Tone.G4, t3),
                               new Tone(Tone.E4, t),
                               new Tone(0, t),
                               new Tone(Tone.C4, t2),
                               new Tone(Tone.A3, t),
                               new Tone(Tone.FSharp3, t),
                               new Tone(Tone.F3, t),
                               new Tone(Tone.F3, t),
                               new Tone(Tone.G3, t),
                               new Tone(0, t),
                               new Tone(Tone.BFlat3, t3),
                               new Tone(Tone.C4, t),
                               new Tone(Tone.C4, t),
                               new Tone(Tone.C4, t),
                               new Tone(Tone.C4, t)
                           }
                );
        }

        private void PlayScale(int t)
        {
            piezo.Play(262, t); // C4
            piezo.Play(294, t); // D4
            piezo.Play(330, t); // E
            piezo.Play(350, t); // F
            piezo.Play(392, t); // G
            piezo.Play(440, t); // A
            piezo.Play(494, t); // B
            piezo.Play(523, t); // C5
            piezo.Play(494, t); // B
            piezo.Play(440, t); // A
            piezo.Play(392, t); // G
            piezo.Play(350, t); // F
            piezo.Play(330, t); // E
            piezo.Play(294, t); // D4
            piezo.Play(262, t); // C4
        }
    }
}