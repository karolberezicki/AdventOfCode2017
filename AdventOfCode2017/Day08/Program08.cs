using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day08
{
    public class Program08
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<string> input = source.Split('\n').ToList();

            Dictionary<string, int> registers = new Dictionary<string, int>();

            int localMax = 0;
            foreach (string line in input)
            {
                string[] lineParts = Regex.Split(line, " if ");

                string[] leftSide = lineParts[0].Split(' ');
                string[] rightSide = lineParts[1].Split(' ');

                int comparedValue = 0;

                if (registers.ContainsKey(rightSide[0]))
                {
                    comparedValue = registers[rightSide[0]];
                }

                bool conditionFulfilled = false;
                switch (rightSide[1])
                {
                    case ">":
                        conditionFulfilled = comparedValue > int.Parse(rightSide[2]);
                        break;
                    case "<":
                        conditionFulfilled = comparedValue < int.Parse(rightSide[2]);
                        break;
                    case ">=":
                        conditionFulfilled = comparedValue >= int.Parse(rightSide[2]);
                        break;
                    case "<=":
                        conditionFulfilled = comparedValue <= int.Parse(rightSide[2]);
                        break;
                    case "==":
                        conditionFulfilled = comparedValue == int.Parse(rightSide[2]);
                        break;
                    case "!=":
                        conditionFulfilled = comparedValue != int.Parse(rightSide[2]);
                        break;
                }

                if (!conditionFulfilled)
                {
                    continue;
                }

                int changeAmountValue = int.Parse(leftSide[2]);

                changeAmountValue = leftSide[1] == "dec" ? changeAmountValue * -1 : changeAmountValue;

                if (registers.ContainsKey(leftSide[0]))
                {
                    registers[leftSide[0]] = registers[leftSide[0]] + changeAmountValue;
                }
                else
                {
                    registers.Add(leftSide[0], changeAmountValue);
                }

                localMax = Math.Max(localMax, registers[leftSide[0]]);
            }

            int overallMax = registers.Values.Max();

            Console.WriteLine($"Part one: {overallMax}");
            Console.WriteLine($"Part two: {localMax}");

            Console.ReadKey();
        }
    }
}
