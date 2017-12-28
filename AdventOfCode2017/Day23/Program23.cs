using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day23
{
    public class Program23
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<List<string>> input = source.Split('\n').Select(c => c.Split(' ').ToList()).ToList();
            int partOne = PartOne(input);
            int partTwo = PartTwo(input);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static int PartOne(List<List<string>> input)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>
            {
                {"a", 0 }, {"b", 0 }, {"c", 0 }, {"d", 0 }, {"e", 0 }, {"f", 0 }, {"g", 0 }, {"h", 0 }, {"ip", 0 }
            };

            int countMulUsage = 0;

            do
            {
                List<string> instruction = input[registers["ip"]];
                string inst = instruction[0];
                string arg = instruction[1];

                if (!int.TryParse(instruction.Last(), out int val))
                {
                    val = registers[instruction.Last()];
                }

                switch (inst)
                {
                    case "set":
                        registers[arg] = val;
                        break;
                    case "sub":
                        registers[arg] -= val;
                        break;
                    case "mul":
                        registers[arg] *= val;
                        countMulUsage++;
                        break;
                    case "mod":
                        registers[arg] %= val;
                        break;
                    case "jnz":
                        if (!int.TryParse(arg, out int jumpCondition))
                        {
                            jumpCondition = registers[arg];
                        }
                        if (jumpCondition != 0)
                        {
                            registers["ip"] += val - 1;
                        }
                        break;
                }

                registers["ip"] += 1;

                if (registers["ip"] >= input.Count)
                {
                    break;
                }

            } while (true);

            return countMulUsage;
        }

        private static int PartTwo(List<List<string>> input)
        {
            Dictionary<string, int> registers = new Dictionary<string, int>
            {
                {"a", 1 }, {"b", 0 }, {"c", 0 }, {"d", 0 }, {"e", 0 }, {"f", 0 }, {"g", 0 }, {"h", 0 }, {"ip", 0 }
            };

            do
            {
                List<string> instruction = input[registers["ip"]];
                string inst = instruction[0];
                string arg = instruction[1];

                if (!int.TryParse(instruction.Last(), out int val))
                {
                    val = registers[instruction.Last()];
                }

                switch (inst)
                {
                    case "set":
                        registers[arg] = val;
                        break;
                    case "sub":
                        registers[arg] -= val;
                        break;
                    case "mul":
                        registers[arg] *= val;
                        break;
                    case "mod":
                        registers[arg] %= val;
                        break;
                    case "jnz":
                        if (!int.TryParse(arg, out int jumpCondition))
                        {
                            jumpCondition = registers[arg];
                        }
                        if (jumpCondition != 0)
                        {
                            registers["ip"] += val - 1;
                        }
                        break;
                }

                registers["ip"] += 1;

                if (registers["ip"] >= input.Count)
                {
                    break;
                }

            } while (registers["f"] == 0);

            int step = int.Parse(input.Last(c => c[0] == "sub")[2]) * -1;

            for (int i = registers["b"]; i <= registers["c"]; i += step)
            {
                if (!IsPrime(i))
                {
                    registers["h"]++;
                }
            }

            return registers["h"];
        }

        public static bool IsPrime(int number)
        {
            if (number == 1) return false;

            if (number == 2) return true;

            if (number % 2 == 0) return false;

            int boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
            {
                if (number % i == 0) return false;
            }

            return true;
        }
    }
}
