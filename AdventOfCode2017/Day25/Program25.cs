using System;
using System.Collections.Generic;
using System.Linq;

namespace Day25
{
    public class Program25
    {
        public static Dictionary<int, int> States = new Dictionary<int, int>();
        public static State CurrentState;
        public static int Index;

        public static void Main(string[] args)
        {
            for (int i = 0; i < 12919244; i++)
            {
                switch (CurrentState)
                {
                    case State.A:
                        CurrentState = ExecuteA();
                        break;
                    case State.B:
                        CurrentState = ExecuteB();
                        break;
                    case State.C:
                        CurrentState = ExecuteC();
                        break;
                    case State.D:
                        CurrentState = ExecuteD();
                        break;
                    case State.E:
                        CurrentState = ExecuteE();
                        break;
                    case State.F:
                        CurrentState = ExecuteF();
                        break;
                }
            }

            int diagnosticChecksum = States.Values.Sum();

            Console.WriteLine($"Part one: {diagnosticChecksum}");

            Console.ReadKey();
        }

        private static void MoveSlotLeft() => Index--;

        private static void MoveSlotRight() => Index++;

        private static void WriteValue0() => States[Index] = 0;

        private static void WriteValue1() => States[Index] = 1;

        private static int GetValue() => States.ContainsKey(Index) ? States[Index] : 0;

        private static State ExecuteA()
        {
            if (GetValue() == 0)
            {
                WriteValue1();
                MoveSlotRight();
                return State.B;
            }

            WriteValue0();
            MoveSlotLeft();
            return State.C;
        }

        private static State ExecuteB()
        {
            if (GetValue() == 0)
            {
                WriteValue1();
                MoveSlotLeft();
                return State.A;
            }

            WriteValue1();
            MoveSlotRight();
            return State.D;
        }

        private static State ExecuteC()
        {
            if (GetValue() == 0)
            {
                WriteValue1();
                MoveSlotRight();
                return State.A;
            }

            WriteValue0();
            MoveSlotLeft();
            return State.E;
        }

        private static State ExecuteD()
        {
            if (GetValue() == 0)
            {
                WriteValue1();
                MoveSlotRight();
                return State.A;
            }

            WriteValue0();
            MoveSlotRight();
            return State.B;
        }

        private static State ExecuteE()
        {
            if (GetValue() == 0)
            {
                WriteValue1();
                MoveSlotLeft();
                return State.F;
            }

            WriteValue1();
            MoveSlotLeft();
            return State.C;
        }

        private static State ExecuteF()
        {
            if (GetValue() == 0)
            {
                WriteValue1();
                MoveSlotRight();
                return State.D;
            }

            WriteValue1();
            MoveSlotRight();
            return State.A;
        }
    }

    public enum State
    {
        A,B,C,D,E,F
    }
}