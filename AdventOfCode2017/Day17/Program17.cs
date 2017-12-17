using System;
using System.Collections.Generic;

namespace Day17
{
    public class Program17
    {
        public static void Main(string[] args)
        {
            const int input = 370;

            List<int> buffer = new List<int>{0};

            int currentPossition = 0;
            for (int i = 1; i <= 2017; i++)
            {
                currentPossition = (currentPossition + input) % i + 1;
                buffer.Insert(currentPossition, i);
            }

            int partOne = buffer[buffer.IndexOf(2017) + 1];

            currentPossition = 0;
            int partTwo = 0;
            for (int i = 1; i <= 50000000; i++)
            {
                currentPossition = (currentPossition + input) % i + 1;

                if (currentPossition == 1)
                {
                    partTwo = i;
                }
            }

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }
    }
}
