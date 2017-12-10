using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day10
{
    public class Program10
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            int partOne = PartOne(source);
            string partTwo = PartTwo(source);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static int PartOne(string source)
        {
            List<int> input = source.Split(',').Select(int.Parse).ToList();
            List<int> list = Enumerable.Range(0, 256).ToList();

            int currentPosition = 0;
            int skipSize = 0;

            KnotHash(input, list, ref currentPosition, ref skipSize);

            return list[0] * list[1];
        }

        private static string PartTwo(string source)
        {
            List<int> input = source.Select(c => (int)c).Concat(new[] { 17, 31, 73, 47, 23 }).ToList();
            List<int> list = Enumerable.Range(0, 256).ToList();

            int currentPosition = 0;
            int skipSize = 0;

            for (int round = 0; round < 64; round++)
            {
                KnotHash(input, list, ref currentPosition, ref skipSize);
            }

            return SparseToDenseHash(list);
        }

        private static void KnotHash(IEnumerable<int> input, IList<int> list, ref int currentPosition, ref int skipSize)
        {
            foreach (int sliceLength in input)
            {
                List<int> subList = new List<int>();
                for (int i = currentPosition; i < currentPosition + sliceLength; i++)
                {
                    int index = i % list.Count;
                    subList.Add(list[index]);
                }

                subList.Reverse();

                int sublistIndex = 0;
                for (int i = currentPosition; i < currentPosition + sliceLength; i++)
                {
                    int index = i % list.Count;
                    list[index] = subList[sublistIndex];
                    sublistIndex++;
                }

                currentPosition += sliceLength + skipSize;
                currentPosition = currentPosition % list.Count;

                skipSize++;
            }
        }

        private static string SparseToDenseHash(IReadOnlyCollection<int> list)
        {
            List<int> xored = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                xored.Add(list.Skip(i * 16).Take(16).Aggregate((acc, val) => (byte)(acc ^ val)));
            }
            return string.Join("", xored.Select(c => c.ToString("x2")));
        }
    }
}
