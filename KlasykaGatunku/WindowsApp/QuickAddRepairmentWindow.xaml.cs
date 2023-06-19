using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using KlasykaGatunku.MVVM.ViewModel;
using KlasykaGatunku.Utils;

namespace KlasykaGatunku.WindowsApp
{
    /// <summary>
    /// Interaction logic for QuickAddRepairmentWindow.xaml
    /// </summary>
    public partial class QuickAddRepairmentWindow : Window
    {
        public Repairment Repairment { get; private set; }

        public int carId;

        public QuickAddRepairmentWindow()
        {
            InitializeComponent();
        }

        private void issueTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (textBox.CaretIndex == 0 && !string.IsNullOrEmpty(e.Text))
            {
                string newText = e.Text.ToUpper();
                if (textBox.Text.Length > 1)
                {
                    textBox.Text = newText + textBox.Text.Substring(1);
                    textBox.CaretIndex = 1;
                }
                else
                {
                    textBox.Text = newText;
                    textBox.CaretIndex = textBox.Text.Length;
                }
                e.Handled = true;
            }
        }

        private void costTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
                return;
            }

            if (textBox.Text.Length >= 9)
            {
                e.Handled = true;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(issueTextBox.Text) &&
            !string.IsNullOrWhiteSpace(costTextBox.Text))
            {
                string damageDescription = issueTextBox.Text;
                int fixCost = Convert.ToInt32(costTextBox.Text);

                Repairment = new Repairment(0, carId, damageDescription, fixCost, DateTime.Today.ToString("d"), false);

                DialogResult = true;
            }
            else
            {
                CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                messageBox.Message = "Insert values";

                bool? result = messageBox.ShowDialog();
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}

