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
    public class Customer : ObservableObject
    {
        public int IdDB { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool RegularCustomer { get; set; }
        public string Regular { get; set; }
        public bool isSelected;
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

        public Customer()
        {
            IdDB = -1;
            Name = "Andrzej";
            Surname = "Duda";
            PhoneNumber = 213702115;
            Email = "wieczne.pioto@yahoo.com";
            RegularCustomer = true;
            Regular = "Yes";
            IsSelected = false;
        }

        public Customer(int idDB, string name, string surname, int phoneNumber, string email, bool regularCustomer)
        {
            if (idDB == null)
            {
                IdDB = -1;
            }
            else
            {
                IdDB = idDB;
            }
            Name = name;
            Surname = surname;
            PhoneNumber = phoneNumber;
            Email = email;
            RegularCustomer = regularCustomer;
            Regular = regularCustomer ? "Yes" : "No";
            IsSelected = false;
        }

        public string DisplayName => $"{Name} {Surname}";
    }

    public class ClientsViewModel : ObservableObject
    {
        private ObservableCollection<Customer> customers;

        public ObservableCollection<Customer> Customers
        {
            get { return customers; }
            set
            {
                customers = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Customer> filteredCustomers;

        public ObservableCollection<Customer> FilteredCustomers
        {
            get { return filteredCustomers; }
            set
            {
                filteredCustomers = value;
                OnPropertyChanged(nameof(FilteredCustomers));
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

        public ClientsViewModel()
        {
            LoadCustomersDB();
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

        private ICommand _editIconClickCommand;
        public ICommand EditIconClickCommand
        {
            get
            {
                return _editIconClickCommand ?? (_editIconClickCommand = new RelayCommand(Edit, (parameter) => true));
            }
        }

        public void Search(object parameter)
        {
            LoadCustomersDB();
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                FilteredCustomers = new ObservableCollection<Customer>(Customers);
                return;
            }

            string searchText = FilterText.ToLower();
            FilteredCustomers = new ObservableCollection<Customer>(Customers.Where(customer =>
                customer.Name.ToLower().Contains(searchText) ||
                customer.Surname.ToLower().Contains(searchText) ||
                customer.Email.ToLower().Contains(searchText)));

            Customers.Clear();
            foreach (Customer customer in FilteredCustomers)
            {
                Customers.Add(customer);
            }
            OnPropertyChanged(nameof(Customers));
        }

        public void Remove(object parameter)
        {
            RemoveCustomers();
        }

        public void Add(object parameter)
        {
            AddCustomerWindow addCustomerWindow = new AddCustomerWindow();
            if (addCustomerWindow.ShowDialog() == true)
            {
                Customer newCustomer = addCustomerWindow.Customer;
                if (newCustomer != null)
                {
                    InsertCustomer(newCustomer);
                }
            }
        }

        public void SelectAll(object parameter)
        {
            bool allSelected = true;
            foreach (Customer customer in Customers)
            {
                if (!customer.IsSelected)
                {
                    allSelected = false;
                    break;
                }
            }

            if (allSelected)
            {
                foreach (Customer customer in Customers)
                {
                    customer.IsSelected = false;
                }
            }
            else
            {
                foreach (Customer customer in Customers)
                {
                    customer.IsSelected = true;
                }
            }

            OnPropertyChanged(nameof(Customers));
        }

        public void Edit(object parameter)
        {

            Customer customerToEdit = new Customer();
            int id = 0;

            int selectedCount = 0;

            foreach (Customer customer in Customers)
            {
                if (customer.IsSelected)
                {
                    selectedCount++;
                    id = customer.IdDB;
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
                messageBoxNoneSelected.Message = "Select a customer to modify";
                bool? result = messageBoxNoneSelected.ShowDialog();
            }
            else if (selectedCount == 1)
            {
                EditCustomerWindow editCustomerWindow = new EditCustomerWindow();
                if (editCustomerWindow.ShowDialog() == true)
                {
                    customerToEdit = editCustomerWindow.Customer;
                    if (UpdateCustomerDB(customerToEdit, id))
                    {
                        Customers.Clear();
                        LoadCustomersDB();
                        CustomMessageBoxOk messageBoxSucces = new CustomMessageBoxOk();
                        messageBoxSucces.Message = "Modified succesfully!";
                        bool? result = messageBoxSucces.ShowDialog();
                    }
                }
            }
            else
            {
                CustomMessageBoxOk messageBoxTooMany = new CustomMessageBoxOk();
                messageBoxTooMany.Message = "Selected to many customers! You can only modify one customer at once";
                bool? result = messageBoxTooMany.ShowDialog();
            }
        }

        //======//

        public void InsertCustomer(Customer customer)
        {
            if (InsertCustomerDB(customer))
            {
                Customers.Clear();
                LoadCustomersDB();
            }
        }

        public void RemoveCustomers()
        {
            bool anySelected = false;

            foreach (Customer customer in Customers)
            {
                anySelected = customer.IsSelected;
                if (anySelected)
                {
                    break;
                }
            }

            if(anySelected)
            { 
                CustomMessageBoxYesNo messageBox = new CustomMessageBoxYesNo();
                messageBox.Message = "Are you sure?";

                bool? result = messageBox.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    if (RemoveSelectedCustomers())
                    {
                        List<Customer> storedCustomers = new List<Customer>(Customers);
                        Customers.Clear();
                        foreach (Customer customer in storedCustomers)
                        {
                            Customers.Add(customer);
                        }
                        OnPropertyChanged(nameof(Customers));
                    }
                }
            }
            else
            {
                CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                messageBox.Message = "Select a customer to delete";

                bool? result = messageBox.ShowDialog();
            }
        }

        //======//

        private void LoadCustomersDB()
        {
            Customers = new ObservableCollection<Customer>();
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
        }

        private bool InsertCustomerDB(Customer customer)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "INSERT INTO Customers (Name, Surname, [Phone Number], [E-mail], [Regular Customer]) " +
                                   "VALUES (@Name, @Surname, @PhoneNumber, @Email, @RegularCustomer)";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Surname", customer.Surname);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@RegularCustomer", customer.RegularCustomer);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Windows.MessageBox.Show("Error inserting customer: " + ex.Message);
                return false;
            }
            catch (System.InvalidOperationException ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
                System.Windows.MessageBox.Show("Please make sure the 'Microsoft.ACE.OLEDB.12.0' provider is installed on your machine.");
                return false;
            }
        }

        private bool RemoveSelectedCustomers()
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    List<Customer> selectedCustomers = new List<Customer>();

                    connection.Open();

                    foreach (Customer customer in Customers)
                    {
                        if (customer.IsSelected)
                        {
                            string query = "DELETE FROM Customers WHERE ID = @CustomerId";
                            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                            command.Parameters.AddWithValue("@CustomerId", customer.IdDB);
                            command.ExecuteNonQuery();
                            ValuesHelper.RemoveRentalsByCustomerId(customer.IdDB);
                            selectedCustomers.Add(customer);
                        }
                    }

                    foreach (Customer selectedCustomer in selectedCustomers)
                    {
                        Customers.Remove(selectedCustomer);
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

        private bool UpdateCustomerDB(Customer customer, int id)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "UPDATE Customers SET Name = @Name, Surname = @Surname, [Phone Number] = @PhoneNumber, " +
                                   "[E-mail] = @Email, [Regular Customer] = @RegularCustomer WHERE ID = @id";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@Name", customer.Name);
                    command.Parameters.AddWithValue("@Surname", customer.Surname);
                    command.Parameters.AddWithValue("@PhoneNumber", customer.PhoneNumber);
                    command.Parameters.AddWithValue("@Email", customer.Email);
                    command.Parameters.AddWithValue("@RegularCustomer", customer.RegularCustomer);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Windows.MessageBox.Show("Error updating customer: " + ex.Message);
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
