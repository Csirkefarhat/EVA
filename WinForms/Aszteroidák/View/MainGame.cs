using Aszteroidák.Model;
using Aszteroidák.Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aszteroidák.View
{
    public partial class MainGame : Form
    {
        private GameModel? model;
        private Panel? GamePanel;
        private Panel? playerPanel;
        private List<Panel>? asteroids;
        private System.Windows.Forms.Timer? gameTimer;
        private bool isPaused;

        #region constructor
        public MainGame()
        {
            InitializeComponent();

            model = new GameModel(ClientSize.Width, ClientSize.Height, new TextFilePersistence());

            model.AsteroidCreated += new EventHandler<Asteroid>(Model_AsteroidCreated);
            model.AsteroidMoved += new EventHandler<int>(Model_AsteroidMoved);
            model.GameEnded += new EventHandler(Model_GameEnded);
            model.AsteroidReMoved += new EventHandler<int>(Model_AsteriodReMoved);
            FormClosing += new FormClosingEventHandler(MainGame_FormClosing);
            model.PlayerGotSaved += new EventHandler<Player>(Model_PlayerSave);

            isPaused = false;
            KeyPreview = true;
            KeyDown += new KeyEventHandler(MainGame_KeyDown);

            InitializeGame();
        }
        #endregion

        private void InitializeGame()
        {
            GamePanel = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.DarkBlue,
            };
            Controls.Add(GamePanel);

            asteroids = new List<Panel>();

            // Játékos panel létrehozása
            playerPanel = new Panel
            {
                Size = new Size(50, 50),
                BackColor = Color.White,
                Location = new Point(ClientSize.Width / 2, ClientSize.Height - 50)
            };
            GamePanel.Controls.Add(playerPanel);

            // Timer beállítása
            gameTimer = new System.Windows.Forms.Timer
            {
                Interval = 100
            };
            gameTimer.Tick += GameTimer_Tick;

            gameTimer.Start();
        }

        private void GameTimer_Tick(object? sender, EventArgs e)
        {
            model!.AdvanceGame();


            for (int i = asteroids!.Count - 1; i >= 0; i--)
            {
                model!.MoveAsteroid(i);
            }
        }

        #region asteroidEvents
        private void Model_AsteroidCreated(object? sender, Asteroid e)
        {
            Panel? asteroidPanel = new Panel
            {
                Size = new Size(50, 50),
                BackColor = Color.Red,
                Location = new Point(e.GetPosition.Item1, e.GetPosition.Item2),
            };
            asteroids!.Add(asteroidPanel);
            GamePanel!.Controls.Add(asteroidPanel);
        }
        private void Model_AsteroidMoved(object? sender, int index)
        {
            asteroids![index].Location = new Point(model!.Asteroids[index].GetPosition.Item1, model!.Asteroids[index].GetPosition.Item2);
        }

        private void Model_AsteriodReMoved(object? sender, int index)
        {
            asteroids!.RemoveAt(index);
            GamePanel!.Controls.RemoveAt(index + 1);
        }
        #endregion

        #region KeyEvent
        private void MainGame_KeyDown(object? sender, KeyEventArgs e)
        {
            if (!isPaused)
            {
                if (e.KeyData == Keys.Left || e.KeyData == Keys.A)
                {
                    model!.MovePlayerLeft();
                    playerPanel!.Left = model.Player.Position.Item1;
                }
                else if (e.KeyData == Keys.Right || e.KeyData == Keys.D)
                {
                    model!.MovePlayerRight();
                    playerPanel!.Left = model.Player.Position.Item1;
                }
                else if (e.KeyData == Keys.Up || e.KeyData == Keys.W)
                {
                    model!.MovePlayerUp();
                    playerPanel!.Top = model.Player.Position.Item2;
                }
                else if (e.KeyData == Keys.Down || e.KeyData == Keys.S)
                {
                    model!.MovePlayerDown();
                    playerPanel!.Top = model.Player.Position.Item2;
                }
                else if (e.KeyData == Keys.Escape)
                {

                    PauseMenu pause = new PauseMenu(this);
                    pause.FormClosed += PauseMenu_Closed;
                    gameTimer!.Stop();
                    pause.Show();
                    isPaused = true;
                }
            }
        }
        #endregion
        private void PauseMenu_Closed(object? sender, FormClosedEventArgs e)
        {
            gameTimer!.Start();
            isPaused = false;
            ((Form)sender!).FormClosed -= PauseMenu_Closed;
        }


        private void MainGame_FormClosing(object? sender, FormClosingEventArgs e)
        {
            gameTimer!.Stop();
            MessageBox.Show($"Game Over! Játékidő: {model!.GameTime.TotalSeconds} másodperc", "Játék vége");
        }

        private void Model_GameEnded(object? sender, EventArgs e)
        {
            Close();
        }

        #region persistence
        public void SaveGame(String path)
        {
            model!.SaveGame(path);
        }

        public void LoadGame(String path)
        {
            GamePanel!.Controls.Clear();
            asteroids!.Clear();

            model!.LoadGame(path);

        }

        public void Model_PlayerSave(object? sender, Player e)
        {
            playerPanel!.Location = new Point(e.Position.Item1, e.Position.Item2);
            GamePanel!.Controls.Add(playerPanel);
        }
        #endregion
    }
}

