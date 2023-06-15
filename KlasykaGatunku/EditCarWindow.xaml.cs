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
    /// Interaction logic for EditCarWindow.xaml
    /// </summary>
    public partial class EditCarWindow : Window
    {
        public Car Car { get; private set; }

        public EditCarWindow()
        {
            InitializeComponent();
        }

        private void brandTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            if (textBox.Text.Length >= 14)
            {
                e.Handled = true;
            }
        }

        private void modelTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
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
            if (textBox.Text.Length >= 14)
            {
                e.Handled = true;
            }
        }

        private void productionYearTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
                return;
            }

            if (textBox.Text.Length >= 4)
            {
                e.Handled = true;
            }
        }

        private void mileageTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            
            if (!int.TryParse(e.Text, out _))
            {
                e.Handled = true;
                return;
            }

            if (textBox.Text.Length >= 7)
            {
                e.Handled = true; 
            }
        }

        private void registerPlateTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;

            if (!string.IsNullOrEmpty(e.Text))
            {
                string newText = e.Text.ToUpper();

                if (textBox.Text.Length + newText.Length <= 10)
                {
                    if (textBox.CaretIndex < textBox.Text.Length)
                    {
                        textBox.Text = textBox.Text.Insert(textBox.CaretIndex, newText);
                        textBox.CaretIndex++;
                    }
                    else
                    {
                        textBox.Text += newText;
                        textBox.CaretIndex = textBox.Text.Length;
                    }
                }

                e.Handled = true;
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(brandTextBox.Text) &&
                !string.IsNullOrWhiteSpace(modelTextBox.Text) &&
                typeComboBox.SelectedItem != null &&
                fuelComboBox.SelectedItem != null &&
                colorComboBox.SelectedItem != null &&
                !string.IsNullOrWhiteSpace(productionYearTextBox.Text) &&
                !string.IsNullOrWhiteSpace(mileageTextBox.Text) &&
                !string.IsNullOrWhiteSpace(registerplateTextBox.Text))
            {
                string brand = brandTextBox.Text;
                string model = modelTextBox.Text;
                string type = (typeComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string fuel = (fuelComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                string color = (colorComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
                int productionYear = Convert.ToInt32(productionYearTextBox.Text);
                int mileage = Convert.ToInt32(mileageTextBox.Text);
                bool available = availabilityCheckBox.IsChecked ?? false;
                string imagePath = "";
                int priceCategory = (priceCategoryComboBox.SelectedItem.ToString() == "☆☆☆☆☆") ? 0 :
                                        (priceCategoryComboBox.SelectedItem.ToString() == "★☆☆☆☆") ? 1 :
                                        (priceCategoryComboBox.SelectedItem.ToString() == "★★☆☆☆") ? 2 :
                                        (priceCategoryComboBox.SelectedItem.ToString() == "★★★☆☆") ? 3 :
                                        (priceCategoryComboBox.SelectedItem.ToString() == "★★★★☆") ? 4 : 5;
                string registerPlate = registerplateTextBox.Text;

                Car = new Car(0, brand, model, type, fuel, color, productionYear, mileage, available, imagePath, priceCategory, registerPlate);

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
