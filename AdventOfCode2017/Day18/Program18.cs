using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day18
{
    public class Program18
    {
        public static IReadOnlyList<List<string>> Input;

        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);
            Input = source.Split('\n').Select(c => c.Split(' ').Select(d => d.Trim()).ToList()).ToList();

            long lastPlayedFrequency = PartOne();
            long program1SendValueCount = PartTwo();

            Console.WriteLine($"Part one: {lastPlayedFrequency}");
            Console.WriteLine($"Part two: {program1SendValueCount}");

            Console.ReadKey();

        }

        private static long PartOne()
        {
            Dictionary<string, long> program0Registers = PrepareRegisters(0);
            Queue<long> queueForProgram0 = new Queue<long>();
            Queue<long> queueForProgram1 = new Queue<long>();
            long counter0 = 0;
            Run(program0Registers, queueForProgram0, queueForProgram1, ref counter0);

            return queueForProgram1.Last();
        }

        private static long PartTwo()
        {
            Dictionary<string, long> program0Registers = PrepareRegisters(0);
            Dictionary<string, long> program1Registers = PrepareRegisters(1);

            Queue<long> queueForProgram0 = new Queue<long>();
            Queue<long> queueForProgram1 = new Queue<long>();

            long counter0 = 0;
            long counter1 = 0;

            do
            {
                Run(program1Registers, queueForProgram1, queueForProgram0, ref counter1);
                Run(program0Registers, queueForProgram0, queueForProgram1, ref counter0);

            } while (queueForProgram1.Count > 0);
            return counter1;
        }

        private static void Run(IDictionary<string, long> registers, Queue<long> myQueue, Queue<long> otherQueue, ref long counter)
        {
            do
            {
                List<string> instruction = Input[(int)registers["ip"]];
                string inst = instruction[0];
                string arg = instruction[1];

                if (!long.TryParse(instruction.Last(), out long val))
                {
                    val = registers[instruction.Last()];
                }

                switch (inst)
                {
                    case "snd":
                        otherQueue.Enqueue(registers[arg]);
                        counter++;
                        break;
                    case "set":
                        registers[arg] = val;
                        break;
                    case "add":
                        registers[arg] += val;
                        break;
                    case "mul":
                        registers[arg] *= val;
                        break;
                    case "mod":
                        registers[arg] %= val;
                        break;
                    case "rcv":
                        if (myQueue.Count == 0)
                        {
                            return;
                        }
                        registers[arg] = myQueue.Dequeue();
                        break;
                    case "jgz":
                        if (!long.TryParse(arg, out long jumpCondition))
                        {
                            jumpCondition = registers[arg];
                        }
                        if (jumpCondition > 0)
                        {
                            registers["ip"] += val - 1;
                        }
                        break;
                }

                registers["ip"] += 1;
            } while (true);
        }

        private static Dictionary<string, long> PrepareRegisters(long programId)
        {
            Dictionary<string, long> registers = new Dictionary<string, long>();

            foreach (string s in Input.Select(c => c[1]).Where(c => c.All(d => !char.IsDigit(d))))
            {
                registers[s] = 0;
            }

            registers["ip"] = 0;
            registers["p"] = programId;

            return registers;
        }
    }

}
