using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Day20
{
    public class Program20
    {
        public static void Main(string[] args)
        {
            string source = File.ReadAllText(@"..\..\input.txt");
            source = source.Remove(source.Length - 1);

            int closestParticleToCenter = FindClosestParticleToCenter(source);
            int countOfNonCollidedParticles = CountNonCollidedParticles(source);

            Console.WriteLine($"Part one: {closestParticleToCenter}");
            Console.WriteLine($"Part two: {countOfNonCollidedParticles}");

            Debug.Assert(closestParticleToCenter == 258);
            Debug.Assert(countOfNonCollidedParticles == 707);

            Console.ReadKey();
        }

        private static int FindClosestParticleToCenter(string source)
        {
            List<Particle> particles = source.Split('\n').Select((c, i) => new Particle(c, i)).ToList();
            return particles.First(p => p.AccelerationSum == particles.Select(a => a.AccelerationSum).Min()).Index;
        }

        private static int CountNonCollidedParticles(string source)
        {
            List<Particle> particles = source.Split('\n').Select((c, i) => new Particle(c, i)).ToList();
            HashSet<int> collided = new HashSet<int>();
            int notFoundCollisionsSince = 0;
            while (true)
            {
                int lastCollisionCount = collided.Count;

                foreach (Particle particle in particles)
                {
                    List<int> colidedParticles = particles.Where(p =>
                        particle.Index != p.Index
                        && !collided.Contains(particle.Index)
                        && particle.IsAtSamePosition(p)).Select(c=> c.Index).ToList();

                    if (colidedParticles.Count <= 0)
                    {
                        continue;
                    }

                    collided.Add(particle.Index);
                    collided.UnionWith(colidedParticles);
                }

                if (lastCollisionCount == collided.Count)
                {
                    notFoundCollisionsSince++;
                }
                if (notFoundCollisionsSince > 50)
                {
                    break;
                }

                foreach (Particle particle in particles)
                {
                    particle.Move();
                }
            }

            return particles.Count - collided.Count;
        }
    }
}