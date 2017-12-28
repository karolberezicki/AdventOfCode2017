using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day22
{
    public enum Direction
    {
        Down,
        Right,
        Up,
        Left
    }

    public class Program22
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            int partOne = PartOne(source);
            int partTwo = PartTwo(source);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static int PartOne(string source)
        {
            Dictionary<string, char> grid = CreateGrid(source);

            int countInfectiousBursts = 0;

            int currentY = 0;
            int currentX = 0;

            Direction currentDirection = Direction.Up;


            for (int i = 0; i < 10000; i++)
            {
                string coord = $"X{currentX}Y{currentY}";

                char node = grid.ContainsKey(coord) ? grid[coord] : '.';

                switch (node)
                {
                    case '#':
                        grid[coord] = '.';
                        currentDirection = TurnRight[currentDirection];
                        break;
                    default:
                        grid[coord] = '#';
                        currentDirection = TurnLeft[currentDirection];
                        countInfectiousBursts++;
                        break;
                }

                Move(currentDirection, ref currentX, ref currentY);
            }

            return countInfectiousBursts;
        }

        private static int PartTwo(string source)
        {
            Dictionary<string, char> grid = CreateGrid(source);

            int countInfectiousBursts = 0;

            int currentY = 0;
            int currentX = 0;

            Direction currentDirection = Direction.Up;


            for (int i = 0; i < 10000000; i++)
            {
                string coord = $"X{currentX}Y{currentY}";

                char node = grid.ContainsKey(coord) ? grid[coord] : '.';

                switch (node)
                {
                    case 'W':
                        grid[coord] = '#';
                        countInfectiousBursts++;
                        break;
                    case '#':
                        grid[coord] = 'F';
                        currentDirection = TurnRight[currentDirection];
                        break;
                    case 'F':
                        grid[coord] = '.';
                        currentDirection = Reverse[currentDirection];
                        break;
                    default:
                        grid[coord] = 'W';
                        currentDirection = TurnLeft[currentDirection];
                        break;
                }

                Move(currentDirection, ref currentX, ref currentY);
            }

            return countInfectiousBursts;
        }

        private static void Move(Direction currentDirection, ref int x, ref int y)
        {
            switch (currentDirection)
            {
                case Direction.Down:
                    y++;
                    break;
                case Direction.Right:
                    x++;
                    break;
                case Direction.Up:
                    y--;
                    break;
                case Direction.Left:
                    x--;
                    break;
            }
        }

        private static Dictionary<string, char> CreateGrid(string source)
        {
            List<string> input = source.Split('\n').ToList();

            Dictionary<string, char> grid = new Dictionary<string, char>();

            // Example
            //input = new List<string>
            //{
            //    "..#", "#..", "..."
            //};

            for (int i = 0; i < input.Count; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    int y = -input.Count + (input.Count / 2 + i) + 1;
                    int x = -input.Count + (input.Count / 2 + j) + 1;
                    string coord = $"X{x}Y{y}";
                    grid[coord] = input[i][j];
                }
            }

            return grid;
        }

        private static readonly Dictionary<Direction, Direction> TurnLeft = new Dictionary<Direction, Direction>
        {
            {Direction.Right, Direction.Up },
            {Direction.Up, Direction.Left },
            {Direction.Left, Direction.Down },
            {Direction.Down, Direction.Right }
        };

        private static readonly Dictionary<Direction, Direction> TurnRight = new Dictionary<Direction, Direction>
        {
            {Direction.Right, Direction.Down },
            {Direction.Up, Direction.Right },
            {Direction.Left, Direction.Up },
            {Direction.Down, Direction.Left }
        };

        private static readonly Dictionary<Direction, Direction> Reverse = new Dictionary<Direction, Direction>
        {
            {Direction.Right, Direction.Left },
            {Direction.Up, Direction.Down },
            {Direction.Left, Direction.Right },
            {Direction.Down, Direction.Up }
        };
    }
}
