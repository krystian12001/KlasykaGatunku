using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KlasykaGatunku.MVVM.ViewModel;

namespace KlasykaGatunku.Utils
{
    public static class ValuesHelper
    {
        public static string databaseFileName = "KlasykaGatunku.accdb";
        public static string absolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseFileName);
        public static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + absolutePath + ";";

        public static Car GetCarByID(int carId)
        {
            Car car = null;

            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM Cars WHERE ID = ?";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("ID", carId);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
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

                            car = new Car(carIdDB, carBrand, carModel, carType, carFuel, carColor, carProductionYear, carMileage, carAvailability, carImagePath, carPriceCategory, carRegisterPlate);
                        }
                    }
                }
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
            }

            return car;
        }

        public static Customer GetCustomerByID(int customerId)
        {
            Customer customer = null;

            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM Customers WHERE ID = ?";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("ID", customerId);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int customerIdDB = Convert.ToInt32(reader["ID"].ToString());
                            string customerName = reader["Name"].ToString();
                            string customerSurname = reader["Surname"].ToString();
                            int customerPhoneNumber = Convert.ToInt32(reader["Phone Number"].ToString());
                            string customerEmail = reader["E-mail"].ToString();
                            bool customerRegularCustomer = Convert.ToBoolean(reader["Regular Customer"].ToString());

                            customer = new Customer(customerIdDB, customerName, customerSurname, customerPhoneNumber, customerEmail, customerRegularCustomer);
                        }
                    }
                }
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
            }

            return customer;
        }

        public static ObservableCollection<Car> GetCarsDB()
        {
            ObservableCollection<Car> Cars = new ObservableCollection<Car>();
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
            return Cars;
        }

        public static ObservableCollection<Customer> GetCustomersDB()
        {
            ObservableCollection<Customer> Customers = new ObservableCollection<Customer>();
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "SELECT * FROM Customers";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int customerIdDB = Convert.ToInt32(reader["ID"].ToString());
                            string customerName = reader["Name"].ToString();
                            string customerSurname = reader["Surname"].ToString();
                            int customerPhoneNumber = Convert.ToInt32(reader["Phone Number"].ToString());
                            string customerEmail = reader["E-mail"].ToString();
                            bool customerRegularCustomer = Convert.ToBoolean(reader["Regular Customer"].ToString());

                            Customer customer = new Customer
                            {
                                IdDB = customerIdDB,
                                Name = customerName,
                                Surname = customerSurname,
                                PhoneNumber = customerPhoneNumber,
                                Email = customerEmail,
                                RegularCustomer = customerRegularCustomer,
                                Regular = customerRegularCustomer ? "Yes" : "No",
                                IsSelected = false // Set initial value to false
                            };

                            Customers.Add(customer);
                        }
                    }
                }
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
            }
            return Customers;
        }

        public static bool ChangeAvailability(bool available, int id)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "UPDATE Cars SET Avaliable = @Avaliable WHERE ID = @id";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Avaliable", available);
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

        public static bool InsertRepairmentDB(Repairment repairment)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "INSERT INTO Damage ([Car ID], [Damage Description], [Fix Cost], [IsFixed], [Damage Date]) " +
                                   "VALUES (@CarId, @DamageDescription, @FixCost, @IsFixed, @DamageDate)";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@CarId", repairment.CarId);
                    command.Parameters.AddWithValue("@DamageDescription", repairment.DamageDescription);
                    command.Parameters.AddWithValue("@FixCost", repairment.FixCost);
                    command.Parameters.AddWithValue("@IsFixed", repairment.IsFixed);
                    command.Parameters.AddWithValue("@DamageDate", repairment.DamageDate);

                    connection.Open();
                    command.ExecuteNonQuery();

                    ValuesHelper.ChangeAvailability(repairment.IsFixed, repairment.CarId);

                    return true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Windows.MessageBox.Show("Error inserting repairment: " + ex.Message);
                return false;
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
                return false;
            }
        }

        public static bool RemoveRentalsByCarId(int carId)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    List<Rental> selectedRentals = new List<Rental>();

                    connection.Open();

                            string query = "DELETE FROM Rentals WHERE [Car ID] = @CarID";
                            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                            command.Parameters.AddWithValue("@CarID", carId);
                            command.ExecuteNonQuery();
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

        public static bool RemoveRentalsByCustomerId(int customerId)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    List<Rental> selectedRentals = new List<Rental>();

                    connection.Open();

                    string query = "DELETE FROM Rentals WHERE [Customer ID] = @CustomerID";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerID", customerId);
                    command.ExecuteNonQuery();
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

        public static bool RemoveRepairmentsByCarId(int carId)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    List<Rental> selectedRentals = new List<Rental>();

                    connection.Open();

                    string query = "DELETE FROM Damage WHERE [Car ID] = @CarID";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@CarID", carId);
                    command.ExecuteNonQuery();
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
    }
}
