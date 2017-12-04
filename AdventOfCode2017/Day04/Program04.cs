using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04
{
    public class Program04
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            var input = source.Split('\n').ToList();

            var partOne = input.Select(passphrase => passphrase.Split(' ')).Count(word => word.Length == word.Distinct().Count());

            Dictionary<string, bool> dictOfValidPassphrases = input.ToDictionary(c => c, c => true);
            foreach (string passphrase in input)
            {
                var words = passphrase.Split(' ');

                for (int i = 0; i < words.Length; i++)
                {
                    for (int j = 0; j < words.Length; j++)
                    {
                        if (i == j)
                        {
                            continue;
                        }

                        var a = words[i].ToList();
                        var b = words[j].ToList();
                        a.Sort();
                        b.Sort();

                        if (string.Join("", a) == string.Join("", b))
                        {
                            dictOfValidPassphrases[passphrase] = false;
                        }

                    }
                }
            }

            var partTwo = dictOfValidPassphrases.Count(c => c.Value);

            Console.WriteLine($"Part one: {partOne}");
            Console.WriteLine($"Part two: {partTwo}");

            Console.ReadKey();
        }
    }
}
