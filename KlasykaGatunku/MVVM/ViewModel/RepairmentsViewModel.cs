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
    public class Repairment : ObservableObject
    {
        public int IdDB { get; set; }
        public int CarId { get; set; }
        public string DamageDescription { get; set; }
        public int FixCost { get; set; }
        public bool IsFixed { get; set; }
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

        //public Car()
        //{
        //    IdDB = -1;
        //    Brand = "Fiat";
        //    Model = "Punto";
        //    Type = "Hatchback";
        //    Fuel = "Gas";
        //    Color = "Red";
        //    ProductionYear = 1999;
        //    Mileage = 222222;
        //    Available = true;
        //    Availability = "Available";
        //    ImagePath = "c:\twojastara";
        //    PriceCategory = 3;
        //    PriceCategorySign = GetPriceCategorySign(PriceCategory);
        //    RegisterPlate = "WY 12001";
        //    IsSelected = false;
        //}

        //public Car(int idDB, string brand, string model, string type, string fuel, string color, int productionYear, int mileage, bool available, string imagePath, int priceCategory, string registerPlate)
        //{
        //    if (idDB == null)
        //    {
        //        IdDB = -1;
        //    }
        //    else
        //    {
        //        IdDB = idDB;
        //    }
        //    Brand = brand;
        //    Model = model;
        //    Type = type;
        //    Fuel = fuel;
        //    Color = color;
        //    ProductionYear = productionYear;
        //    Mileage = mileage;
        //    Available = available;
        //    Availability = available ? "Available" : "Unavailable";
        //    ImagePath = imagePath;
        //    PriceCategory = priceCategory;
        //    PriceCategorySign = GetPriceCategorySign(priceCategory);
        //    RegisterPlate = registerPlate;
        //    IsSelected = false;
        //}

        //public string GetPriceCategorySign(int priceCategory)
        //{
        //    switch (priceCategory)
        //    {
        //        case 0:
        //            return "☆☆☆☆☆";
        //        case 1:
        //            return "★☆☆☆☆";
        //        case 2:
        //            return "★★☆☆☆";
        //        case 3:
        //            return "★★★☆☆";
        //        case 4:
        //            return "★★★★☆";
        //        default:
        //            return "★★★★★";
        //    }
        //}
    }

    class RepairmentsViewModel
    {

        //private void LoadRepairmentsDB()
        //{
        //    Repairments = new ObservableCollection<Repairment>();
        //    try
        //    {
        //        using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
        //        {
        //            string query = "SELECT * FROM Rentals";
        //            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);

        //            connection.Open();

        //            using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                {
        //                    int carId = Convert.ToInt32(reader["Car ID"].ToString());
        //                    string damageDescription = reader["Damage Description"].ToString();
        //                    int fixCost = Convert.ToInt32(reader["Fix Cost"].ToString());
        //                    bool isFixed = Convert.ToBoolean(reader["IsFixed"].ToString());

        //                    Repairment repairment = new Repairment
        //                    {
        //                        CarId = carId,
        //                        DamageDescription = damageDescription,
        //                        FixCost = fixCost,
        //                        IsFixed = isFixed,
        //                        IsSelected = false // Set initial value to false
        //                    };

        //                    Repairments.Add(repairment);
        //                }
        //            }
        //        }
        //    }
        //    catch (System.InvalidOperationException ex)
        //    {
        //        System.Windows.MessageBox.Show("Error: " + ex.Message);
        //        System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
        //    }
        //}

        //private bool InsertRepairmentDB(Repairment repairment)
        //{
        //    try
        //    {
        //        using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
        //        {
        //            string query = "INSERT INTO Repairments ([Car ID], [Damage Description], [Fix Cost], [IsFixed]) " +
        //                           "VALUES (@CarId, @DamageDescription, @FixCost, @IsFixed)";

        //            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
        //            command.Parameters.AddWithValue("@CarId", repairment.CarId);
        //            command.Parameters.AddWithValue("@DamageDescription", repairment.DamageDescription);
        //            command.Parameters.AddWithValue("@FixCost", repairment.FixCost);
        //            command.Parameters.AddWithValue("@IsFixed", repairment.IsFixed);

        //            connection.Open();
        //            command.ExecuteNonQuery();
        //            return true;
        //        }
        //    }
        //    catch (System.Data.OleDb.OleDbException ex)
        //    {
        //        System.Windows.MessageBox.Show("Error inserting repairment: " + ex.Message);
        //        return false;
        //    }
        //    catch (System.InvalidOperationException ex)
        //    {
        //        System.Windows.MessageBox.Show("Error: " + ex.Message);
        //        System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
        //        return false;
        //    }
        //}

        //private bool RemoveSelectedRepairments()
        //{
        //    try
        //    {
        //        using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
        //        {
        //            List<Repairment> selectedRepairments = new List<Repairment>();

        //            connection.Open();

        //            foreach (Repairment repairment in Repairments)
        //            {
        //                if (repairment.IsSelected)
        //                {
        //                    string query = "DELETE FROM Repairments WHERE [ID] = @IdDB";
        //                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
        //                    command.Parameters.AddWithValue("@IdDB", repairment.IdDB);
        //                    command.ExecuteNonQuery();
        //                    selectedRepairments.Add(repairment);
        //                }
        //            }

        //            foreach (Repairment selectedRepairment in selectedRepairments)
        //            {
        //                Repairments.Remove(selectedRepairment);
        //            }
        //            return true;
        //        }
        //    }
        //    catch (System.InvalidOperationException ex)
        //    {
        //        System.Windows.MessageBox.Show("Error: " + ex.Message);
        //        System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
        //        return false;
        //    }
        //}

        //private bool UpdateRepairmentDB(Repairment repairment, int Id)
        //{
        //    try
        //    {
        //        using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
        //        {
        //            string query = "UPDATE Repairments SET [CarId] = @CarId, [Damage Description] = @DamageDescription, " +
        //                           "[Fix Cost] = @FixCost, [IsFixed] = @IsFixed WHERE [ID] = @Id";

        //            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
        //            command.Parameters.AddWithValue("@DamageDescription", repairment.DamageDescription);
        //            command.Parameters.AddWithValue("@FixCost", repairment.FixCost);
        //            command.Parameters.AddWithValue("@IsFixed", repairment.IsFixed);
        //            command.Parameters.AddWithValue("@CarId", repairment.CarId);

        //            connection.Open();
        //            command.ExecuteNonQuery();
        //            return true;
        //        }
        //    }
        //    catch (System.Data.OleDb.OleDbException ex)
        //    {
        //        System.Windows.MessageBox.Show("Error updating repairment: " + ex.Message);
        //        return false;
        //    }
        //    catch (System.InvalidOperationException ex)
        //    {
        //        System.Windows.MessageBox.Show("Error: " + ex.Message);
        //        System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
        //        return false;
        //    }
        //}
    }
}
