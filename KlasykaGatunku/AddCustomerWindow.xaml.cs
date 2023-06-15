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
using KlasykaGatunku.MVVM.ViewModel;

namespace KlasykaGatunku
{
    /// <summary>
    /// Interaction logic for AddCustomerWindow.xaml
    /// </summary>
    public partial class AddCustomerWindow : Window
    {
        public Customer Customer { get; private set; }

        public AddCustomerWindow()
        {
            InitializeComponent();
        }

        private void nameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            if (textBox.Text.Length >= 19)
            {
                e.Handled = true;
            }
        }

        private void emailTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            if (textBox.Text.Length >= 35)
            {
                e.Handled = true;
            }
        }

        private void phoneNumberTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            if (!string.IsNullOrWhiteSpace(nameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(surnameTextBox.Text) &&
                !string.IsNullOrWhiteSpace(phoneNumberTextBox.Text) &&
                !string.IsNullOrWhiteSpace(emailTextBox.Text))
            {
                string name = nameTextBox.Text;
                string surname = surnameTextBox.Text;
                int phoneNumber = Convert.ToInt32(phoneNumberTextBox.Text);
                string email = emailTextBox.Text;
                bool regularCustomer = regularCustomerCheckBox.IsChecked ?? false;

                Customer = new Customer(0, name, surname, phoneNumber, email, regularCustomer);

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
