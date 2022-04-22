using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Diver.Common;
using Diver.Pages.Images;

namespace Diver.Pages
{
    public class MenuItem
    {
        public string Icon { get; init; }
        public string Text { get; init; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentPresenter
    {
        private readonly NavigationManager _navigationManager;

        public MainWindow(NavigationManager navigationManager)
        {
            InitializeComponent();

            _navigationManager = navigationManager;
        }

        private void MovingWindow(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Maximize_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
            {
                WindowState = WindowState.Maximized;
            }
            else
            {
                WindowState = WindowState.Normal;
            }
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Menu_Selected(object sender, RoutedEventArgs e)
        {
            var menuItem = (sender as ListBoxItem).Content as MenuItem;

            if (menuItem.Text == "Home")
            {
                _navigationManager.Navigate<Home.Home>();
            }
            else if (menuItem.Text == "Images")
            {
                _navigationManager.Navigate<ImageList>();
            }
        }

        #region IContentPresenter
        public void SetContent(UserControl userControl)
        {
            ContentControl.Content = userControl;
        }
        #endregion
    }
}
