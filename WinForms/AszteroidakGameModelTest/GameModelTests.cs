using System.Numerics;
using System.Reflection;
using Aszteroidák.Model;
using Aszteroidák.Persistence;
using Moq;

namespace AszteroidakGameModelTest
{
    [TestClass]
    public class GameModelTests
    {
        private GameModel _model = null!;
        private Mock<IPersistence> _mockPersistence = null!;
        private List<Asteroid> _mockAsteroids = null!;
        private Player _mockPlayer = null!;

        [TestInitialize]
        public void Initialize()
        {
            _mockPersistence = new Mock<IPersistence>();
            _model = new GameModel(800, 600, _mockPersistence.Object);

            _model.AsteroidCreated += (s, e) => Assert.IsNotNull(e);
            _model.AsteroidMoved += (s, i) => Assert.IsTrue(i >= 0);
            _model.GameEnded += (s, e) => Assert.IsTrue(_model.Asteroids.Exists(a => _model.Player.CollidesWith(a)));
        }

        [TestMethod]
        public void AdvanceGame_CreatesAsteroids()
        {
            int initialAsteroidCount = _model.Asteroids.Count;

            for (int i = 0; i < 1000; i++)
            {
                _model.AdvanceGame();
            }

            Assert.IsTrue(_model.Asteroids.Count > initialAsteroidCount);
        }

        [TestMethod]
        public void MoveAsteroid()
        {
            if (_model.Asteroids.Count == 0)
            {
                _model.Asteroids.Add(new Asteroid((100, 0)));
            }
            _model.AdvanceGame();
            var initialPosition = _model.Asteroids[0].GetPosition;

            _model.MoveAsteroid(0);
            var newPosition = _model.Asteroids[0].GetPosition;

            Assert.AreEqual(initialPosition.Item1, newPosition.Item1); // sould be the same
            Assert.IsTrue(newPosition.Item2 > initialPosition.Item2); // should increase
        }

        [TestMethod]
        public void GameEndsWhenCollided()
        {
            var asteroid = new Asteroid((_model.Player.Position.Item1, _model.Player.Position.Item2)); //place it on the player
            _model.Asteroids.Add(asteroid);

            bool gameEnded = false;
            _model.GameEnded += (s, e) => gameEnded = true;

            _model.MoveAsteroid(0);

            Assert.IsTrue(gameEnded);
        }


        [TestMethod]
        public void Boundaries()
        {
            // Left
            for (int i = 0; i < 800; i++)
                _model.MovePlayerLeft();
            Assert.AreEqual(0, _model.Player.Position.Item1);

            // Right
            for (int i = 0; i < 800; i++)
                _model.MovePlayerRight();
            Assert.IsTrue(_model.Player.Position.Item1 <= 800);

            //Up
            for (int i = 0; i < 800; i++)
                _model.MovePlayerUp();
            Assert.AreEqual(0, _model.Player.Position.Item2);

            //Down
            for (int i = 0; i < 800; i++)
                _model.MovePlayerDown();
            
            Assert.IsTrue(_model.Player.Position.Item2 <= 800);
        }

        [TestMethod]
        public void PersistenceSave()
        {
            string savePath = "savefile.txt";
            _model.SaveGame(savePath);

            _mockPersistence.Verify(p => p.Save(savePath, It.IsAny<Game>()), Times.Once());
        }

        [TestMethod]
        public void PersistenceLoad()
        {
            _mockPlayer = new Player(50, 50);
            _mockAsteroids = [new Asteroid((100, 0)), new Asteroid((200, 50))];
            var mockGame = new Game();
            mockGame.SetPlayer(_mockPlayer);
            mockGame.SetAsteroids(_mockAsteroids);
            mockGame.SetGameTime(TimeSpan.FromMinutes(5));
            _mockPersistence.Setup(p => p.Load(It.IsAny<string>())).Returns(mockGame);

            string loadPath = "savefile.txt";
            _model.LoadGame(loadPath);

            Assert.AreEqual(_mockPlayer.Position, _model.Player.Position);
            Assert.AreEqual(_mockAsteroids.Count, _model.Asteroids.Count);
            Assert.AreEqual(TimeSpan.FromMinutes(5), _model.GameTime);

            _mockPersistence.Verify(p => p.Load(loadPath), Times.Once()); //only called once
        }
    }
}