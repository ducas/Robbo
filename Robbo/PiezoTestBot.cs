using System.Threading;

namespace Robbo
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
            piezo.Play(262, t3);
            piezo.Play(330, t);
            Thread.Sleep(t);
            piezo.Play(367, t);
            Thread.Sleep(t);
            piezo.Play(440, t);
            piezo.Play(392, t3);
            piezo.Play(330, t);
            Thread.Sleep(t);
            piezo.Play(262, t2);
            piezo.Play(220, t);
            piezo.Play(185, t);
            piezo.Play(175, t);
            piezo.Play(175, t);
            piezo.Play(196, t);
            Thread.Sleep(t);
            piezo.Play(233, t3);
            piezo.Play(262, t);
            piezo.Play(262, t);
            piezo.Play(262, t);
            piezo.Play(262, t);
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