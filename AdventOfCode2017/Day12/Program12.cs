using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day12
{
    public class Program12
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            string[] input = source.Split('\n');

            Dictionary<string, List<string>> connections = input
                .Select(c => c.Split(new[] {" <-> "}, StringSplitOptions.None))
                .ToDictionary(c => c[0], c => c[1].Split(new[] {", "}, StringSplitOptions.None).ToList());

            HashSet<HashSet<string>> groupsOfConnections = new HashSet<HashSet<string>>();

            foreach (KeyValuePair<string, List<string>> connection in connections)
            {
                if (groupsOfConnections.Any(c => c.Any(d => d == connection.Key)))
                {
                    continue;
                }

                HashSet<string> group = new HashSet<string>();
                Queue<string> toCheck = new Queue<string>();
                toCheck.Enqueue(connection.Key);
                group.Add(connection.Key);

                while (toCheck.Count > 0)
                {
                    string currentConnection = toCheck.Dequeue();

                    IEnumerable<string> possibleConnections = connections[currentConnection].Except(group);

                    foreach (string possibleConnection in possibleConnections)
                    {
                        toCheck.Enqueue(possibleConnection);
                        group.Add(possibleConnection);
                    }
                }

                groupsOfConnections.Add(group);
            }

            Console.WriteLine($"Part one: {groupsOfConnections.First().Count}");
            Console.WriteLine($"Part two: {groupsOfConnections.Count}");

            Console.ReadKey();
        }
    }
}
