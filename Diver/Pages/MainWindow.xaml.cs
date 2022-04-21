using System.Windows;
using System.Windows.Input;
using Diver.Application.Images;
using Diver.Infrastructure.Repositories;
using Diver.Pages.Images;

namespace Diver.Pages
{
    public class MenuItem
    {
        public string Icon { get; set; }
        public string Text { get; set; }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            MenuList.ItemsSource = new[]
            {
                new MenuItem
                {
                    Icon = "/Resources/Icons/Home.png",
                    Text = "Home",
                },
                new MenuItem
                {
                    Icon = "/Resources/Icons/Cloud.png",
                    Text = "Images",
                },
            };
        }

        private void Navigate(object sender, MouseButtonEventArgs e)
        {
            var control = new ImageList()
            {
                DataContext = new ImageListViewModel(new ImageAppService(new ImageRepository())),
            };

            ContentControl = control;
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
    }
}
