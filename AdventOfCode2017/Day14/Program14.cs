using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Day14
{
    public class Program14
    {
        public static void Main(string[] args)
        {
            const string source = "ljoxqyyw";
            int[][] grid = GenerateGrid(source);

            var squaresCount = grid.SelectMany(c => c).Sum();

            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid[i].Length; j++)
                {
                    var x = grid[i][j];
                    var n = GetNeighbors(i, j, grid);
                }
            }

        }

        private static int[][] GenerateGrid(string source)
        {
            int[][] grid = new int[128][];
            for (int i = 0; i < 128; i++)
            {
                List<int> input = $"{source}-{i}".Select(c => (int)c).Concat(new[] { 17, 31, 73, 47, 23 }).ToList();
                List<int> hash = KnotHash(input, 64);
                string denseBinHash = SparseToDenseBinaryHash(hash);
                grid[i] = denseBinHash.Select(c => int.Parse(c.ToString())).ToArray();
            }
            return grid;
        }

        private static List<int> KnotHash(IReadOnlyCollection<int> input, uint rounds)
        {
            List<int> hash = Enumerable.Range(0, 256).ToList();
            int currentPosition = 0;
            int skipSize = 0;

            for (int round = 0; round < rounds; round++)
            {
                foreach (int sliceLength in input)
                {
                    List<int> subList = new List<int>();
                    for (int i = currentPosition; i < currentPosition + sliceLength; i++)
                    {
                        int index = i % hash.Count;
                        subList.Add(hash[index]);
                    }

                    subList.Reverse();

                    for (int i = currentPosition, sublistIndex = 0; i < currentPosition + sliceLength; i++, sublistIndex++)
                    {
                        int index = i % hash.Count;
                        hash[index] = subList[sublistIndex];
                    }

                    currentPosition += sliceLength + skipSize;
                    currentPosition = currentPosition % hash.Count;

                    skipSize++;
                }
            }

            return hash;
        }

        private static string SparseToDenseBinaryHash(IReadOnlyCollection<int> list)
        {
            List<int> xored = new List<int>();
            for (int i = 0; i < 16; i++)
            {
                xored.Add(list.Skip(i * 16).Take(16).Aggregate((acc, val) => (byte)(acc ^ val)));
            }
            return string.Join("", xored.Select(c => Convert.ToString(c, 2).PadLeft(8, '0')));
        }

        public static List<Neighbor<T>> GetNeighbors<T>(int i, int j, T[][] cellValues)
        {
            int size = cellValues.Length;

            List<Neighbor<T>> neighbors = new List<Neighbor<T>>();

            if (IsInsideArray(i, j, size))
            {
                if (IsInsideArray(i + 1, j, size))
                    neighbors.Add(new Neighbor<T>{ Value = cellValues[i + 1][j], X = i + 1, Y = j});
                if (IsInsideArray(i - 1, j, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i - 1][j], X = i-1, Y = j });
                if (IsInsideArray(i, j + 1, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i][j + 1], X = i, Y = j+1 });
                if (IsInsideArray(i, j - 1, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i][j - 1], X = i, Y = j-1 });
                if (IsInsideArray(i - 1, j + 1, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i - 1][j + 1], X = i-1, Y = j+1 });
                if (IsInsideArray(i + 1, j - 1, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i + 1][j - 1], X = i+1, Y = j-1 });
                if (IsInsideArray(i + 1, j + 1, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i + 1][j + 1], X = i+1, Y = j+1 });
                if (IsInsideArray(i - 1, j - 1, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i - 1][j - 1], X = i-1, Y = j-1 });
            }
            return neighbors;
        }

        public static bool IsInsideArray(int i, int j, int size)
        {
            return i >= 0 && i < size && j >= 0 && j < size;
        }
    }
    [DebuggerDisplay("Value = {Value}, X = {X}, Y = {Y}")]
    public class Neighbor<T>
    {
        public int X { get; set; }
        public int Y { get; set; }
        public T Value { get; set; }
    }
}
