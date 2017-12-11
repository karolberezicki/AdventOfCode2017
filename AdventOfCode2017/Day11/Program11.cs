using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day11
{
    public class Program11
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> input = source.Split(',').ToList();

            Hex start = new Hex();
            Hex current = new Hex();

            int maxDistanceFromStart = 0;

            foreach (string step in input)
            {
                switch (step)
                {
                    case "n":
                        current = new Hex(current.R, current.Q-1, current.S+1);
                        break;

                    case "s":
                        current = new Hex(current.R, current.Q+1, current.S-1);
                        break;

                    case "ne":
                        current = new Hex(current.R + 1, current.Q-1, current.S);
                        break;

                    case "se":
                        current = new Hex(current.R + 1, current.Q, current.S - 1);
                        break;

                    case "nw":
                        current = new Hex(current.R - 1, current.Q, current.S+1);
                        break;

                    case "sw":
                        current = new Hex(current.R - 1, current.Q+1, current.S);
                        break;
                }

                maxDistanceFromStart = Math.Max(maxDistanceFromStart, Hex.Hex_Distance(current, start));
            }

            int distanceFromStart = Hex.Hex_Distance(current, start);

            Console.WriteLine($"Part one: {distanceFromStart}");
            Console.WriteLine($"Part two: {maxDistanceFromStart}");

            Console.ReadKey();
        }
    }
}
