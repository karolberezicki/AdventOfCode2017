using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day24
{
    public class Program24
    {
        public static void Main(string[] args)
        {
            List<BridgeComponent> ports = ExtractPorts();
            HashSet<HashSet<BridgeComponent>> bridges = Build(ports);

            int strengthOfStrongestBridge = bridges.Select(c => c.Select(b => b.PortA + b.PortB).Sum()).Max();

            int maxBridgeLength = bridges.Select(b => b.Count).Max();
            int strengthOfLongestBridge = bridges
                .Where(c => c.Count == maxBridgeLength)
                .Select(c => c.Select(b => b.PortA + b.PortB).Sum()).Max();

            Console.WriteLine($"Part one: {strengthOfStrongestBridge}");
            Console.WriteLine($"Part two: {strengthOfLongestBridge}");

            Console.ReadKey();
        }
        private static List<BridgeComponent> ExtractPorts()
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<List<int>> input = source.Split('\n').Select(c => c.Split('/').Select(int.Parse).ToList()).ToList();
            List<BridgeComponent> ports = input.Select((c, i) => new BridgeComponent(c[0], c[1], i)).ToList();
            return ports;
        }

        private static HashSet<HashSet<BridgeComponent>> FindStartPorts(List<BridgeComponent> ports)
        {
            return new HashSet<HashSet<BridgeComponent>>(
                ports.Where(c => c.PortA == 0 || c.PortA == 0)
                    .Select(c => new HashSet<BridgeComponent>
                    { new BridgeComponent(0, c.PortA == 0 ? c.PortB : c.PortA, c.Number) }));
        }

        private static HashSet<HashSet<BridgeComponent>> Build(List<BridgeComponent> ports)
        {
            HashSet<HashSet<BridgeComponent>> bridges = FindStartPorts(ports);

            bool foundBridgeExtensions = true;

            while (foundBridgeExtensions)
            {
                foundBridgeExtensions = false;

                HashSet<HashSet<BridgeComponent>> newBridges = new HashSet<HashSet<BridgeComponent>>();

                foreach (HashSet<BridgeComponent> bridge in bridges)
                {
                    BridgeComponent last = bridge.Last();

                    List<BridgeComponent> possibleConnections = ports.Where(c =>
                        (c.PortA == last.PortB || c.PortB == last.PortB)
                        && !bridge.Select(b => b.Number).Contains(c.Number)).ToList();

                    if (possibleConnections.Count == 0)
                    {
                        newBridges.Add(bridge);
                        continue;
                    }

                    foundBridgeExtensions = true;
                    foreach (BridgeComponent c in possibleConnections)
                    {
                        newBridges.Add(new HashSet<BridgeComponent>(bridge)
                        {
                            last.PortB == c.PortA
                                ? new BridgeComponent(c.PortA, c.PortB, c.Number)
                                : new BridgeComponent(c.PortB, c.PortA, c.Number)
                        });
                    }
                }

                bridges = newBridges;
            }

            return bridges;
        }
    }
}
