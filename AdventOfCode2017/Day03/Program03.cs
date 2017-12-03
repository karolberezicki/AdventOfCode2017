using System;
using System.Collections.Generic;
using System.Linq;

namespace Day03
{
    public class Program03
    {
        public static void Main(string[] args)
        {
            int source = 265149;
            int partOne = PartOne(source);
            int partTwo = PartTwo(source);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static int PartOne(int source)
        {
            int n = 0;
            int columnValue;
            do
            {
                n++;
                columnValue = 4 * n * n - 5 * n + 2;
            } while (columnValue < source);

            n = n - 1;
            int columnValueAtSourceLevel = 4 * n * n - 5 * n + 2;
            return source - columnValueAtSourceLevel + n - 1;
        }

        public static int PartTwo(int input)
        {
            int[,] memoryGrid = new int[20, 20];

            int currentWallCount = 1;
            Direction currentDirection = Direction.Right;

            int x = 9;
            int y = 9;

            memoryGrid[x, y] = 1;

            var nextDirection = new Dictionary<Direction, Direction>
            {
                {Direction.Right, Direction.Up },
                {Direction.Up, Direction.Left },
                {Direction.Left, Direction.Down },
                {Direction.Down, Direction.Right }
            };

            while (true)
            {
                for (int i = 0; i < currentWallCount; i++)
                {
                    switch (currentDirection)
                    {
                        case Direction.Right:
                            ++x;
                            break;
                        case Direction.Up:
                            --y;
                            break;
                        case Direction.Left:
                            --x;
                            break;
                        case Direction.Down:
                            ++y;
                            break;
                    }

                    memoryGrid[x, y] = GetNeighbors(x, y, memoryGrid).Sum();

                    if (memoryGrid[x, y] > input)
                    {
                        return memoryGrid[x, y];
                    }
                }

                if (currentDirection == Direction.Up || currentDirection == Direction.Down)
                {
                    currentWallCount++;
                }

                currentDirection = nextDirection[currentDirection];
            }
        }

        public static List<T> GetNeighbors<T>(int i, int j, T[,] cellValues)
        {
            List<T> neighbors = new List<T>
            {
                cellValues[i + 1, j],
                cellValues[i - 1, j],
                cellValues[i, j + 1],
                cellValues[i, j - 1],
                cellValues[i - 1, j + 1],
                cellValues[i + 1, j - 1],
                cellValues[i + 1, j + 1],
                cellValues[i - 1, j - 1]
            };

            return neighbors;
        }
    }
    public enum Direction
    {
        Right,
        Up,
        Left,
        Down
    }
}