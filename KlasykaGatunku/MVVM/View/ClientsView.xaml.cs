using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KlasykaGatunku.MVVM.View
{
    /// <summary>
    /// Interaction logic for ClientsView.xaml
    /// </summary>
    public partial class ClientsView : UserControl
    {
        public ClientsView()
        {
            InitializeComponent();
        }
        private void LineUpButton_Click(object sender, RoutedEventArgs e)
        {
            if (scrollBar.VerticalOffset > 0)
            {
                scrollBar.ScrollToVerticalOffset(scrollBar.VerticalOffset - 1);
            }
        }

        private void LineDownButton_Click(object sender, RoutedEventArgs e)
        {
            if (scrollBar.VerticalOffset < scrollBar.ExtentHeight - scrollBar.ViewportHeight)
            {
                scrollBar.ScrollToVerticalOffset(scrollBar.VerticalOffset + 1);
            }
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            ScrollViewer scrollViewer = FindVisualParent<ScrollViewer>((Thumb)sender);
            if (scrollViewer != null)
            {
                double verticalOffset = scrollViewer.VerticalOffset - e.VerticalChange;
                if (verticalOffset < 0)
                    verticalOffset = 0;
                if (verticalOffset > scrollViewer.ScrollableHeight)
                    verticalOffset = scrollViewer.ScrollableHeight;
                scrollViewer.ScrollToVerticalOffset(verticalOffset);
            }
        }

        private static T FindVisualParent<T>(UIElement element) where T : UIElement
        {
            UIElement parent = element;
            while (parent != null)
            {
                if (parent is T correctlyTyped)
                {
                    return correctlyTyped;
                }
                parent = VisualTreeHelper.GetParent(parent) as UIElement;
            }
            return null;
        }
    }
}
