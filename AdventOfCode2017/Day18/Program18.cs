using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day18
{
    public class Program18
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            var input = source.Split('\n').Select(c => c.Split(' ').Select(d => d.Trim()).ToList()).ToList();

            Dictionary<string, long> registers = new Dictionary<string, long>();

            foreach (string s in input.Select(c => c[1]))
            {
                if (!registers.ContainsKey(s) && !int.TryParse(s, out int c))
                {
                    registers.Add(s, 0);
                }
            }

            long lastPlayedFrequency = PartOne(input, registers);

        }

        private static long PartOne(List<List<string>> input, Dictionary<string, long> registers)
        {
            long lastPlayedFrequency = 0;

            for (int i = 0; i < input.Count; i++)
            {
                var inst = input[i][0];
                long val;
                switch (inst)
                {
                    case "snd":
                        lastPlayedFrequency = registers[input[i][1]];
                        break;
                    case "set":
                        if (long.TryParse(input[i][2], out val))
                        {
                            registers[input[i][1]] = val;
                        }
                        else
                        {
                            registers[input[i][1]] = registers[input[i][2]];
                        }
                        break;
                    case "add":
                        if (long.TryParse(input[i][2], out val))
                        {
                            registers[input[i][1]] = registers[input[i][1]] + val;
                        }
                        else
                        {
                            registers[input[i][1]] = registers[input[i][1]] + registers[input[i][2]];
                        }
                        break;
                    case "mul":
                        if (long.TryParse(input[i][2], out val))
                        {
                            registers[input[i][1]] = registers[input[i][1]] * val;
                        }
                        else
                        {
                            registers[input[i][1]] = registers[input[i][1]] * registers[input[i][2]];
                        }
                        break;
                    case "mod":
                        if (long.TryParse(input[i][2], out val))
                        {
                            registers[input[i][1]] = registers[input[i][1]] % val;
                        }
                        else
                        {
                            registers[input[i][1]] = registers[input[i][1]] % registers[input[i][2]];
                        }
                        break;
                    case "rcv":
                        if (registers[input[i][1]] != 0)
                        {
                            registers[input[i][1]] = lastPlayedFrequency;
                            return lastPlayedFrequency;
                        }
                        break;
                    case "jgz":
                        if (!long.TryParse(input[i][1], out val))
                        {
                            val = registers[input[i][1]];
                        }
                        if (val > 0)
                        {
                            if (long.TryParse(input[i][2], out val))
                            {
                                i = (int)(i + val - 1);
                            }
                            else
                            {
                                i = (int)(i + registers[input[i][2]] - 1);
                            }
                        }
                        break;
                }
            }

            return lastPlayedFrequency;
        }
    }
}
