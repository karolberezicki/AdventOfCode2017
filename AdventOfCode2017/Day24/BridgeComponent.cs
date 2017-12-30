using System.Diagnostics;

namespace Day24
{
    [DebuggerDisplay("{PortA}/{PortB} - {Number}")]
    public class BridgeComponent
    {
        public BridgeComponent(int portA, int portB, int number)
        {
            PortA = portA;
            PortB = portB;
            Number = number;
        }

        public int PortA { get; }
        public int PortB { get; }
        public int Number { get; }
    }
}