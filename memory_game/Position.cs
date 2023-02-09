using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace memory_game
{
    public class Position
    {
        protected int _x;
        protected int _y;

        public int x { get => _x; set => _x = value; }
        public int y { get => _y; set => _y = value; }

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Position() { }
    }
}
