using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using KlasykaGatunku.Utils;

namespace KlasykaGatunku.WindowsApp
{
    /// <summary>
    /// Interaction logic for EditRepairmentWindow.xaml
    /// </summary>
    public partial class EditRepairmentWindow : Window
    {
        public Repairment Repairment { get; private set; }

        private ObservableCollection<Car> Cars = ValuesHelper.GetCarsDB();

        private Car selectedCar;

        public EditRepairmentWindow()
        {
            InitializeComponent();

            foreach (Car car in Cars)
            {
                if (car.Available)
                {
                    carComboBox.Items.Add($"{car.Brand} {car.Model} {car.RegisterPlate}");
                }
            }
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

        private void dateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            int selectedIndex = carComboBox.SelectedIndex;
            if (selectedIndex >= 0 && selectedIndex < Cars.Count)
            {
                selectedCar = Cars[selectedIndex];
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
            if (!string.IsNullOrWhiteSpace(issueTextBox.Text) &&
            !string.IsNullOrWhiteSpace(costTextBox.Text) &&
            carComboBox.SelectedItem != null &&
            !string.IsNullOrWhiteSpace(dateTextBox.Text))
            {
                if (checkDate(dateTextBox.Text))
                {
                    if (IsValidFormat(dateTextBox.Text))
                    {
                        if (DateTime.Parse(dateTextBox.Text) > DateTime.Today)
                        {
                            CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                            messageBox.Message = "Damage couldn't have been done in future!";

                            bool? result = messageBox.ShowDialog();
                        }
                        else
                        {
                            int carId = selectedCar.IdDB;
                            string damageDescription = issueTextBox.Text;
                            int fixCost = Convert.ToInt32(costTextBox.Text);
                            bool isFixed = isFixedCheckBox.IsChecked ?? false;
                            string damageDate = dateTextBox.Text;

                            Repairment = new Repairment(0, carId, damageDescription, fixCost, damageDate, isFixed);

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
