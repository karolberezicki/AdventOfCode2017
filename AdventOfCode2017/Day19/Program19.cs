using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day19
{
    public class Program19
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            char[][] input = source.Split('\n').Select(c => c.ToCharArray()).ToArray();

            int currentY = 0;
            int currentX = 0;

            for (int i = 0; i < input[currentX].Length; i++)
            {
                if (input[0][i] == '|')
                {
                    currentY = i;
                }
            }

            bool[,] seen = new bool[input.Length, input.Length];

            Direction currentDirection = Direction.Down;

            List<char> foundLetters = new List<char>();

            int counter = 0;

            while (true)
            {
                counter++;

                seen[currentX, currentY] = true;
                if (char.IsLetterOrDigit(input[currentX][currentY]))
                {
                    foundLetters.Add(input[currentX][currentY]);
                }

                if (input[currentX][currentY] == '+')
                {
                    Neighbor<char> crossroads = GetNeighbors(currentX, currentY, input)
                        .FirstOrDefault(c => seen[c.X, c.Y] == false && c.Value != ' ')
                        ?? GetNeighbors(currentX, currentY, input).First(c => c.Value != ' ');

                    if (crossroads.X == currentX + 1)
                    {
                        currentDirection = Direction.Down;
                    }
                    if (crossroads.X == currentX - 1)
                    {
                        currentDirection = Direction.Up;
                    }
                    if (crossroads.Y == currentY + 1)
                    {
                        currentDirection = Direction.Right;
                    }
                    if (crossroads.Y == currentY - 1)
                    {
                        currentDirection = Direction.Left;
                    }

                    currentX = crossroads.X;
                    currentY = crossroads.Y;
                    continue;
                }

                switch (currentDirection)
                {
                    case Direction.Down:
                        currentX++;
                        break;
                    case Direction.Right:
                        currentY++;
                        break;
                    case Direction.Up:
                        currentX--;
                        break;
                    case Direction.Left:
                        currentY--;
                        break;
                }

                if (input[currentX][currentY] == ' ')
                {
                    break;
                }

            }

            Console.WriteLine($"Part one: {new string(foundLetters.ToArray())}");
            Console.WriteLine($"Part two: {counter}");

            Console.ReadKey();
        }

        public static HashSet<Neighbor<T>> GetNeighbors<T>(int i, int j, T[][] cellValues)
        {
            int size = cellValues.Length;

            HashSet<Neighbor<T>> neighbors = new HashSet<Neighbor<T>>();

            if (IsInsideArray(i, j, size))
            {
                if (IsInsideArray(i + 1, j, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i + 1][j], X = i + 1, Y = j });
                if (IsInsideArray(i - 1, j, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i - 1][j], X = i - 1, Y = j });
                if (IsInsideArray(i, j + 1, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i][j + 1], X = i, Y = j + 1 });
                if (IsInsideArray(i, j - 1, size))
                    neighbors.Add(new Neighbor<T> { Value = cellValues[i][j - 1], X = i, Y = j - 1 });
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

    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
    }
}
