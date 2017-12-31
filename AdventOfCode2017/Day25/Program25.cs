using System;
using System.Collections.Generic;

namespace Day25
{
    public class Program25
    {
        public static State CurrentState;
        public static int Index;
        public static HashSet<int> States = new HashSet<int>();

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

            int diagnosticChecksum = States.Count;

            Console.WriteLine($"Part one: {diagnosticChecksum}");

            Console.ReadKey();
        }

        private static void MoveSlotLeft() => Index--;

        private static void MoveSlotRight() => Index++;

        private static void WriteValueZero() => States.Remove(Index);

        private static void WriteValueOne() => States.Add(Index);

        private static int CurrentValue => States.Contains(Index) ? 1 : 0;

        private static State ExecuteA()
        {
            if (CurrentValue == 0)
            {
                WriteValueOne();
                MoveSlotRight();
                return State.B;
            }

            WriteValueZero();
            MoveSlotLeft();
            return State.C;
        }

        private static State ExecuteB()
        {
            if (CurrentValue == 0)
            {
                WriteValueOne();
                MoveSlotLeft();
                return State.A;
            }

            WriteValueOne();
            MoveSlotRight();
            return State.D;
        }

        private static State ExecuteC()
        {
            if (CurrentValue == 0)
            {
                WriteValueOne();
                MoveSlotRight();
                return State.A;
            }

            WriteValueZero();
            MoveSlotLeft();
            return State.E;
        }

        private static State ExecuteD()
        {
            if (CurrentValue == 0)
            {
                WriteValueOne();
                MoveSlotRight();
                return State.A;
            }

            WriteValueZero();
            MoveSlotRight();
            return State.B;
        }

        private static State ExecuteE()
        {
            if (CurrentValue == 0)
            {
                WriteValueOne();
                MoveSlotLeft();
                return State.F;
            }

            WriteValueOne();
            MoveSlotLeft();
            return State.C;
        }

        private static State ExecuteF()
        {
            if (CurrentValue == 0)
            {
                WriteValueOne();
                MoveSlotRight();
                return State.D;
            }

            WriteValueOne();
            MoveSlotRight();
            return State.A;
        }
    }

    public enum State
    {
        A,B,C,D,E,F
    }
}