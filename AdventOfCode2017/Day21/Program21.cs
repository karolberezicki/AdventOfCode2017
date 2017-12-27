using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day21
{
    public class Program21
    {
        public static void Main()
        {
            Dictionary<string, string> rules = GetRules();

            int partOne = Solve(rules, 5);
            int partTwo = Solve(rules, 18);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static int Solve(Dictionary<string, string> rules, int iterations)
        {
            string[] grid = { ".#.", "..#", "###" };

            for (int i = 0; i < iterations; i++)
            {
                grid = Enhance(grid, rules);
            }

            return grid.Sum(c => c.Count(d => d == '#'));
        }

        private static Dictionary<string, string> GetRules()
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            Dictionary<string, string> rules = source.Split('\n')
                .Select(c => c.Split(new[] { " => " }, StringSplitOptions.RemoveEmptyEntries))
                .ToDictionary(c => c[0], c => c[1]);

            CompleteRules(rules);

            return rules;
        }

        private static void CompleteRules(Dictionary<string, string> rules)
        {
            foreach (KeyValuePair<string, string> rule in rules.ToDictionary(c => c.Key, c => c.Value))
            {
                rules[rule.Key] = rule.Value;
                rules[FlipHorizontal(rule.Key)] = rule.Value;
                rules[FlipVertical(rule.Key)] = rule.Value;

                string newKey = rule.Key;
                for (int i = 0; i < 3; i++)
                {
                    newKey = Rotate(newKey);
                    rules[newKey] = rule.Value;
                    rules[FlipHorizontal(newKey)] = rule.Value;
                    rules[FlipVertical(newKey)] = rule.Value;
                }
            }
        }

        private static string FlipHorizontal(string rule) =>
            string.Join("/", rule.Split('/').Select(c => string.Join("", c.Reverse())));

        private static string FlipVertical(string rule) =>
            string.Join("/", rule.Split('/').Reverse());

        private static string Rotate(string rule)
        {
            char[][] rows = rule.Split('/').Select(c => c.ToCharArray()).ToArray();
            return string.Join("/",
                Enumerable.Range(0, rows.Length)
                .Select(x => Enumerable.Range(0, rows.Length)
                .Select(y => rows[y][x]))
                .Reverse().Select(c => string.Join("", c)));
        }

        private static string[] Enhance(string[] grid, Dictionary<string, string> rules)
        {
            int size = grid.Length % 2 == 0 ? 2 : 3;
            List<string> newGrid = new List<string>();

            for (int skip = 0; skip < grid.Length; skip = skip + size)
            {
                string[] newSubGrid = new string[size+1];
                List<string> subGridRow = grid.Skip(skip).Take(size).ToList();

                for (int i = 0; i < subGridRow[0].Length ; i = i+size)
                {
                    List<string> subGrid = new List<string>();
                    for (int j = 0; j < size; j++)
                    {
                        subGrid.Add(string.Join("", subGridRow[j].Skip(i).Take(size)));
                    }

                    string[] subGridEnhanced = rules[string.Join("/", subGrid)].Split('/');

                    for (int j = 0; j < subGridEnhanced.Length; j++)
                    {
                        newSubGrid[j] += subGridEnhanced[j];
                    }
                }

                newGrid.AddRange(newSubGrid);
            }

            return newGrid.ToArray();
        }
    }
}
