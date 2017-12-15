using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day15
{
    public class Program15
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            string[] input = source.Split('\n');

            const long factorA = 16807;
            const long factorB = 48271;

            long generatorA = long.Parse(string.Join("", input[0].Select(c => c).Where(char.IsDigit)));
            long generatorB = long.Parse(string.Join("", input[1].Select(c => c).Where(char.IsDigit)));

            long judgeCountPartOne = 0;

            for (long i = 0; i < 40000000; i++)
            {
                generatorA = generatorA * factorA % 2147483647L;
                generatorB = generatorB * factorB % 2147483647L;

                if ((generatorA & 0xFFFF) == (generatorB & 0xFFFF))
                {
                    judgeCountPartOne++;
                }
            }

            List<long> valuesA = Generator(generatorA, factorA, 4).Take(5000000).ToList();
            List<long> valuesB = Generator(generatorB, factorB, 8).Take(5000000).ToList();

            long judgeCountPartTwo = 0;

            for (int i = 0; i < 5000000; i++)
            {
                if ((valuesA[i] & 0xFFFF) == (valuesB[i] & 0xFFFF))
                {
                    judgeCountPartTwo++;
                }
            }

            Console.WriteLine($"Part one: {judgeCountPartOne}");
            Console.WriteLine($"Part two: {judgeCountPartTwo}");

            Console.ReadKey();
        }

        public static IEnumerable<long> Generator(long startingValue, long factor, long criteria)
        {
            long generator = startingValue * factor % 2147483647L;
            while (true)
            {
                generator = generator * factor % 2147483647L;

                if (generator % criteria == 0)
                {
                    yield return generator;
                }
            }
        }
    }
}
