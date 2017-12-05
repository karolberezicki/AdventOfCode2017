using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day05
{
    public class Program05
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            int partOne = CalcStepsToExit(source);
            int partTwo = CalcStepsToExit(source, true);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static int CalcStepsToExit(string source, bool isPartTwo = false)
        {
            List<int> offsets = source.Split('\n').Select(int.Parse).ToList();

            int instructionPointer = 0;
            int steps = 0;
            while (instructionPointer < offsets.Count)
            {
                int jumpValue = offsets[instructionPointer];
                offsets[instructionPointer] = jumpValue >= 3 && isPartTwo ? offsets[instructionPointer] - 1 : offsets[instructionPointer] + 1;
                instructionPointer += jumpValue;
                steps++;
            }

            return steps;
        }
    }
}
