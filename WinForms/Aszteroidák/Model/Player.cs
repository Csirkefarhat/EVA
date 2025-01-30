using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aszteroidák.Model
{
    public class Player
    {
        #region fields and properties
        public (int, int) Position { get; private set; }
        private int speed = 10;
        #endregion

        #region constructor
        public Player(int startX, int startY)
        {
            Position = (startX, startY);
        }
        #endregion


        public void MoveLeft()
        {
            Position = (Position.Item1 - speed, Position.Item2);
            if (Position.Item1 < 0) //ha kimenne a panelről
            {
                Position = (0, Position.Item2);
            }
        }


        public void MoveRight(int panelWidth)
        {
            Position = (Position.Item1 + speed, Position.Item2);
            if (Position.Item1 + 50 > panelWidth)  //ha kimennénk jobbra (player 50px nagyságú)
            {
                Position = (panelWidth - 50, Position.Item2);
            }
        }

        public void MoveUp()
        {
            Position = (Position.Item1, Position.Item2 - speed);
            if (Position.Item2 < 0)
            {
                Position = (Position.Item1, 0);
            }
        }


        public void MoveDown(int panelHeight)
        {
            Position = (Position.Item1, Position.Item2 + speed);
            if (Position.Item2 + 50 > panelHeight)
            {
                Position = (Position.Item1, panelHeight - 50);
            }
        }

        public bool CollidesWith(Asteroid asteroid)
        {

            int playerX = Position.Item1;
            int playerY = Position.Item2;
            int asteroidX = asteroid.GetPosition.Item1;
            int asteroidY = asteroid.GetPosition.Item2;

            //Mikor ütköznek
            bool collisionX = playerX <= asteroidX + 50 && playerX + 50 >= asteroidX;
            bool collisionY = playerY <= asteroidY + 50 && playerY + 50 >= asteroidY;

            //Akkor utköznek ha x és y koordináta is ütközik
            return collisionX && collisionY;
        }
    }
}
