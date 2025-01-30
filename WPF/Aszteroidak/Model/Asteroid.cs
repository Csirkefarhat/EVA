using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aszteroidák.Model
{
    public class Asteroid
    {

        private int _x;
        private int _y;

        public int X
        {
            get => _x; set => _x = value;
        }

        public int Y
        {
            get => _y; set => _y = value;
        }
        public void SetPosition (int x, int y) { X = x; Y = y; }

        public Asteroid(int x, int y )
        {
            X = x;
            Y = y;
        }
    }
}
