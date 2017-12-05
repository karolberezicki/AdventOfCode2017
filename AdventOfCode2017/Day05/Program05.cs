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
            List<int> input = source.Split('\n').Select(int.Parse).ToList();

            int ip = 0;
            int steps = 0;

            while (ip < input.Count)
            {
                if (ip < 0)
                {
                    ip = 0;
                }

                int c = input[ip];

                steps++;

                input[ip] = input[ip] + 1;
                ip += c;
            }

            int partOne = steps;

            input = source.Split('\n').Select(int.Parse).ToList();
            ip = 0;
            steps = 0;

            while (ip < input.Count)
            {
                if (ip < 0)
                {
                    ip = 0;
                }

                int c = input[ip];

                steps++;

                if (c >= 3)
                {
                input[ip] = input[ip] - 1;

                }
                else
                {
                input[ip] = input[ip] + 1;

                }
                ip += c;
            }

            int partTwo = steps;


            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }
    }
}
