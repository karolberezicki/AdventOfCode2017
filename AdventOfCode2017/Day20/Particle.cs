using System;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Day20
{
    [DebuggerDisplay("{Display}")]
    public class Particle
    {
        private static readonly Regex IntRegex = new Regex(@"-?\d+");

        public Particle(string instruction, int index)
        {
            Index = index;

            MatchCollection match = IntRegex.Matches(instruction);
            Position = new Coordinates
            {
                X = int.Parse(match[0].Value),
                Y = int.Parse(match[1].Value),
                Z = int.Parse(match[2].Value)
            };
            Velocity = new Coordinates
            {
                X = int.Parse(match[3].Value),
                Y = int.Parse(match[4].Value),
                Z = int.Parse(match[5].Value)
            };
            Acceleration = new Coordinates
            {
                X = int.Parse(match[6].Value),
                Y = int.Parse(match[7].Value),
                Z = int.Parse(match[8].Value)
            };
        }

        public int Index { get; }

        public Coordinates Position { get; }
        public Coordinates Velocity { get; }
        public Coordinates Acceleration { get; }

        public string Display =>
            $"p=<{Position.X},{Position.Y},{Position.Z}>, v=<{Velocity.X},{Velocity.Y},{Velocity.Z}>, a=<{Acceleration.X},{Acceleration.Y},{Acceleration.Z}>";

        public bool IsAtSamePosition(Particle particle)
        {
            return Position.X == particle.Position.X && Position.Y == particle.Position.Y && Position.Z == particle.Position.Z;
        }

        public void Move()
        {
            Velocity.X += Acceleration.X;
            Velocity.Y += Acceleration.Y;
            Velocity.Z += Acceleration.Z;
            Position.X += Velocity.X;
            Position.Y += Velocity.Y;
            Position.Z += Velocity.Z;
        }

        public int AccelerationSum =>
            Math.Abs(Acceleration.X) + Math.Abs(Acceleration.Y) + Math.Abs(Acceleration.Z);
    }
}