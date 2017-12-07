using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Day07
{
    public class Program07
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            IReadOnlyCollection<string> input = source.Split('\n');

            Solve(input, out string partOne, out int partTwo);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static void Solve(IReadOnlyCollection<string> input, out string partOne, out int partTwo)
        {
            Dictionary<string, int> totalWeights = new Dictionary<string, int>();

            Queue<string> toCalcTotalWeights = new Queue<string>();

            Dictionary<string, int> diskWeights = input.ToDictionary(
                c => string.Join("", c.Take(c.IndexOf('(') - 1)),
                c => int.Parse(Regex.Match(c, "[0-9]+").Value));

            Dictionary<string, List<string>> nodesWithChildren = new Dictionary<string, List<string>>();

            List<string[]> splitedInput = input.Select(line => Regex.Split(line, @" \([0-9]+\)( -> )?")).ToList();
            splitedInput.Where(c => c.Length > 2).ToList().ForEach(program => nodesWithChildren.Add(program[0], Regex.Split(program[2], @", ").ToList()));
            splitedInput.Where(c => c.Length <= 2).ToList().ForEach(program => totalWeights.Add(program[0], diskWeights[program[0]]));

            nodesWithChildren.Select(c => c.Key).ToList().ForEach(toCalcTotalWeights.Enqueue);

            while (toCalcTotalWeights.Count > 0)
            {
                string node = toCalcTotalWeights.Dequeue();

                List<string> childrenNodes = nodesWithChildren[node];
                if (!childrenNodes.Except(totalWeights.Select(c => c.Key)).Any())
                {
                    int weight = diskWeights[node] + totalWeights.Where(c => childrenNodes.Contains(c.Key)).Select(c => c.Value).Sum();
                    totalWeights.Add(node, weight);

                }
                else
                {
                    toCalcTotalWeights.Enqueue(node);
                }
            }

            partOne = totalWeights.OrderByDescending(c => c.Value).First().Key;

            foreach (KeyValuePair<string, List<string>> node in nodesWithChildren)
            {
                List<IGrouping<int, KeyValuePair<string, int>>> group = totalWeights
                    .Where(c => node.Value.Contains(c.Key))
                    .GroupBy(b => b.Value)
                    .ToList();

                if (group.Count <= 1)
                {
                    continue;
                }
                IGrouping<int, KeyValuePair<string, int>> good = group.First(c => c.Count() > 1);
                IGrouping<int, KeyValuePair<string, int>> bad = group.First(c => c.Count() == 1);

                int diff = good.Key - bad.Key;


                partTwo = diskWeights[bad.First().Key] + diff;
                return;
            }
            throw new NotSupportedException();
        }
    }
}
