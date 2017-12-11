using System;

namespace Day11
{
    public struct Hex
    {
        public readonly int Q, R, S;

        public Hex(int q, int r, int s)
        {
            Q = q;
            R = r;
            S = s;
        }
        public static Hex Hex_Subtract(Hex a, Hex b)
        {
            return new Hex(a.Q - b.Q, a.R - b.R, a.S - b.S);
        }

        public static int Hex_Length(Hex hex)
        {
            return (Math.Abs(hex.Q) + Math.Abs(hex.R) + Math.Abs(hex.S)) / 2;
        }

        public static int Hex_Distance(Hex a, Hex b)
        {
            return Hex_Length(Hex_Subtract(a, b));
        }
    };
}