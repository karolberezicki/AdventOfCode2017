using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day02
{
    public class Program02
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<List<int>> spreadSheet = source.Split('\n').Select(c => c.Split('\t').Select(int.Parse).ToList()).ToList();

            int partOne =  spreadSheet.Select(c => c.Max() - c.Min()).Sum();
            int partTwo = spreadSheet.Select(RowChecksum).Sum();

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }

        private static int RowChecksum(List<int> row)
        {
            for (int i = 0; i < row.Count; i++)
            {
                for (int j = 0; j < row.Count; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }

                    if (row[i] > row[j] && row[i] % row[j] == 0)
                    {
                        return row[i] / row[j];
                    }
                }
            }
            throw new ArgumentException();
        }
    }
}
