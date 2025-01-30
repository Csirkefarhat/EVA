using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System;
using Project.Model;
using Project.Persistence;

namespace Beadandó.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    #region Fields
    private readonly GameModel? _gameModel;
    private int _gridWidth;
    private int _gridHeight;

    private bool _mainMenuVisible;
    private bool _mainGameVisible;
    private bool _pauseMenuVisible;
    private bool _isAlive;

    #endregion

    #region Properties
    public int GridWidth
    {
        get => _gridWidth;
        set
        {
            _gridWidth = value;
            OnPropertyChanged(nameof(GridWidth));
        }
    }

    public int GridHeight
    {
        get => _gridHeight + 70;
        set
        {
            _gridHeight = value;
            OnPropertyChanged(nameof(GridHeight));
        }
    }

    public bool MainMenuVisible
    {
        get => _mainMenuVisible;
        set
        {
            _mainMenuVisible = value;
            OnPropertyChanged(nameof(MainMenuVisible));
        }
    }

    public bool MainGameVisible
    {
        get => _mainGameVisible;
        set
        {
            _mainGameVisible = value;
            OnPropertyChanged(nameof(MainGameVisible));
        }
    }

    public bool PauseMenuVisible
    {
        get => _pauseMenuVisible;
        set
        {
            _pauseMenuVisible = value;
            OnPropertyChanged(nameof(PauseMenuVisible));
        }
    }


    public ObservableCollection<ObservableAsteroid>? Asteroids { get; set; }

    public int PlayerX
    {
        get => _gameModel!.Player.X;
        set
        {
            _gameModel!.Player.X = value;
            OnPropertyChanged(nameof(PlayerX));
        }
    }

    public int PlayerY
    {
        get => _gameModel!.Player.Y;
        set
        {
            _gameModel!.Player!.Y = value;
            OnPropertyChanged(nameof(PlayerY));
        }
    }
    #endregion

    #region Commands & Events
    public RelayCommand NewGameCommand { get; private set; }
    public RelayCommand ExitCommand { get; private set; }

    public RelayCommand PauseMenuCommand { get; private set; }
    public RelayCommand PlayerUpCommand { get; private set; }
    public RelayCommand PlayerDownCommand { get; private set; }
    public RelayCommand PlayerLeftCommand { get; private set; }
    public RelayCommand PlayerRightCommand { get; private set; }

    public RelayCommand LoadGameCommand { get; private set; }
    public RelayCommand SaveGameCommand { get; private set; }

    public event EventHandler<string>? GameOver;
    public event EventHandler? ExitGame;
    public event EventHandler? NewGame;
    public event EventHandler? PauseMenu;
    public event EventHandler? LoadGame;
    public event EventHandler? SaveGame;

    #endregion

    #region Constructor
    public MainViewModel()
    {
        _gridWidth = 500;
        _gridHeight = 800;
        _isAlive = true;

        _gameModel = new GameModel(_gridWidth, _gridHeight, new TextFilePersistence());
        Asteroids = new ObservableCollection<ObservableAsteroid>();

        ExitCommand = new RelayCommand(OnExitButton);
        NewGameCommand = new RelayCommand(OnNewGame);
        PauseMenuCommand = new RelayCommand(OnPauseMenu);

        PlayerUpCommand = new RelayCommand(OnPlayerUp);
        PlayerDownCommand = new RelayCommand(OnPlayerDown);
        PlayerLeftCommand = new RelayCommand(OnPlayerLeft);
        PlayerRightCommand = new RelayCommand(OnPlayerRight);

        LoadGameCommand = new RelayCommand(OnLoadGame);
        SaveGameCommand = new RelayCommand(OnSaveGame);


        _gameModel.AsteroidCreated += new EventHandler<Asteroid>(Model_AsteroidCreated);
        _gameModel.AsteroidReMoved += new EventHandler<int>(Model_AsteroidReMoved);
        _gameModel.GameEnded += new EventHandler(Model_GameEnded);
        _gameModel.PlayerGotSaved += new EventHandler<Player>(Model_PlayerSaved);

        InitializeGame();
    }
    #endregion

    #region Public methods
    public void Tick()
    {
        _gameModel!.AdvanceGame();
        for (int i = Asteroids!.Count - 1; i >= 0; i--)
        {
            var asteroid = Asteroids[i];
            asteroid.Y += 3;
            _gameModel!.MoveAsteroid(i);
        }
    }

    public async void SaveFile(IStorageFile file)
    {
        using (var stream = await file.OpenWriteAsync())
        {
            _gameModel!.SaveGame(stream);
        }
        OnPauseMenu();
    }

    public async void OpenFile(IStorageFile file)
    {
        Asteroids!.Clear();
        using (var stream = await file.OpenReadAsync())
        {
            await _gameModel!.LoadGame(stream);
        }

        OnPauseMenu();
    }

    #endregion


    #region Private methods
    private void InitializeGame()
    {
        MainMenuVisible = true;
        MainGameVisible = false;
        PauseMenuVisible = false;
    }
    #endregion


    #region Event methods
    private void OnExit(string? e)
    {
        GameOver!.Invoke(this, e!);
        _isAlive = false;
    }

    private void OnExitButton()
    {
        ExitGame!.Invoke(this, EventArgs.Empty);
    }

    private void OnNewGame()
    {
        Asteroids!.Clear();
        MainGameVisible = true;
        MainMenuVisible = false;
        NewGame!.Invoke(this, EventArgs.Empty);
    }

    private void OnPlayerUp()
    {
        if (!_isAlive) { return; }
        _gameModel!.MovePlayerUp();
        PlayerX = _gameModel.Player.X;
        PlayerY = _gameModel.Player.Y;

    }

    private void OnPlayerDown()
    {
        if (!_isAlive) { return; }
        _gameModel!.MovePlayerDown();
        PlayerX = _gameModel.Player.X;
        PlayerY = _gameModel.Player.Y;
    }

    private void OnPlayerLeft()
    {
        if (!_isAlive) { return; }
        _gameModel!.MovePlayerLeft();
        PlayerX = _gameModel.Player.X;
        PlayerY = _gameModel.Player.Y;
    }

    private void OnPlayerRight()
    {
        if (!_isAlive) { return; }
        _gameModel!.MovePlayerRight();
        PlayerX = _gameModel.Player.X;
        PlayerY = _gameModel.Player.Y;
    }

    private void OnPauseMenu()
    {
        if (PauseMenuVisible) { PauseMenuVisible = false; }
        else { PauseMenuVisible = true; }
        PauseMenu?.Invoke(this, EventArgs.Empty);
    }


    private void OnLoadGame()
    {

        LoadGame!.Invoke(this, EventArgs.Empty);


    }

    private void OnSaveGame()
    {
        SaveGame!.Invoke(this, EventArgs.Empty);

    }
    #endregion

    #region Game event handlers
    private void Model_AsteroidCreated(object? sender, Asteroid e)
    {
        Asteroids!.Add(new(e));
    }

    private void Model_AsteroidReMoved(object? sender, int e)
    {
        Asteroids!.RemoveAt(e);
    }

    private void Model_GameEnded(object? sender, EventArgs e)
    {
        string showTime = _gameModel!.GameTime.ToString(@"hh\:mm\:ss");
        OnExit(showTime);
    }

    private void Model_PlayerSaved(object? sender, Player e)
    {
        PlayerX = e.X; PlayerY = e.Y;
    }
    #endregion
}
