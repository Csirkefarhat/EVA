using Aszteroidak_WPF.ViewModel;
using System.Configuration;
using System.Data;
using System.Windows;
using Aszteroidak_WPF.View;
using Aszteroidák.Model;
using Aszteroidák.Persistence;
using System.Windows.Threading;

namespace Aszteroidak_WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        #region Fields
        private MainMenu? _mainMenu = null; //view
        private MainMenuViewModel _mainMenuViewModel = null!; //viewModel
        private DispatcherTimer? _timer;
        #endregion

        #region constructor
        public App()
        {
            Startup += new StartupEventHandler(OnStartUp);
        }
        #endregion


        private void OnStartUp(Object sender, StartupEventArgs e)
        {
            //nezetmodel
            _mainMenuViewModel = new MainMenuViewModel();

            //nezet 
            _mainMenu = new MainMenu
            {
                DataContext = _mainMenuViewModel
            };

            //events
            _mainMenuViewModel.ExitGame += OnCloseApp;
            _mainMenuViewModel.NewGame += OnNewGameStarted;
            _mainMenuViewModel.PauseMenu += OnPauseMenu;

            _mainMenu.Show();

            //időzítő
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromMilliseconds(100)
            };
            _timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            _mainMenuViewModel.Tick();
        }

        private void OnNewGameStarted(object? sender, EventArgs e)
        {
            _timer!.Start();
        }

        private void OnPauseMenu(object? sender, EventArgs e)
        {
            if (_timer!.IsEnabled) { _timer.Stop(); }
            else { _timer!.Start(); }
            
        }

        private void OnCloseApp(object? sender, String e)
        {
            MessageBox.Show($"Game ended, game time {e}");
            _mainMenu?.Close();
        }
    }
}
