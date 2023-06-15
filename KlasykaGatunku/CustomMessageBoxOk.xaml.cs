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
using System.Windows.Shapes;

namespace KlasykaGatunku
{
    /// <summary>
    /// Interaction logic for CustomMessageBoxOk.xaml
    /// </summary>
    public partial class CustomMessageBoxOk : Window
    {
        public string Message { get; set; }
        public string Title { get; set; }

        public CustomMessageBoxOk()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
    
}
