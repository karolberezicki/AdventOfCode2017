using System;
using System.IO;

namespace Day09
{
    public class Program09
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            int totalScore = 0;
            int currentScore = 1;
            bool garbage = false;
            int garbageCount = 0;

            for (int i = 0; i < source.Length; i++)
            {
                switch (source[i])
                {
                    case '!':
                        i = i + 1;
                        continue;
                    case '>':
                        garbageCount--;
                        garbage = false;
                        break;
                    case '<':
                        garbage = true;
                        break;
                }

                if (garbage)
                {
                    garbageCount++;
                    continue;
                }

                switch (source[i])
                {
                    case '{':
                        currentScore++;
                        break;
                    case '}':
                        currentScore--;
                        totalScore += currentScore;
                        break;
                }
            }

            Console.WriteLine($"Part one: {totalScore}");
            Console.WriteLine($"Part two: {garbageCount}");

            Console.ReadKey();

        }
    }
}
