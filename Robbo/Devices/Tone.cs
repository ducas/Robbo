namespace Robbo.Devices
{
    /// <summary>
    /// Represents a tone to be played by a Piezo buzzer.
    /// </summary>
    public struct Tone
    {
        #region Tone Frequencies
        public const int Breathe = 0;
        public const int C0 = 16;
        public const int CSharp0 = 17;
        public const int D0 = 18;
        public const int EFlat0 = 19;
        public const int E0 = 21;
        public const int F0 = 22;
        public const int FSharp0 = 23;
        public const int G0 = 25;
        public const int GSharp0 = 26;
        public const int A0 = 28;
        public const int BFlat0 = 29;
        public const int B0 = 31;
        public const int C1 = 33;
        public const int CSharp1 = 35;
        public const int D1 = 37;
        public const int EFlat1 = 39;
        public const int E1 = 41;
        public const int F1 = 44;
        public const int FSharp1 = 46;
        public const int G1 = 49;
        public const int GSharp1 = 52;
        public const int A1 = 55;
        public const int BFlat1 = 58;
        public const int B1 = 62;
        public const int C2 = 65;
        public const int CSharp2 = 69;
        public const int D2 = 73;
        public const int EFlat2 = 78;
        public const int E2 = 82;
        public const int F2 = 87;
        public const int FSharp2 = 93;
        public const int G2 = 98;
        public const int GSharp2 = 104;
        public const int A2 = 110;
        public const int BFlat2 = 117;
        public const int B2 = 123;
        public const int C3 = 131;
        public const int CSharp3 = 139;
        public const int D3 = 147;
        public const int EFlat3 = 156;
        public const int E3 = 165;
        public const int F3 = 175;
        public const int FSharp3 = 185;
        public const int G3 = 196;
        public const int GSharp3 = 208;
        public const int A3 = 220;
        public const int BFlat3 = 233;
        public const int B3 = 247;
        /// <summary>
        /// Middle C.
        /// </summary>
        public const int C4 = 262;
        public const int CSharp4 = 277;
        public const int D4 = 294;
        public const int EFlat4 = 311;
        public const int E4 = 330;
        public const int F4 = 349;
        public const int FSharp4 = 370;
        public const int G4 = 392;
        public const int GSharp4 = 415;
        public const int A4 = 440;
        public const int BFlat4 = 466;
        public const int B4 = 493;
        public const int C5 = 523;
        public const int CSharp5 = 554;
        public const int D5 = 587;
        public const int EFlat5 = 622;
        public const int E5 = 659;
        public const int F5 = 698;
        public const int FSharp5 = 740;
        public const int G5 = 784;
        public const int GSharp5 = 831;
        public const int A5 = 880;
        public const int BFlat5 = 932;
        public const int B5 = 988;
        public const int C6 = 1047;
        public const int CSharp6 = 1109;
        public const int D6 = 1175;
        public const int EFlat6 = 1245;
        public const int E6 = 1319;
        public const int F6 = 1397;
        public const int FSharp6 = 1480;
        public const int G6 = 1568;
        public const int GSharp6 = 1661;
        public const int A6 = 1760;
        public const int BFlat6 = 1865;
        public const int B6 = 1976;
        public const int C7 = 2093;
        public const int CSharp7 = 2217;
        public const int D7 = 2349;
        public const int EFlat7 = 2489;
        public const int E7 = 2637;
        public const int F7 = 2794;
        public const int FSharp7 = 2960;
        public const int G7 = 3136;
        public const int GSharp7 = 3322;
        public const int A7 = 3520;
        public const int BFlat7 = 3729;
        public const int B7 = 3951;
        public const int C8 = 4186;
        public const int CSharp8 = 4435;
        public const int D8 = 4699;
        public const int EFlat8 = 4978;
        #endregion

        /// <summary>
        /// Creates an instance of a Tone.
        /// </summary>
        /// <param name="frequency">The frequency of the tone in hertz.</param>
        /// <param name="duration">The duration of the tone in milliseconds.</param>
        public Tone(int frequency, int duration)
        {
            Frequency = frequency;
            Duration = duration;
        }

        /// <summary>
        /// The frequency of the tone in hertz.
        /// </summary>
        public int Frequency;
        /// <summary>
        /// The duration of the tone in milliseconds.
        /// </summary>
        public int Duration;
    }
}