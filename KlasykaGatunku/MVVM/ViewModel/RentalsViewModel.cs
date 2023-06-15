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
    public class Rental : ObservableObject
    {
        public int IdDB { get; set; }
        public int CustomerId { get; set; }
        public int CarId { get; set; }
        public string RentalStart { get; set; }
        public string RentalEnd { get; set; }
        public bool IsDone { get; set; }
        public bool IsReturned { get; set; }
        public bool IsDamage { get; set; }
        public int DamageId { get; set; }
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
    }

    class RentalsViewModel
    {

        private void LoadRentalsDB()
        {
            Rentals = new ObservableCollection<Rental>();
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM Rentals";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int carId = Convert.ToInt32(reader["Car ID"].ToString());
                            int customerId = Convert.ToInt32(reader["Customer ID"].ToString());
                            string rentalStart = reader["Rental Start"].ToString();
                            string rentalEnd = reader["Rental End"].ToString();
                            bool isDone = Convert.ToBoolean(reader["Is Done"].ToString());
                            bool isReturned = Convert.ToBoolean(reader["Is Returned"].ToString());
                            bool isDamage = Convert.ToBoolean(reader["Is Damage"].ToString());
                            int damageId = Convert.ToInt32(reader["Damage ID"].ToString());


                            Rental rental = new Rental
                            {
                                CarId = carId,
                                CustomerId = customerId,
                                RentalStart = rentalStart,
                                RentalEnd = rentalEnd,
                                IsDone = isDone,
                                IsReturned = isReturned,
                                IsDamage = isDamage,
                                DamageId = damageId,
                                IsSelected = false // Set initial value to false
                            };

                            Rentals.Add(rental);
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

        private bool InsertRentalDB(Rental rental)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    string query = "INSERT INTO Rentals ([Car ID], [Customer ID], [Rental Start], [Rental End], [Is Done], [Is Returned], " +
                        "[Is Damage], [Damage ID]) VALUES (@CarId, @CustomerId, @RentalStart, @RentalEnd, @IsDone, @IsReturned, @IsDamage, @DamageId)";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerId", rental.CustomerId);
                    command.Parameters.AddWithValue("@CarId", rental.CarId);
                    command.Parameters.AddWithValue("@RentalStart", rental.RentalStart);
                    command.Parameters.AddWithValue("@RentalEnd", rental.RentalEnd);
                    command.Parameters.AddWithValue("@IsDone", rental.IsDone);
                    command.Parameters.AddWithValue("@IsReturned", rental.IsReturned);
                    command.Parameters.AddWithValue("@IsDamage", rental.IsDamage);
                    command.Parameters.AddWithValue("@DamageId", rental.DamageId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Windows.MessageBox.Show("Error inserting rental: " + ex.Message);
                return false;
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
                return false;
            }
        }

        private bool RemoveSelectedRentals()
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    List<Rental> selectedRentals = new List<Rental>();

                    connection.Open();

                    foreach (Rental rental in Rentals)
                    {
                        if (rental.IsSelected)
                        {
                            string query = "DELETE FROM Rentals WHERE [ID] = @Rental.IdDB";
                            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                            command.Parameters.AddWithValue("@Customer.IdDB", rental.CarId);
                            command.ExecuteNonQuery();
                            selectedRentals.Add(rental);
                        }
                    }

                    foreach (Rental selectedRental in selectedRentals)
                    {
                        Rentals.Remove(selectedRental);
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

        private bool UpdateRentalDB(Rental rental, int Id)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    string query = "UPDATE Rentals SET [Car ID] = @CarId, [Customer ID] = @CustomerId, [Rental Start] = @RentalStart," +
                                   "[Rental End] = @RentalEnd, [Is Done] = @IsDone, [Is Returned] = @IsReturned," +
                                   "[Is Damage] = @IsDamage, [Damage ID] = @DamageId WHERE [ID] = @IdDB";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@RentalStart", rental.RentalStart);
                    command.Parameters.AddWithValue("@RentalEnd", rental.RentalEnd);
                    command.Parameters.AddWithValue("@IsDone", rental.IsDone);
                    command.Parameters.AddWithValue("@IsReturned", rental.IsReturned);
                    command.Parameters.AddWithValue("@IsDamage", rental.IsDamage);
                    command.Parameters.AddWithValue("@DamageId", rental.DamageId);
                    command.Parameters.AddWithValue("@CarId", rental.CarId);
                    command.Parameters.AddWithValue("@CustomerId", rental.CustomerId);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Windows.MessageBox.Show("Error updating rental: " + ex.Message);
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
