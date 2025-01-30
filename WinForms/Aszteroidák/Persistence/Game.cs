using Aszteroidák.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aszteroidák.Persistence
{
    public class Game
    {
        private List<Asteroid>? asteroids;

        public List<Asteroid> GetAsteroids { get { return asteroids!; } }
        public void SetAsteroids (List<Asteroid> asteroids){ this.asteroids = asteroids; }
        public void AddAsteroid(Asteroid asteroid)
        {
            asteroids!.Add(asteroid);
        }


        private Player? player;
        public Player GetPlayer { get { return player!; } }
        public void SetPlayer(Player player) { this.player = player; }

        private TimeSpan gameTime;
        public TimeSpan GetGameTime { get {  return gameTime; } }

        public void SetGameTime(TimeSpan gameTime) {  this.gameTime = gameTime; }

    }
}


