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
using KlasykaGatunku.Core;
using KlasykaGatunku.WindowsApp;
using KlasykaGatunku.Utils;

namespace KlasykaGatunku.MVVM.ViewModel
{
    public class Car : ObservableObject
    {
        public int IdDB { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Type { get; set; }
        public string Fuel { get; set; }
        public string Color { get; set; }
        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public bool Available { get; set; }
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

        public Car()
        {
            IdDB = -1;
            Brand = "Fiat";
            Model = "Punto";
            Type = "Hatchback";
            Fuel = "Gas";
            Color = "Red";
            ProductionYear = 1999;
            Mileage = 222222;
            Available = true;
            Availability = "Available";
            ImagePath = "c:\twojastara";
            PriceCategory = 3;
            PriceCategorySign = GetPriceCategorySign(PriceCategory);
            RegisterPlate = "WY 12001";
            IsSelected = false;
        }

        public Car(int idDB, string brand, string model, string type, string fuel, string color, int productionYear, int mileage, bool available, string imagePath, int priceCategory, string registerPlate)
        {
            if (idDB == null)
            {
                IdDB = -1;
            }
            else
            {
                IdDB = idDB;
            }
            Brand = brand;
            Model = model;
            Type = type;
            Fuel = fuel;
            Color = color;
            ProductionYear = productionYear;
            Mileage = mileage;
            Available = available;
            Availability = available ? "Available" : "Unavailable";
            ImagePath = imagePath;
            PriceCategory = priceCategory;
            PriceCategorySign = GetPriceCategorySign(priceCategory);
            RegisterPlate = registerPlate;
            IsSelected = false;
        }

        public string GetPriceCategorySign(int priceCategory)
        {
            switch (priceCategory)
            {
                case 0:
                    return "☆☆☆☆☆";
                case 1:
                    return "★☆☆☆☆";
                case 2:
                    return "★★☆☆☆";
                case 3:
                    return "★★★☆☆";
                case 4:
                    return "★★★★☆";
                default:
                    return "★★★★★";
            }
        }

        public string DisplayName => $"{Brand} {Model} ({RegisterPlate})";
    }

    public class CarsViewModel : ObservableObject
    {
        private ObservableCollection<Car> cars;

        public ObservableCollection<Car> Cars
        {
            get { return cars; }
            set
            {
                cars = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Car> filteredCars;

        public ObservableCollection<Car> FilteredCars
        {
            get { return filteredCars; }
            set
            {
                filteredCars = value;
                OnPropertyChanged(nameof(FilteredCars));
            }
        }

        private string filterText;

        public string FilterText
        {
            get { return filterText; }
            set
            {
                filterText = value;
                OnPropertyChanged();
            }
        }

        //======//

        public CarsViewModel()
        {
            LoadCarsDB();
        }

        //======//

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

        private ICommand _editIconClickCommand;
        public ICommand EditIconClickCommand
        {
            get
            {
                return _editIconClickCommand ?? (_editIconClickCommand = new RelayCommand(Edit, (parameter) => true));
            }
        }

        //======//

        public void Search(object parameter)
        {
            LoadCarsDB();
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                FilteredCars = new ObservableCollection<Car>(Cars);
                return;
            }

            string searchText = FilterText.ToLower();
            FilteredCars = new ObservableCollection<Car>(Cars.Where(car =>
                car.Brand.ToLower().Contains(searchText) ||
                car.Model.ToLower().Contains(searchText) ||
                car.Type.ToLower().Contains(searchText) ||
                car.Color.ToLower().Contains(searchText)));

            Cars.Clear();
            foreach (Car car in FilteredCars)
            {
                Cars.Add(car);
            }
            OnPropertyChanged(nameof(Cars));
        }

        public void Remove(object parameter)
        {
            RemoveCars();
        }

        public void Add(object parameter)
        {
            AddCarWindow addCarWindow = new AddCarWindow();
            if (addCarWindow.ShowDialog() == true)
            {
                Car newCar = addCarWindow.Car;
                if (newCar != null)
                {
                    InsertCar(newCar);
                }
            }
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

        public void Edit(object parameter)
        {

            Car carToEdit = new Car();
            int id = 0;

            int selectedCount = 0;

            foreach (Car car in Cars)
            {
                if (car.IsSelected)
                {
                    selectedCount++;
                    id = car.IdDB;
                }
            }

            if (id <= 0 && selectedCount > 0)
            {
                CustomMessageBoxOk messageBoxNoneSelected = new CustomMessageBoxOk();
                messageBoxNoneSelected.Message = "Fatal error! Index out of range, contact software provider";
                bool? result = messageBoxNoneSelected.ShowDialog();
                return;
            }

            if (selectedCount == 0)
            {
                CustomMessageBoxOk messageBoxNoneSelected = new CustomMessageBoxOk();
                messageBoxNoneSelected.Message = "Select a car to modify";
                bool? result = messageBoxNoneSelected.ShowDialog();
            }
            else if (selectedCount == 1)
            {
                EditCarWindow editCarWindow = new EditCarWindow();
                if (editCarWindow.ShowDialog() == true)
                {
                    carToEdit = editCarWindow.Car;
                    if (UpdateCarDB(carToEdit, id))
                    {
                        Cars.Clear();
                        LoadCarsDB();
                        CustomMessageBoxOk messageBoxSucces = new CustomMessageBoxOk();
                        messageBoxSucces.Message = "Modified succesfully!";
                        bool? result = messageBoxSucces.ShowDialog();
                    }
                }
            }
            else
            {
                CustomMessageBoxOk messageBoxTooMany = new CustomMessageBoxOk();
                messageBoxTooMany.Message = "Selected to many cars!\nYou can only modify one car at once";
                bool? result = messageBoxTooMany.ShowDialog();
            }
        }

        //======//

        public void InsertCar(Car car)
        {
            if (InsertCarDB(car))
            {
                Cars.Clear();
                LoadCarsDB();
            }
        }

        public void RemoveCars()
        {
            bool anySelected = false;

            foreach (Car car in Cars)
            {
                anySelected = car.IsSelected;
                if (anySelected)
                {
                    break;
                }
            }

            if (anySelected)
            {
                CustomMessageBoxYesNo messageBox = new CustomMessageBoxYesNo();
                messageBox.Message = "Are you sure?";

                bool? result = messageBox.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    if (RemoveSelectedCars())
                    {
                        List<Car> storedCars = new List<Car>(Cars);
                        Cars.Clear();
                        foreach (Car car in storedCars)
                        {
                            Cars.Add(car);
                        }
                        OnPropertyChanged(nameof(Cars));
                    }
                }
            }
            else
            {
                CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                messageBox.Message = "Select a car to delete";

                bool? result = messageBox.ShowDialog();
            }
        }

        //======//

        private void LoadCarsDB()
        {
            Cars = new ObservableCollection<Car>();
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "SELECT * FROM Cars";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int carIdDB = Convert.ToInt32(reader["ID"].ToString());
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

                            Car car = new Car(carIdDB, carBrand, carModel, carType, carFuel, carColor, carProductionYear, carMileage, carAvailability, carImagePath, carPriceCategory, carRegisterPlate);

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

        private bool InsertCarDB(Car car)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "INSERT INTO Cars (Brand, Model, Type, Fuel, Color, [Production Year], Mileage, Avaliable, [Image Path], [Price Category], [Register Plate]) " +
                                   "VALUES (@Brand, @Model, @Type, @Fuel, @Color, @ProductionYear, @Mileage, @Avaliable, @ImagePath, @PriceCategory, @RegisterPlate)";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Brand", car.Brand);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@Type", car.Type);
                    command.Parameters.AddWithValue("@Fuel", car.Fuel);
                    command.Parameters.AddWithValue("@Color", car.Color);
                    command.Parameters.AddWithValue("@ProductionYear", car.ProductionYear);
                    command.Parameters.AddWithValue("@Mileage", car.Mileage);
                    command.Parameters.AddWithValue("@Avaliable", car.Available);
                    command.Parameters.AddWithValue("@ImagePath", car.ImagePath);
                    command.Parameters.AddWithValue("@PriceCategory", car.PriceCategory);
                    command.Parameters.AddWithValue("@RegisterPlate", car.RegisterPlate);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Windows.MessageBox.Show("Error inserting car: values don't match database's prefixes and/or input masks");
                return false;
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
                return false;
            }
        }

        private bool RemoveSelectedCars()
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    List<Car> selectedCars = new List<Car>();
                    
                    connection.Open();

                    foreach (Car car in Cars)
                    {
                        if (car.IsSelected)
                        {
                            string query = "DELETE FROM Cars WHERE ID = @CarId";
                            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                            command.Parameters.AddWithValue("@CarId", car.IdDB);
                            command.ExecuteNonQuery();
                            ValuesHelper.RemoveRentalsByCarId(car.IdDB);
                            ValuesHelper.RemoveRepairmentsByCarId(car.IdDB);
                            selectedCars.Add(car);
                        }
                    }

                    foreach (Car selectedCar in selectedCars)
                    {
                        Cars.Remove(selectedCar);
                    }
                    return true;
                }
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
                return false;
            }
        }

        private bool UpdateCarDB(Car car, int id)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "UPDATE Cars SET Brand = @Brand, Model = @Model, Type = @Type, Fuel = @Fuel, Color = @Color, " +
                                   "[Production Year] = @ProductionYear, Mileage = @Mileage, Avaliable = @Avaliable, " +
                                   "[Image Path] = @ImagePath, [Price Category] = @PriceCategory, [Register Plate] = @RegisterPlate " +
                                   "WHERE ID = @id";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Brand", car.Brand);
                    command.Parameters.AddWithValue("@Model", car.Model);
                    command.Parameters.AddWithValue("@Type", car.Type);
                    command.Parameters.AddWithValue("@Fuel", car.Fuel);
                    command.Parameters.AddWithValue("@Color", car.Color);
                    command.Parameters.AddWithValue("@ProductionYear", car.ProductionYear);
                    command.Parameters.AddWithValue("@Mileage", car.Mileage);
                    command.Parameters.AddWithValue("@Avaliable", car.Available);
                    command.Parameters.AddWithValue("@ImagePath", car.ImagePath);
                    command.Parameters.AddWithValue("@PriceCategory", car.PriceCategory);
                    command.Parameters.AddWithValue("@RegisterPlate", car.RegisterPlate);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Windows.MessageBox.Show("Error updating car: " + ex.Message);
                return false;
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
                return false;
            }
        }
    }
}
