using Aszteroidák.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aszteroidák.Model
{
    public class GameModel
    {
        #region fields and properties

        private int panelWidth;
        private int panelHeight;

        private int tickCounter;
        private int spawnRate;

        private int fallSpeed;
        private IPersistence persistence;
        private Random random = new Random();


        public List<Asteroid> Asteroids { get; private set; }
        public Player Player { get; private set; }


        public TimeSpan GameTime { get; private set; }
        #endregion

        #region events
        public event EventHandler<Asteroid>? AsteroidCreated;
        public event EventHandler<Player>? PlayerGotSaved;
        public event EventHandler<int>? AsteroidMoved;
        public event EventHandler<int>? AsteroidReMoved;
        public event EventHandler? GameEnded;
        #endregion


        #region constructor
        public GameModel(int panelWidth, int panelHeight, IPersistence persistence)
        {
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;
            Asteroids = new List<Asteroid>();
            fallSpeed = 3;
            tickCounter = 0;
            spawnRate = 10;
            Player = new Player(panelWidth / 2, panelHeight - 50); // Űrhajó kezdeti pozíciója
            this.persistence = persistence;
        }
        #endregion

        public void AdvanceGame()
        {
            tickCounter++;

            if (tickCounter % 500 == 0 && spawnRate > 1) { spawnRate--; }
            if (random.Next(0, 11) >= spawnRate) { CreateAsteroid(); }

            GameTime += TimeSpan.FromSeconds(0.1); //interval 100 miatt
        }

        private void CreateAsteroid()
        {
            int xCord = random.Next(0, panelWidth - 50);
            Asteroid newAsteroid = new Asteroid((xCord, 0));
            Asteroids.Add(newAsteroid);
            Model_Invoke_AsteroidCreated(newAsteroid);   
        }


        public void MoveAsteroid(int i)
        {
            Asteroid asteroid = Asteroids[i];
            asteroid.SetPosition((asteroid.GetPosition.Item1, asteroid.GetPosition.Item2 + fallSpeed));
            //AsteroidMoved?.Invoke(this, i);
            Model_Invoke_AsteroidMoved(i);

            // Ellenőrizzük az ütközést
            if (Player.CollidesWith(asteroid))
            {
                //GameEnded?.Invoke(this, EventArgs.Empty);
                Model_Invoke_GameEnded();
            }

            // Ha elérte a képernyő alját
            if (Asteroids[i].GetPosition.Item2 > panelHeight)
            {
                Asteroids.RemoveAt(i);
                //AsteroidReMoved?.Invoke(this, i);
                Model_Invoke_AsteroidReMoved(i);
            }
        }

        #region moving
        public void MovePlayerLeft()
        {
            Player.MoveLeft();
        }

        public void MovePlayerRight()
        {
            Player.MoveRight(panelWidth);
        }

        public void MovePlayerUp()
        {
            Player.MoveUp();
        }

        public void MovePlayerDown()
        {
            Player.MoveDown(panelHeight);
        }
        #endregion

        public void LoadGame(String path)
        {
            Game values = persistence.Load(path);

            this.Player = values.GetPlayer!;
            this.GameTime = values.GetGameTime!;
            this.Asteroids = values.GetAsteroids!;
            //PlayerGotSaved?.Invoke(this, Player);
            Model_Invoke_PlayerGotSaved(Player);
            foreach (var a in Asteroids)
            {
                //AsteroidCreated?.Invoke(this, a);
                Model_Invoke_AsteroidCreated(a);
            }
        }

        public void SaveGame(String path)
        {
            Game game = new Game();
            game.SetPlayer(this.Player);
            game.SetAsteroids(this.Asteroids);
            game.SetGameTime(this.GameTime);
            persistence.Save(path, game);
        }

        #region invokes
        private void Model_Invoke_AsteroidCreated(Asteroid asteroid)
        {
            AsteroidCreated?.Invoke(this, asteroid);
        }

        private void Model_Invoke_AsteroidMoved(int i)
        {
            AsteroidMoved?.Invoke(this, i);
        }

        private void Model_Invoke_GameEnded()
        {
            GameEnded?.Invoke(this, EventArgs.Empty);
        }

        private void Model_Invoke_AsteroidReMoved(int i)
        {
            AsteroidReMoved?.Invoke(this, i);
        }

        private void Model_Invoke_PlayerGotSaved(Player player)
        {
            PlayerGotSaved?.Invoke(this, player);
        }

        #endregion
    }
}
