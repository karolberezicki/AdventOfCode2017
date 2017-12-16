using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day16
{
    public class Program16
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            List<string> input = source.Split(',').ToList();

            const string programs = "abcdefghijklmnop";
            char[] programsArray = programs.ToCharArray();

            string partOne = new string(Dance(input, programsArray));

            programsArray = programs.ToCharArray();

            HashSet<string> seen = new HashSet<string>();

            int afterCycles = 0;

            for (int i = 0; i < 1000000000; i++)
            {
                programsArray = Dance(input, programsArray);

                string prog = new string(programsArray);

                if (seen.Contains(programs))
                {
                    afterCycles = 1000000000 % i;
                    break;
                }

                seen.Add(prog);
            }

            programsArray = programs.ToCharArray();

            for (int i = 0; i < afterCycles; i++)
            {
                programsArray = Dance(input, programsArray);
            }

            string partTwo = new string(programsArray);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static char[] Dance(IEnumerable<string> input, char[] programsArray)
        {
            foreach (string instruction in input)
            {
                char danceMove = instruction.First();
                string[] instructionParameters = new string(instruction.Skip(1).ToArray()).Split('/');

                switch (danceMove)
                {
                    case 's':
                        int spinSize = int.Parse(instructionParameters[0]);
                        programsArray = Spin(programsArray, spinSize);
                        break;
                    case 'x':
                        int a = int.Parse(instructionParameters[0]);
                        int b = int.Parse(instructionParameters[1]);
                        programsArray = Exchange(programsArray, a, b);
                        break;
                    case 'p':
                        char p1 = instructionParameters[0].First();
                        char p2 = instructionParameters[1].First();
                        programsArray = Partner(programsArray, p1, p2);
                        break;
                }
            }

            return programsArray;
        }

        public static char[] Spin(char[] array, int spinSize)
        {
            return array.Skip(array.Length - spinSize).Concat(array.Take(array.Length - spinSize)).ToArray();
        }

        public static char[] Exchange(char[] array, int a, int b)
        {
            char tmp = array[b];
            array[b] = array[a];
            array[a] = tmp;
            return array;
        }

        public static char[] Partner(char[] array, char a, char b)
        {
            return Exchange(array, Array.IndexOf(array, a), Array.IndexOf(array, b));
        }
    }
}
