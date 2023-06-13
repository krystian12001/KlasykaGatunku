using KlasykaGatunku.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace KlasykaGatunku.MVVM.ViewModel
{
    public class Car : ObservableObject
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Fuel { get; set; }
        public string Color { get; set; }
        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public bool Availabile { get; set; }
        public string Availability { get; set; }
        public string ImagePath { get; set; }
        public int PriceCategory { get; set; }
        public string PriceCategorySign { get; set; }
        public string RegisterPlate { get; set; }
        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged();
                }
            }
        }
    }

    class CarsViewModel : ObservableObject 
    {
        public ObservableCollection<Car> Cars { get; set; }

        static string databaseFileName = "KlasykaGatunku.accdb";
        static string absolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseFileName);
        static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + absolutePath + ";";

        public CarsViewModel()
        {
            LoadCarsDB();
        }

        private ICommand _searchIconClickCommand;
        public ICommand SearchIconClickCommand
        {
            get
            {
                return _searchIconClickCommand ?? (_searchIconClickCommand = new RelayCommand(Search, (parameter) => true));
            }
        }

        private ICommand _addIconClickCommand;
        public ICommand AddIconClickCommand
        {
            get
            {
                return _addIconClickCommand ?? (_addIconClickCommand = new RelayCommand(Add, (parameter) => true));
            }
        }

        private ICommand _trashIconClickCommand;
        public ICommand TrashIconClickCommand
        {
            get
            {
                return _trashIconClickCommand ?? (_trashIconClickCommand = new RelayCommand(Remove, (parameter) => true));
            }
        }

        private ICommand _selectAllIconClickCommand;
        public ICommand SelectAllIconClickCommand
        {
            get
            {
                return _selectAllIconClickCommand ?? (_selectAllIconClickCommand = new RelayCommand(SelectAll, (parameter) => true));
            }
        }

        public void Search(object parameter)
        {
            MessageBox.Show("Your message here", "Title of the message box", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void Remove(object parameter)
        {
            MessageBox.Show("Your message here", "Title of the message box", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void Add(object parameter)
        {
            MessageBox.Show("Your message here", "Title of the message box", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        public void SelectAll(object parameter)
        {
            bool allSelected = true;
            foreach (Car car in Cars)
            {
                if (!car.IsSelected)
                {
                    allSelected = false;
                    break;
                }
            }

            if (allSelected)
            {
                foreach (Car car in Cars)
                {
                    car.IsSelected = false;
                }
            }
            else
            {
                foreach (Car car in Cars)
                {
                    car.IsSelected = true;
                }
            }

            OnPropertyChanged(nameof(Cars));
        }

        private void LoadCarsDB()
        {
            Cars = new ObservableCollection<Car>();
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM Cars";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string carBrand = reader["Brand"].ToString();
                            string carModel = reader["Model"].ToString();
                            string carType = reader["Type"].ToString();
                            string carFuel = reader["Fuel"].ToString();
                            string carColor = reader["Color"].ToString();
                            int carProductionYear = Convert.ToInt32(reader["Production Year"].ToString());
                            int carMileage = Convert.ToInt32(reader["Mileage"].ToString());
                            bool carAvailability = Convert.ToBoolean(reader["Avaliable"].ToString());
                            string carImagePath = reader["Image Path"].ToString();
                            int carPriceCategory = Convert.ToInt32(reader["Price Category"]);
                            string carRegisterPlate = reader["Register Plate"].ToString();

                            Car car = new Car
                            {
                                Brand = carBrand,
                                Model = carModel,
                                Type = carType,
                                Fuel = carFuel,
                                Color = carColor,
                                ProductionYear = carProductionYear,
                                Mileage = carMileage,
                                Availabile = carAvailability,
                                Availability = carAvailability ? "Available" : "Unavailable",
                                ImagePath = carImagePath,
                                PriceCategory = carPriceCategory,
                                PriceCategorySign = (carPriceCategory == 0) ? "☆☆☆☆☆" : (carPriceCategory == 1) ? "★☆☆☆☆" : (carPriceCategory == 2) ? "★★☆☆☆" : (carPriceCategory == 3) ? "★★★☆☆" : (carPriceCategory == 4) ? "★★★★☆" : "★★★★★",
                                RegisterPlate = carRegisterPlate,
                                IsSelected = false // Set initial value to false
                            };

                            Cars.Add(car);
                        }
                    }
                }
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
            }
        }
    }

}
