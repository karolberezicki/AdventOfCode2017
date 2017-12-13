using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day13
{
    public class Program13
    {
        private static IReadOnlyDictionary<int, int> _input;
        private static int[] _keys;

        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            _input = source.Split('\n').Select(c => c.Split(':'))
                .ToDictionary(c => int.Parse(c[0].Trim()), c => int.Parse(c[1].Trim()));
            _keys = _input.Select(c => c.Key).ToArray();

            HashSet<int> caught = new HashSet<int>();

            Dictionary<int, int> firewallState = _input.ToDictionary(c => c.Key, c => 0);
            Dictionary<int, bool> firewallOscilatorState = _input.ToDictionary(c => c.Key, c => false);

            int picosecond = 0;

            while (_keys.Max() >= picosecond)
            {

                if (firewallState.ContainsKey(picosecond) && firewallState[picosecond] == 0)
                {
                    caught.Add(picosecond);
                }

                MoveThroughFirewall(firewallState, firewallOscilatorState);

                picosecond++;
            }

            int tripSeverity = caught.Sum(c => c * _input[c]);

            Console.WriteLine($"Part one: {tripSeverity}");

            firewallState = _input.ToDictionary(c => c.Key, c => 0);
            firewallOscilatorState = _input.ToDictionary(c => c.Key, c => false);

            int minDelayForPass;

            for (int delay = 0; ; delay++)
            {
                MoveThroughFirewall(firewallState, firewallOscilatorState);
                caught = CheckCaught(firewallState, firewallOscilatorState);

                if (caught.Count == 0)
                {
                    minDelayForPass = delay + 1;
                    break;
                }

            }

            Console.WriteLine($"Part two: {minDelayForPass}");
            Console.ReadKey();
        }

        private static HashSet<int> CheckCaught(Dictionary<int, int> firewallState, Dictionary<int, bool> firewallOscilatorState)
        {
            var firewallState2 = firewallState.ToDictionary(c => c.Key, c => c.Value);
            var firewallOscilatorState2 = firewallOscilatorState.ToDictionary(c => c.Key, c => c.Value);

            HashSet<int> caught = new HashSet<int>();

            var picosecond = 0;

            while (_keys.Max() >= picosecond)
            {
                if (firewallState2.ContainsKey(picosecond) && firewallState2[picosecond] == 0)
                {
                    caught.Add(picosecond);
                }

                MoveThroughFirewall(firewallState2, firewallOscilatorState2);

                picosecond++;
            }

            return caught;
        }

        private static void MoveThroughFirewall(Dictionary<int, int> firewallState, Dictionary<int, bool> firewallOscilatorState)
        {
            foreach (var key in _keys)
            {
                if (firewallState[key] == _input[key] - 1)
                {
                    firewallOscilatorState[key] = true;
                }
                if (firewallState[key] == 0)
                {
                    firewallOscilatorState[key] = false;
                }

                if (firewallOscilatorState[key])
                {
                    firewallState[key] = firewallState[key] - 1;
                }
                else
                {
                    firewallState[key] = firewallState[key] + 1;
                }
            }
        }
    }
}
