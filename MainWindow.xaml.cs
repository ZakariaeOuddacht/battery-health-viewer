using Battery_Health_Viewer.Services;
using Battery_Health_Viewer.Views;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using WinRT.Interop;

namespace Battery_Health_Viewer
{
    public sealed partial class MainWindow : Window
    {
        private Menu? _menuPage;
        public Frame RootFrame => rootFrame;
        public MainWindow()
        {
            InitializeComponent();

            ExtendsContentIntoTitleBar = true;
            SetTitleBar(AppTitleBar);

            var appWindow = this.AppWindow;

            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            appWindow.TitleBar.ButtonBackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);
            appWindow.TitleBar.ButtonInactiveBackgroundColor = Windows.UI.Color.FromArgb(0, 0, 0, 0);

            RestoreWindowState();
            Closed += (_, _) => SaveWindowState();

            rootFrame.Navigate(typeof(Menu)); // Default page
            _menuPage = rootFrame.Content as Menu; // I hate when it warns me about nullable thingies >:(

            NavView.SelectedItem = NavView.MenuItems[0];
            ApplySavedTheme();
        }
        public void SetTheme(string theme)
        {
            var titleBar = AppWindow.TitleBar;

            ElementTheme target = theme switch
            {
                "Light" => ElementTheme.Light,
                "Dark" => ElementTheme.Dark,
                _ => ElementTheme.Default
            };

            RootLayout.RequestedTheme = target;

            if (target == ElementTheme.Light)
            {
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.Black;
                titleBar.ButtonHoverForegroundColor = Microsoft.UI.Colors.Black;
            }
            else
            {
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonHoverForegroundColor = Microsoft.UI.Colors.White;
            }
        }
        private void ApplySavedTheme()
        {
            var titleBar = AppWindow.TitleBar;

            ElementTheme theme = AppSettings.Theme switch
            {
                "Light" => ElementTheme.Light,
                "Dark" => ElementTheme.Dark,
                _ => ElementTheme.Default
            };

            RootLayout.RequestedTheme = theme;

            if (theme == ElementTheme.Light)
            {
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.Black;
                titleBar.ButtonHoverForegroundColor = Microsoft.UI.Colors.Black;
                titleBar.ButtonPressedForegroundColor = Microsoft.UI.Colors.Black;
            }
            else
            {
                titleBar.ButtonForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonHoverForegroundColor = Microsoft.UI.Colors.White;
                titleBar.ButtonPressedForegroundColor = Microsoft.UI.Colors.White;
            }
        }
        private void NavView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            if (args.IsSettingsSelected)
            {
                rootFrame.Navigate(typeof(Views.Settings));
                return;
            }

            if (args.SelectedItemContainer is NavigationViewItem item)
            {
                switch (item.Tag?.ToString())
                {
                    case "dashboard":
                        rootFrame.Navigate(typeof(Views.Menu));
                        break;
                }
            }
        }
        private void RestoreWindowState()
        {
            AppWindow.Resize(new Windows.Graphics.SizeInt32
            {
                Width = AppSettings.WindowWidth,
                Height = AppSettings.WindowHeight
            });

            if (AppSettings.WindowX >= 0 && AppSettings.WindowY >= 0)
            {
                AppWindow.Move(new Windows.Graphics.PointInt32(
                    AppSettings.WindowX,
                    AppSettings.WindowY));
            }

            if (AppSettings.WindowMaximized &&
                AppWindow.Presenter is OverlappedPresenter presenter)
            {
                presenter.Maximize();
            }
        }

        private void SaveWindowState()
        {
            if (AppWindow.Presenter is OverlappedPresenter presenter)
            {
                AppSettings.WindowMaximized =
                    presenter.State == OverlappedPresenterState.Maximized;
            }

            if (!AppSettings.WindowMaximized)
            {
                AppSettings.WindowWidth = AppWindow.Size.Width;
                AppSettings.WindowHeight = AppWindow.Size.Height;

                AppSettings.WindowX = AppWindow.Position.X;
                AppSettings.WindowY = AppWindow.Position.Y;
            }
        }
    }
}
