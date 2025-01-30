using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Persistence
{
    public class Game
    {
        private List<Asteroid>? asteroids;

        public List<Asteroid>? Asteroids
        {
            get => asteroids;
            set => asteroids = value;
        }
        public void AddAsteroid(Asteroid asteroid)
        {
            asteroids!.Add(asteroid);
        }


        private Player? player;

        public Player? Player
        {
            get => player;
            set => player = value;
        }


        private TimeSpan gameTime;

        public TimeSpan GameTime
        {
            get => gameTime;
            set => gameTime = value;
        }

    }
}
