using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace KlasykaGatunku.WindowsApp
{
    /// <summary>
    /// Interaction logic for EditSinglePropertyWindow.xaml
    /// </summary>
    public partial class EditSinglePropertyWindow : Window
    {
        public string Property { get; set; }

        public string Case { get; set; }

        public string newProperty { get; private set; }

        public string argument { get; set; }

        public EditSinglePropertyWindow()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += EditSinglePropertyWindow_Loaded;
        }

        private void EditSinglePropertyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (Case == "Return Date")
            {
                propertyTextBox.PreviewTextInput += DateTextBox_PreviewTextInput;
            }
        }

        private void DateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (!Regex.IsMatch(textBox.Text, "^[0-9/]*$"))
            {
                e.Handled = true;
                return;
            }

            if (!int.TryParse(e.Text, out _) && e.Text != "/")
            {
                e.Handled = true;
                return;
            }

            if (textBox.Text.Length > 10)
            {
                e.Handled = true;
            }
        }

        public bool IsValidFormat(string input)
        {
            string pattern = @"^[0-9]{1,2}/[0-9]{1,2}/\d{4}$";
            return Regex.IsMatch(input, pattern);
        }

        public bool checkDate(string input)
        {
            string[] parts = input.Split('/');
            if (parts.Length == 3 && int.TryParse(parts[0], out int month) && int.TryParse(parts[1], out int day))
            {
                return month <= 12 && day <= 31;
            }
            else
            {
                return false;
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            if (Case == "Return Date")
            {
                if (!string.IsNullOrWhiteSpace(propertyTextBox.Text))
                {
                    if (checkDate(propertyTextBox.Text))
                    {
                        if (IsValidFormat(propertyTextBox.Text))
                        {
                            if (DateTime.Parse(propertyTextBox.Text) > DateTime.Today)
                            {
                                CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                                messageBox.Message = "Rental return date cannot be upcoming day";

                                bool? result = messageBox.ShowDialog();
                            }
                            else if (DateTime.Parse(propertyTextBox.Text) < DateTime.Parse(argument))
                            {
                                CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                                messageBox.Message = "Rental return date cannot be lesser than rental starting date";

                                bool? result = messageBox.ShowDialog();
                            }
                            else
                            {
                                newProperty = propertyTextBox.Text;

                                DialogResult = true;
                            }
                        }
                        else
                        {
                            CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                            messageBox.Message = "Invalid date format!";

                            bool? result = messageBox.ShowDialog();
                        }
                    }
                    else
                    {
                        CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                        messageBox.Message = "Invalid month and/or day!";

                        bool? result = messageBox.ShowDialog();
                    }
                }
                else
                {
                    CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                    messageBox.Message = "Insert values";

                    bool? result = messageBox.ShowDialog();
                }
            }
            else
            {
                DialogResult = true;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
