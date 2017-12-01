using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day01
{
    public class Program01
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            List<int> sourceInts = source.Select(c => c - '0').ToList();

            int partOne = sourceInts.Where((c, i) =>
            i == sourceInts.Count - 1 && sourceInts.First() == sourceInts.Last()
            ||c == sourceInts[i + 1])
            .Sum();

            int partTwo = sourceInts.Where((c, i) =>
            i < sourceInts.Count / 2 && c == sourceInts[i + source.Length / 2] ||
            i > sourceInts.Count / 2 && c == sourceInts[i - source.Length / 2])
            .Sum();

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }
    }
}
