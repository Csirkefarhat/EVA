using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Platform.Storage;
using Avalonia.Threading;
using Beadandó.ViewModels;
using Beadandó.Views;
using Project.Persistence;
using System;
using Project.Model;
using System.IO;
using System.Threading.Tasks;
using MsBox.Avalonia.Enums;
using MsBox.Avalonia;

namespace Beadandó;

public partial class App : Application
{
    private MainViewModel? _mainViewModel;
    private DispatcherTimer? _timer;

    private TopLevel? TopLevel
    {
        get
        {
            return ApplicationLifetime switch
            {
                IClassicDesktopStyleApplicationLifetime desktop => TopLevel.GetTopLevel(desktop.MainWindow),
                ISingleViewApplicationLifetime singleViewPlatform => TopLevel.GetTopLevel(singleViewPlatform.MainView),
                _ => null
            };
        }
    }
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        _mainViewModel = new MainViewModel();
        _mainViewModel.NewGame += OnNewGameStarted;
        _mainViewModel.ExitGame += OnExit;
        _mainViewModel.GameOver += OnGameOver;
        _mainViewModel.LoadGame += ViewModel_LoadGame;
        _mainViewModel.SaveGame += ViewModel_SaveGame;
        _mainViewModel.PauseMenu += ViewModel_PauseMenu;

        _timer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromMilliseconds(100)
        };
        _timer.Tick += new EventHandler(Timer_Tick);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = _mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = _mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private async void ViewModel_LoadGame(object? sender, System.EventArgs e)
    {
        if (TopLevel == null)
        {
            await MessageBoxManager.GetMessageBoxStandard(
                    "Aszteroidák játék betöltése",
                    "A fájlkezelés nem támogatott!",
                    ButtonEnum.Ok, Icon.Error)
                .ShowAsync();
            return;
        }

        // Start async operation to open the dialog.
        try
        {
            var files = await TopLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
            {
                Title = "Aszteroidák játék betöltése",
                AllowMultiple = false,
                FileTypeFilter = new[]
                {
                    new FilePickerFileType("Pálya")
                    {
                        Patterns = new[] { "*.stl" }
                    }
                }
            });

            if (files.Count > 0)
            {
                if (files[0] != null)
                {
                    _mainViewModel!.OpenFile(files[0]);

                }
            }
        }
        catch (DataException)
        {
            await MessageBoxManager.GetMessageBoxStandard(
                    "Aszteroida játék",
                    "A fájl betöltése sikertelen!",
                    ButtonEnum.Ok, Icon.Error)
                .ShowAsync();
        }
    }

    private void ViewModel_PauseMenu(object? sender, EventArgs e)
    {
        if (_timer!.IsEnabled) { _timer!.Stop(); }
        else { _timer!.Start(); }
    }

    private async void ViewModel_SaveGame(object? sender, EventArgs e)
    {
        if (TopLevel == null)
        {
            await MessageBoxManager.GetMessageBoxStandard(
                   "Aszteroidák játék mentése",
                   "A fájlkezelés nem támogatott!",
                   ButtonEnum.Ok, Icon.Error)
               .ShowAsync();
            return;
        }


        try
        {
            var file = await TopLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions()
            {
                Title = "Pálya mentése",
                FileTypeChoices = new[]
                {
                    new FilePickerFileType("Pálya")
                    {
                        Patterns = new[] { "*.stl" }
                    }
                }
            });

            if (file != null)
            {
                _mainViewModel!.SaveFile(file);

            }
        }
        catch (Exception ex)
        {
            await MessageBoxManager.GetMessageBoxStandard(
                    "Aszteroida játék",
                    "A fájl mentése sikertelen!" + ex.Message,
                    ButtonEnum.Ok, Icon.Error)
                .ShowAsync();
        }
    }

    public void Timer_Tick(object? sender, EventArgs e)
    {
        _mainViewModel!.Tick();
    }

    private void OnNewGameStarted(object? sender, EventArgs e)
    {
        _timer!.Start();
    }

    private void OnExit(object? sender, EventArgs e)
    {
        _timer!.Stop();
        Environment.Exit(0);
    }

    private async void OnGameOver(object? sender, String e)
    {
        _timer?.Stop();
        await MessageBoxManager.GetMessageBoxStandard(
                "Kilépés",
                "Game over, your time: " + e
                )
            .ShowAsync();
        Environment.Exit(0);
    }
}
