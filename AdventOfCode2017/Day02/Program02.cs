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

            var partOne =  spreadSheet.Select(c => c.Max() - c.Min()).Sum();


        }
    }
}
