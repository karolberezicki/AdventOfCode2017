using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day10
{
    public class Program10
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            List<int> input = source.Split(',').Select(int.Parse).ToList();
            int partOne = PartOne(input);
        }

        private static int PartOne(IEnumerable<int> input)
        {
            List<int> list = Enumerable.Range(0, 256).ToList();

            int currentPosition = 0;
            int skipSize = 0;

            foreach (int sliceLenght in input)
            {

                List<int> subList = new List<int>();
                for (int i = currentPosition; i < currentPosition + sliceLenght; i++)
                {
                    int index = i % list.Count;

                    subList.Add(list[index]);
                }

                subList.Reverse();

                int sublistIndex = 0;
                for (int i = currentPosition; i < currentPosition + sliceLenght; i++)
                {
                    int index = i % list.Count;

                    list[index] = subList[sublistIndex];
                    sublistIndex++;
                }

                currentPosition += sliceLenght + skipSize;
                currentPosition = currentPosition % list.Count;

                skipSize++;
            }

            return list[0] * list[1];
        }
    }
}
