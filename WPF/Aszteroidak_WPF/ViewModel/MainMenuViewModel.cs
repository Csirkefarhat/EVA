using Aszteroidák.Model;
using Aszteroidák.Persistence;
using Aszteroidak_WPF.View;
using Microsoft.Windows.Themes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Aszteroidak_WPF.ViewModel
{
    public class MainMenuViewModel : ViewModelBase
    {
        #region Fields
        private bool _menuVisible;
        private bool _mainGameVisible;
        private bool _pauseGameVisible;
        private double _gridWidth;
        private double _gridHeight;


        private GameModel? _gameModel;
        #endregion


        #region Properties
        public ObservableCollection<ObservableAsteroid>? Asteroids { get; set; }


        public int PlayerX
        {
            get => _gameModel!.Player.X;
            set
            {
                _gameModel!.Player.X = value;
                OnPropertyChanged();
            }
        }

        public int PlayerY
        {
            get => _gameModel!.Player.Y;
            set
            {
                _gameModel!.Player.Y = value;
                OnPropertyChanged();
            }
        }

        public double GridWidth
        {
            get => _gridWidth;
            set
            {
                _gridWidth = value;
                OnPropertyChanged();
            }
        }

        public double GridHeight
        {
            get => _gridHeight;
            set
            {
                _gridHeight = value;
                OnPropertyChanged();
            }
        }
        public bool MainMenuVisible
        {
            get => _menuVisible;
            set
            {
                _menuVisible = value;
                OnPropertyChanged();
            }
        }

        public bool MainGameVisible
        {
            get => _mainGameVisible;
            set
            {
                _mainGameVisible = value;
                OnPropertyChanged();
            }
        }

        public bool PauseMenuVisible
        {
            get => _pauseGameVisible;
            set
            {
                _pauseGameVisible = value;
                OnPropertyChanged();
            }
        }
        #endregion


        #region Commands
        public DelegateCommand ExitCommand { get; private set; }
        public DelegateCommand NewGameCommand { get; private set; }

        public DelegateCommand PlayerUpCommand { get; private set; }
        public DelegateCommand PlayerDownCommand { get; private set; }
        public DelegateCommand PlayerLeftCommand { get; private set; }
        public DelegateCommand PlayerRightCommand { get; private set; }

        public DelegateCommand PauseMenuCommand {  get; private set; }

        public DelegateCommand LoadGameCommand { get; private set; }
        public DelegateCommand SaveGameCommand { get; private set; }
        #endregion


        #region Events
        public event EventHandler<String>? ExitGame;
        public event EventHandler? NewGame;
        public event EventHandler? PauseMenu;
        #endregion


        #region Constructor
        public MainMenuViewModel()
        {
            GridWidth = 800;
            GridHeight = 500;
            _gameModel = new GameModel((int)GridWidth, (int)GridHeight, new TextFilePersistence());

            ExitCommand = new DelegateCommand(param => OnExit(param as string));
            NewGameCommand = new DelegateCommand(param => OnNewGame());
            PlayerUpCommand = new DelegateCommand(param => OnPlayerUp());
            PlayerDownCommand = new DelegateCommand(param => OnPlayerDown());
            PlayerLeftCommand = new DelegateCommand(param => OnPlayerLeft());
            PlayerRightCommand = new DelegateCommand(param => OnPlayerRight());
            PauseMenuCommand = new DelegateCommand(param => OnPauseMenu());
            LoadGameCommand = new DelegateCommand(param => {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    OnLoadGame(openFileDialog.FileName);
                }
            });
            SaveGameCommand = new DelegateCommand(param => {
                Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
                if (saveFileDialog.ShowDialog() == true)
                {
                    OnSaveGame(saveFileDialog.FileName);
                }
            });

            _gameModel.AsteroidCreated += new EventHandler<Asteroid>(Model_AsteroidCreated);
            _gameModel.AsteroidReMoved += new EventHandler<int>(Model_AsteroidReMoved);
            _gameModel.GameEnded += new EventHandler(Model_GameEnded);
            _gameModel.PlayerGotSaved += new EventHandler<Player>(Model_PlayerGotSaved);

            InitializeGame();
        }
        #endregion


        #region Private events
        private void InitializeGame()
        {
            MainMenuVisible = true;
            MainGameVisible = false;
            PauseMenuVisible = false;
        }
        #endregion

        #region Public events
        public void Tick()
        {
            _gameModel!.AdvanceGame();

            for (int i = Asteroids!.Count - 1; i >= 0; i--)
            {
                var asteroid = Asteroids[i];
                asteroid.Y += 3;
                _gameModel!.MoveAsteroid(i);
            }

            OnPropertyChanged(nameof(Asteroids));
        }
        #endregion


        #region Game event handlers
        private void Model_GameEnded(object? sender, EventArgs e)
        {
            String showTime = _gameModel!.GameTime.ToString(@"hh\:mm\:ss");
            OnExit(showTime);
        }

        private void Model_AsteroidReMoved(object? sender, int e)
        {
            Asteroids!.RemoveAt(e);
        }

        private void Model_AsteroidCreated(object? sender, Asteroid e)
        {
            Asteroids!.Add(new(e));
        }

        private void Model_PlayerGotSaved(object? sender, Player e)
        {
            PlayerX = e.X;
            PlayerY = e.Y;
        }
        #endregion


        #region Event methods
        private void OnExit(string? e)
        {
            ExitGame?.Invoke(this, e!);
        }

        private void OnNewGame()
        {
            Asteroids = new();
            MainMenuVisible = false;
            MainGameVisible = true;
            PauseMenuVisible = false;

            NewGame?.Invoke(this, EventArgs.Empty);
        }


        private void OnPlayerUp()
        {
            _gameModel!.MovePlayerUp();
            PlayerX = _gameModel.Player.X;
            PlayerY = _gameModel.Player.Y;
        }

        private void OnPlayerDown()
        {
            _gameModel!.MovePlayerDown();
            PlayerX = _gameModel.Player.X;
            PlayerY = _gameModel.Player.Y;
        }

        private void OnPlayerLeft()
        {
            _gameModel!.MovePlayerLeft();
            PlayerX = _gameModel.Player.X;
            PlayerY = _gameModel.Player.Y;
        }

        private void OnPlayerRight()
        {
            _gameModel!.MovePlayerRight();
            PlayerX = _gameModel.Player.X;
            PlayerY = _gameModel.Player.Y;
        }

        private void OnPauseMenu()
        {
            if(PauseMenuVisible) { PauseMenuVisible = false; }
            else { PauseMenuVisible = true; }
            PauseMenu?.Invoke(this, EventArgs.Empty);
        }

        private void OnSaveGame(String path)
        {
            _gameModel!.SaveGame(path);
            OnPauseMenu();
        }

        private void OnLoadGame(String path)
        {
            Asteroids = [];
            _gameModel!.LoadGame(path);
            OnPauseMenu();
        }
        #endregion
    }
}
