using KlasykaGatunku.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasykaGatunku.MVVM.ViewModel
{
    public class Customer
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool RegularCustomer { get; set; }
        public bool IsSelected { get; set; }
    }

    class ClientsViewModel : ObservableObject
    {
        public ObservableCollection<Customer> Customers { get; set; }

        static string databaseFileName = "KlasykaGatunku.accdb";
        static string absolutePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, databaseFileName);
        static string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + absolutePath + ";";

        public ClientsViewModel()
        {
            LoadClientsDB();
        }

        private void LoadClientsDB()
        {
            Customers = new ObservableCollection<Customer>();
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(connectionString))
                {
                    string query = "SELECT * FROM Customers";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string customerName = reader["Name"].ToString();
                            string customerSurname = reader["Surname"].ToString();
                            int customerPhoneNumber = Convert.ToInt32(reader["Phone Number"].ToString());
                            string customerEmail = reader["E-mail"].ToString();
                            bool customerRegularCustomer = Convert.ToBoolean(reader["Regular Customer"].ToString());

                            Customer customer = new Customer
                            {
                                Name = customerName,
                                Surname = customerSurname,
                                PhoneNumber = customerPhoneNumber,
                                Email = customerEmail,
                                RegularCustomer = customerRegularCustomer,
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
        }
    }
}
