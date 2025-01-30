using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Model
{
    public class Player
    {
        #region Fields

        private int _x;
        private int _y;
        private int _speed = 10;
        private int _width = 50;
        private int _height = 50;
        #endregion

        #region Properties
        public int Width
        {
            get => _width;
        }

        public int Height
        {
            get => _height;
        }



        public int X
        {
            get => _x; set => _x = value;
        }

        public int Y
        {
            get => _y; set => _y = value;
        }
        #endregion

        #region Constructor
        public Player(int startX, int startY)
        {
            X = startX;
            Y = startY;
        }
        #endregion

        #region Methods
        public void MoveLeft()
        {
            X -= _speed;
            if (X < 0)
            {
                X = 0;
            }
        }

        public void MoveRight(int panelWidth)
        {
            X += _speed;
            if (X + _width > panelWidth)
            {
                X = panelWidth - _width;
            }
        }

        public void MoveUp()
        {
            Y -= _speed;
            if (Y < 0)
            {
                Y = 0;
            }
        }


        public void MoveDown(int panelHeight)
        {
            Y += _speed;
            if (Y + _height > panelHeight)
            {
                Y = panelHeight - _height;
            }
        }

        public bool CollidesWith(Asteroid asteroid)
        {
            //Mikor ütköznek
            bool collisionX = X <= asteroid.X + 45 && X + 45 >= asteroid.X;
            bool collisionY = Y <= asteroid.Y + 35 && Y + 35 >= asteroid.Y;

            //Akkor utköznek ha x és y koordináta is ütközik
            return collisionX && collisionY;
        }
        #endregion
    }
}
