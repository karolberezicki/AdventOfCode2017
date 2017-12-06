using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day06
{
    public class Program06
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<int> input = source.Split('\t').Select(int.Parse).ToList();
            //List<int> input = new List<int>() { 0, 2, 7, 0 }; // example

            HashSet<string> seen = new HashSet<string>();

            int partOne = 0;
            int partTwo;

            while (true)
            {
                string current = string.Join(" ", input);

                if (seen.Contains(current))
                {
                    partTwo = partOne - seen.ToList().IndexOf(current);
                    break;
                }

                seen.Add(current);

                int maxBlock = input.Max();
                int indexOfMaxBlock = input.IndexOf(maxBlock);

                input[indexOfMaxBlock] = 0;
                int index = indexOfMaxBlock;
                while (maxBlock > 0)
                {
                    index = index == input.Count - 1 ? 0 : index + 1;
                    input[index] = input[index] + 1;
                    maxBlock--;
                    indexOfMaxBlock++;
                }

                partOne++;
            }

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }
    }
}
