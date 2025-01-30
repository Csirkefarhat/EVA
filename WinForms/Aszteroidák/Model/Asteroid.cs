using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aszteroidák.Model
{
    public class Asteroid
    {
        private (int, int) Position;

        public (int, int) GetPosition { get { return Position; } }

        public void SetPosition ((int, int) Position) { this.Position = Position; }

        public Asteroid((int x, int y) initialPosition)
        {
            Position = initialPosition;
        }
    }
}
