using System;

namespace GCodeDitherGenerator
{
    internal class Dot
    {
        public byte value;
        public int x;
        public int y;
        public bool used;
        public double xDeg;
        public double yDeg;

        public Dot(byte value, int x, int y)
        {
            this.value = value;
            this.x = x;
            this.y = y;
            used = false;
        }
    }
}