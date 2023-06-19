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
using System.Globalization;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using KlasykaGatunku.MVVM.ViewModel;
using KlasykaGatunku.Utils;

namespace KlasykaGatunku.WindowsApp
{
    /// <summary>
    /// Interaction logic for AddRentalWindow.xaml
    /// </summary>
    public partial class AddRentalWindow : Window
    {
        public Rental Rental { get; private set; }

        private ObservableCollection<Car> Cars = ValuesHelper.GetCarsDB();

        private ObservableCollection<Customer> Customers = ValuesHelper.GetCustomersDB();

        private Customer selectedCustomer;

        private Car selectedCar;

        public AddRentalWindow()
        {
            InitializeComponent();

            var availableCars = Cars.Where(car => car.Available);
            carComboBox.ItemsSource = availableCars;
            carComboBox.DisplayMemberPath = "DisplayName";

            customerComboBox.ItemsSource = Customers;
            customerComboBox.DisplayMemberPath = "DisplayName";
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

        private void carComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (carComboBox.SelectedItem is Car selected)
            {
                selectedCar = selected;
            }
        }

        private void customerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (customerComboBox.SelectedItem is Customer selected)
            {
                selectedCustomer = selected;
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

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(StartingDateTextBox.Text) &&
            !string.IsNullOrWhiteSpace(EndingDateTextBox.Text) &&
            carComboBox.SelectedItem != null &&
            customerComboBox.SelectedItem != null)
            {
                if (checkDate(StartingDateTextBox.Text) && checkDate(EndingDateTextBox.Text))
                {
                    if (IsValidFormat(StartingDateTextBox.Text) && IsValidFormat(EndingDateTextBox.Text))
                    {
                        if (DateTime.Parse(StartingDateTextBox.Text) > DateTime.Parse(EndingDateTextBox.Text))
                        {
                            CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                            messageBox.Message = "Rental starting date cannot be bigger than ending date";

                            bool? result = messageBox.ShowDialog();
                        }
                        else if (DateTime.Parse(StartingDateTextBox.Text) > DateTime.Today)
                        {
                            CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                            messageBox.Message = "Rental starting date cannot be upcoming day";

                            bool? result = messageBox.ShowDialog();
                        }
                        else
                        {
                            int customerId = selectedCustomer.IdDB;
                            string customerString = $"{selectedCustomer.Name} {selectedCustomer.Surname}";
                            int carId = selectedCar.IdDB;
                            string carString = $"{selectedCar.Brand} {selectedCar.Model} {selectedCar.RegisterPlate}";
                            string rentalStart = StartingDateTextBox.Text;
                            string rentalEnd = EndingDateTextBox.Text;
                            bool timesOut = (DateTime.Parse(EndingDateTextBox.Text) < DateTime.Today) ? true : false;

                            Rental = new Rental(0, customerId, customerString, carId, carString, rentalStart, rentalEnd, "", timesOut, false, false);

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

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
