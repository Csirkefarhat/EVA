using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project.Persistence;

namespace Project.Model
{
    public class GameModel
    {
        #region Fields

        private int panelWidth;
        private int panelHeight;

        private int tickCounter;
        private int spawnRate;
        private int fallSpeed;
        private IPersistence persistence;
        private Random random = new Random();


        private Player? player;
        private TimeSpan _gameTime;
        #endregion

        #region Properties
        public TimeSpan GameTime
        {
            get => _gameTime; set => _gameTime = value;
        }


        public List<Asteroid> Asteroids { get; private set; }
        public Player Player
        {
            get => player!; set => player = value;
        }
        #endregion


        #region Events
        public event EventHandler<Asteroid>? AsteroidCreated;
        public event EventHandler<Player>? PlayerGotSaved;
        public event EventHandler<int>? AsteroidMoved;
        public event EventHandler<int>? AsteroidReMoved;
        public event EventHandler? GameEnded;
        #endregion


        #region Constructor
        public GameModel(int panelWidth, int panelHeight, IPersistence persistence)
        {
            this.persistence = persistence;
            this.panelWidth = panelWidth;
            this.panelHeight = panelHeight;


            Asteroids = new List<Asteroid>();
            Player = new Player((panelWidth - 50) / 2, panelHeight - 50); // Űrhajó kezdeti pozíciója
            fallSpeed = 3;
            tickCounter = 0;
            spawnRate = 10;
        }
        #endregion

        #region Game methods
        public void AdvanceGame()
        {
            tickCounter++;

            if (tickCounter % 500 == 0 && spawnRate > 1) { spawnRate--; }
            if (random.Next(0, 11) >= spawnRate) { CreateAsteroid(); }

            _gameTime += TimeSpan.FromSeconds(0.1); //interval 100 miatt
        }

        private void CreateAsteroid()
        {
            int xCord = random.Next(0, panelWidth - 50);
            Asteroid newAsteroid = new Asteroid(xCord, 0);
            Asteroids.Add(newAsteroid);
            OnAsteroidCreated(newAsteroid);
        }


        public void MoveAsteroid(int i)
        {
            Asteroid asteroid = Asteroids[i];
            asteroid.SetPosition(asteroid.X, asteroid.Y + fallSpeed);
            OnAsteroidMoved(i);

            // Ellenőrizzük az ütközést
            if (Player.CollidesWith(asteroid))
            {
                OnGameEnded();
            }

            // Ha elérte a képernyő alját
            if (Asteroids[i].Y > panelHeight)
            {
                Asteroids.RemoveAt(i);
                OnAsteroidReMoved(i);
            }
        }

        #endregion

        #region Moving events
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


        #region Persistence
        public async Task LoadGame(Stream stream)
        {
            Game values = await persistence.Load(stream);

            this.Player = values.Player!;
            this._gameTime = values.GameTime!;
            this.Asteroids = values.Asteroids!;
            PlayerGotSaved?.Invoke(this, Player);
            OnPlayerGotSaved(player!);
            foreach (var a in Asteroids)
            {
                OnAsteroidCreated(a);
            }
        }

        public void SaveGame(Stream path)
        {
            Game game = new Game();
            game.Player = Player;
            game.Asteroids = Asteroids;
            game.GameTime = _gameTime;
            persistence.Save(path, game);
        }
        #endregion

        #region Event methods
        private void OnAsteroidCreated(Asteroid asteroid)
        {
            AsteroidCreated?.Invoke(this, asteroid);
        }

        private void OnAsteroidMoved(int i)
        {
            AsteroidMoved?.Invoke(this, i);
        }

        private void OnGameEnded()
        {
            GameEnded?.Invoke(this, EventArgs.Empty);
        }

        private void OnAsteroidReMoved(int i)
        {
            AsteroidReMoved?.Invoke(this, i);
        }

        private void OnPlayerGotSaved(Player player)
        {
            PlayerGotSaved?.Invoke(this, player);
        }

        #endregion
    }
}
