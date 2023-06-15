using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Runtime;
using System.Windows.Interop;

namespace KlasykaGatunku.WindowsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool isDragging;
        private Point dragStartPosition;

        public MainWindow()
        {
            InitializeComponent();
            this.MaxHeight = SystemParameters.MaximizedPrimaryScreenHeight;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.Source is Grid topBarGrid && topBarGrid.Name == "TopBarGrid")
            {
                WindowInteropHelper helper = new WindowInteropHelper(this);
                SendMessage(helper.Handle, 161, 2, 0);
                //DragMove();
            }
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(this);
                double deltaX = currentPosition.X - dragStartPosition.X;
                double deltaY = currentPosition.Y - dragStartPosition.Y;

                this.Left += deltaX;
                this.Top += deltaY;
            }
        }

        private void MinimizeAppButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ShortenExpandAppButton_Click(object sender, RoutedEventArgs e)
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

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            ShutdownApplication();
        }

        private void ShutdownApplication()
        {
            Application.Current.Shutdown();
        }
    }
}
