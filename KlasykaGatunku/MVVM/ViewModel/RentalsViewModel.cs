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
    public class Rental : ObservableObject
    {
        public int IdDB { get; set; }
        public int CustomerId { get; set; }
        public string CustomerString { get; set; }
        public int CarId { get; set; }
        public string CarString { get; set; }

        public string RentalStart { get; set; }
        public DateTime RentalStartDateTime { get; set; }
        private string rentalEnd { get; set; }
        public string RentalEnd
        {
            get { return rentalEnd; }
            set
            {
                if (rentalEnd != value)
                {
                    rentalEnd = value;
                    OnPropertyChanged();
                }
            }
        }

        public DateTime RentalEndDateTime { get; set; }
        public string RentalReturn { get; set; }
        public DateTime RentalReturnDateTime { get; set; }

        public bool IsDone { get; set; }
        public string Done { get; set; }
        private bool isReturned { get; set; }
        public bool IsReturned
        {
            get { return isReturned; }
            set
            {
                if (isReturned != value)
                {
                    isReturned = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Returned { get; set; }
        public bool IsDamage { get; set; }
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

        private int totalCost { get; set; }
        public int TotalCost
        {
            get { return totalCost; }
            set
            {
                if (totalCost != value)
                {
                    totalCost = value;
                    OnPropertyChanged();
                }
            }
        }

        public Rental()
        {
            IdDB = -1;
            CustomerId = 1;
            CustomerString = "Zenon Martyniuk";
            CarId = 1;
            CarString = "Fiat 126p WX RT4D";

            RentalStart = DateTime.Parse("6/6/2023").ToString("d");
            RentalStartDateTime = DateTime.Parse(RentalStart);
            RentalEnd = DateTime.Parse("6/16/2023").ToString("d");
            RentalEndDateTime = DateTime.Parse(RentalEnd);
            RentalReturn = DateTime.Parse("6/16/2023").ToString("d");
            if (RentalReturn != "")
            {
                RentalReturnDateTime = DateTime.Parse(RentalReturn);
            }

            IsDone = (RentalReturn != "") ? true : false;
            Done = IsDone ? "Yes" : "No";
            IsReturned = true;
            Returned = IsReturned ? "Yes" : "No";
            IsDamage = false;
            IsSelected = false;
        }

        public Rental(int idDB, int customerId, string customerString, int carId, string carString, string rentalStart, string rentalEnd, string rentalReturn, bool isDone, bool isReturned, bool isDamage)
        {
            if (idDB == null)
            {
                IdDB = -1;
            }
            else
            {
                IdDB = idDB;
            }
            IdDB = idDB;
            CustomerId = customerId;
            CustomerString = customerString;
            CarId = carId;
            CarString = carString;

            RentalStart = DateTime.Parse(rentalStart).ToString("d");
            RentalStartDateTime = DateTime.Parse(rentalStart);
            RentalEnd = DateTime.Parse(rentalEnd).ToString("d");
            RentalEndDateTime = DateTime.Parse(rentalEnd);
            if (rentalReturn != "")
            {
                RentalReturn = DateTime.Parse(rentalReturn).ToString("d");
            }
            else
            {
                RentalReturn = rentalReturn;
            }
            if (RentalReturn != "")
            {
                RentalReturnDateTime = DateTime.Parse(rentalReturn);
            }
            IsDone = isDone;
            Done = IsDone ? "Yes" : "No";
            IsReturned = isReturned;
            Returned = IsReturned ? "Yes" : "No";
            IsDamage = isDamage;
            IsSelected = false;
        }

        public Rental(int idDB, int customerId, int carId, string rentalStart, string rentalEnd, string rentalReturn, bool isDone, bool isReturned, bool isDamage)
        {
            if (idDB == null)
            {
                IdDB = -1;
            }
            else
            {
                IdDB = idDB;
            }
            IdDB = idDB;

            Car car = ValuesHelper.GetCarByID(carId);
            Customer customer = ValuesHelper.GetCustomerByID(customerId); 
            CustomerId = customerId;
            CustomerString = CarString = $"{car.Brand} {car.Model} {car.RegisterPlate}";
            CarId = carId;
            CustomerString = $"{customer.Name} {customer.Surname}";

            RentalStart = DateTime.Parse(rentalStart).ToString("d");
            RentalStartDateTime = DateTime.Parse(rentalStart);
            RentalEnd = DateTime.Parse(rentalEnd).ToString("d");
            RentalEndDateTime = DateTime.Parse(rentalEnd);
            RentalReturn = DateTime.Parse(rentalReturn).ToString("d");
            if (RentalReturn != "")
            {
                RentalReturnDateTime = DateTime.Parse(rentalReturn);
            }

            IsDone = isDone;
            Done = IsDone ? "Yes" : "No";
            IsReturned = isReturned;
            Returned = IsReturned ? "Yes" : "No";
            IsDamage = isDamage;
            IsSelected = false;
        }

        public void updateDates()
        {
            RentalStartDateTime = DateTime.Parse(RentalStart);
            RentalEndDateTime = DateTime.Parse(RentalEnd);
            if (RentalReturn != "")
            {
                RentalReturnDateTime = DateTime.Parse(RentalReturn);
            }
        }

        public int computeCost()
        {
            Car car = ValuesHelper.GetCarByID(CarId);

            TimeSpan timeDifferencea = RentalEndDateTime.Subtract(RentalStartDateTime);
            int daysDifferencea = timeDifferencea.Days;

            TimeSpan timeDifferenceb = RentalEndDateTime.Subtract(RentalStartDateTime);
            int daysDifferenceb = timeDifferenceb.Days;

            if (IsReturned)
            {
                if (RentalEndDateTime >= RentalReturnDateTime)
                {
                    
                    TimeSpan timeDifference = RentalEndDateTime.Subtract(RentalStartDateTime);
                    int daysDifference = timeDifference.Days;
                    return (car.PriceCategory * 150 * daysDifference);
                }
                else
                {
                    TimeSpan timeDifferenceNormal = RentalEndDateTime.Subtract(RentalStartDateTime);
                    int daysDifferenceNormal = timeDifferenceNormal.Days;

                    TimeSpan timeDifferenceExced = RentalReturnDateTime.Subtract(RentalEndDateTime);
                    int daysDifferenceExced = timeDifferenceExced.Days;

                    return (car.PriceCategory * 150 * daysDifferenceNormal) + (2 * (car.PriceCategory * 150 * daysDifferenceExced));
                }
            }
            else
            {
                if (RentalEndDateTime >= DateTime.Today)
                {

                    TimeSpan timeDifference = RentalEndDateTime.Subtract(RentalStartDateTime);
                    int daysDifference = timeDifference.Days;
                    return (car.PriceCategory * 150 * daysDifference);
                }
                else
                {
                    TimeSpan timeDifferenceNormal = RentalEndDateTime.Subtract(RentalStartDateTime);
                    int daysDifferenceNormal = timeDifferenceNormal.Days;

                    TimeSpan timeDifferenceExced = DateTime.Today.Subtract(RentalEndDateTime);
                    int daysDifferenceExced = timeDifferenceExced.Days;

                    return (car.PriceCategory * 150 * daysDifferenceNormal) + (2 * (car.PriceCategory * 150 * daysDifferenceExced));
                }
            }
        }
    }

    public class RentalsViewModel : ObservableObject
    {
        private ObservableCollection<Rental> rentals;

        public ObservableCollection<Rental> Rentals
        {
            get { return rentals; }
            set
            {
                rentals = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Rental> filteredRentals;

        public ObservableCollection<Rental> FilteredRentals
        {
            get { return filteredRentals; }
            set
            {
                filteredRentals = value;
                OnPropertyChanged(nameof(FilteredRentals));
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

        public RentalsViewModel()
        {
            LoadRentalsDB();
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

        private ICommand _finishRentalCommand;
        public ICommand FinishRentalCommand
        {
            get
            {
                return _finishRentalCommand ?? (_finishRentalCommand = new RelayCommand(FinishRental, _ => true));
            }
        }

        private ICommand _editReturnDateCommand;
        public ICommand EditReturnDateCommand
        {
            get
            {
                return _editReturnDateCommand ?? (_editReturnDateCommand = new RelayCommand(EditReturnDate, _ => true));
            }
        }

        //======//

        public void Search(object parameter)
        {
            LoadRentalsDB();
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                FilteredRentals = new ObservableCollection<Rental>(Rentals);
                return;
            }

            string searchText = FilterText.ToLower();
            FilteredRentals = new ObservableCollection<Rental>(Rentals.Where(rental =>
                rental.CarString.ToLower().Contains(searchText) ||
                rental.CustomerString.ToLower().Contains(searchText) ||
                rental.RentalEnd.ToLower().Contains(searchText) ||
                rental.RentalStart.ToLower().Contains(searchText)));

            Rentals.Clear();
            foreach (Rental rental in FilteredRentals)
            {
                Rentals.Add(rental);
            }
            OnPropertyChanged(nameof(Rentals));
        }

        public void Remove(object parameter)
        {
            RemoveRentals();
        }

        public void Add(object parameter)
        {
            AddRentalWindow addRentalWindow = new AddRentalWindow();
            if (addRentalWindow.ShowDialog() == true)
            {
                Rental newRental = addRentalWindow.Rental;
                if (newRental != null)
                {
                    InsertRental(newRental);
                }
            }
        }

        public void SelectAll(object parameter)
        {
            bool allSelected = true;
            foreach (Rental rental in Rentals)
            {
                if (!rental.IsSelected)
                {
                    allSelected = false;
                    break;
                }
            }

            if (allSelected)
            {
                foreach (Rental rental in Rentals)
                {
                    rental.IsSelected = false;
                }
            }
            else
            {
                foreach (Rental rental in Rentals)
                {
                    rental.IsSelected = true;
                }
            }

            OnPropertyChanged(nameof(Rentals));
        }

        public void Edit(object parameter)
        {

            Rental rentalToEdit = new Rental();
            int id = 0;

            int selectedCount = 0;

            foreach (Rental rental in Rentals)
            {
                if (rental.IsSelected)
                {
                    selectedCount++;
                    id = rental.IdDB;
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
                messageBoxNoneSelected.Message = "Select a rental to modify";
                bool? result = messageBoxNoneSelected.ShowDialog();
            }
            else if (selectedCount == 1)
            {
                EditRentalWindow editRentalWindow = new EditRentalWindow();
                if (editRentalWindow.ShowDialog() == true)
                {
                    rentalToEdit = editRentalWindow.Rental;
                    if (UpdateRentalDB(rentalToEdit, id))
                    {
                        Rentals.Clear();
                        LoadRentalsDB();
                        CustomMessageBoxOk messageBoxSucces = new CustomMessageBoxOk();
                        messageBoxSucces.Message = "Modified succesfully!";
                        bool? result = messageBoxSucces.ShowDialog();
                    }
                }
            }
            else
            {
                CustomMessageBoxOk messageBoxTooMany = new CustomMessageBoxOk();
                messageBoxTooMany.Message = "Selected to many rentals!\nYou can only modify one rental at once";
                bool? result = messageBoxTooMany.ShowDialog();
            }
        }

        private void FinishRental(object parameter)
        {
            if (parameter is Rental rental)
            {
                rental.IsReturned = true;
                rental.Returned = "Yes";
                rental.RentalReturn = DateTime.Today.ToString("d");
                rental.updateDates();
                if (UpdateRentalDB(rental, rental.IdDB))
                {
                    while (true)
                    {
                        CustomMessageBoxYesNo messageBoxIsDamaged = new CustomMessageBoxYesNo();
                        messageBoxIsDamaged.Message = "Is car damaged?";
                        bool? result = messageBoxIsDamaged.ShowDialog();
                        if (result.HasValue && !result.Value)
                        {
                            break;
                        }

                        QuickAddRepairmentWindow addRepairmentWindow = new QuickAddRepairmentWindow();
                        addRepairmentWindow.carId = rental.CarId;

                        if (addRepairmentWindow.ShowDialog() == true)
                        {
                            if(ValuesHelper.InsertRepairmentDB(addRepairmentWindow.Repairment))
                            {
                                CustomMessageBoxOk messageBoxTooMany = new CustomMessageBoxOk();
                                messageBoxTooMany.Message = "Repairment inserted succesfully!";
                                messageBoxTooMany.ShowDialog();
                            }
                            break;
                        }
                        else
                        {
                            if (ValuesHelper.InsertRepairmentDB(addRepairmentWindow.Repairment))
                            {
                                CustomMessageBoxOk messageBoxTooMany = new CustomMessageBoxOk();
                                messageBoxTooMany.Message = "Repairment inserted succesfully!";
                                messageBoxTooMany.ShowDialog();
                            }
                            break;
                        }
                    }

                    List<Rental> storedRentals = new List<Rental>(Rentals);
                    Rentals.Clear();
                    foreach (Rental storedRental in storedRentals)
                    {
                        Rentals.Add(storedRental);
                    }
                }
                else
                {
                    rental.IsReturned = false;
                    rental.Returned = "No";
                    rental.RentalReturn = "";
                    rental.updateDates();
                }
            }
        }

        private void EditReturnDate(object parameter)
        {
            if (parameter is Rental rental)
            {
                EditSinglePropertyWindow editWindow = new EditSinglePropertyWindow();
                editWindow.Property = "Return Date: ";
                editWindow.Case = "Return Date";
                editWindow.argument = rental.RentalStart;
                bool? result = editWindow.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    string oldProperty = rental.RentalReturn;
                    rental.RentalReturn = editWindow.newProperty;
                    rental.updateDates();
                    rental.TotalCost = rental.computeCost();
                    if (UpdateRentalDB(rental, rental.IdDB))
                    {
                        List<Rental> storedRentals = new List<Rental>(Rentals);
                        Rentals.Clear();
                        foreach (Rental storedRental in storedRentals)
                        {
                            Rentals.Add(storedRental);
                        }
                    }
                    else
                    {
                        rental.RentalReturn = oldProperty;
                        rental.updateDates();
                        rental.TotalCost = rental.computeCost();
                    }
                }
            }
        }

        //======//

        public void InsertRental(Rental rental)
        {
            if (InsertRentalDB(rental))
            {
                rental.updateDates();
                rental.TotalCost = rental.computeCost();
                Rentals.Clear();
                LoadRentalsDB();
            }
        }

        public void RemoveRentals()
        {
            bool anySelected = false;

            foreach (Rental rental in Rentals)
            {
                anySelected = rental.IsSelected;
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
                    if (RemoveSelectedRentals())
                    {
                        List<Rental> storedRentals = new List<Rental>(Rentals);
                        Rentals.Clear();
                        foreach (Rental rental in storedRentals)
                        {
                            Rentals.Add(rental);
                        }
                        OnPropertyChanged(nameof(Rentals));
                    }
                }
            }
            else
            {
                CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                messageBox.Message = "Select a rental to delete";

                bool? result = messageBox.ShowDialog();
            }
        }

        //

        public void LoadRentalsDB()
        {
            Rentals = new ObservableCollection<Rental>();
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "SELECT * FROM Rentals";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int renralId = Convert.ToInt32(reader["ID"].ToString());
                            int carId = Convert.ToInt32(reader["Car ID"].ToString());
                            int customerId = Convert.ToInt32(reader["Customer ID"].ToString());
                            string rentalStart = reader["Rental Start"].ToString();
                            string rentalEnd = reader["Rental End"].ToString();
                            bool isDone = Convert.ToBoolean(reader["Is Done"].ToString());
                            bool isReturned = Convert.ToBoolean(reader["Is Returned"].ToString());
                            bool isDamage = Convert.ToBoolean(reader["Is Damage"].ToString());

                            string rentalReturn = reader["Date Return"].ToString();

                            Car car = ValuesHelper.GetCarByID(carId);
                            Customer customer = ValuesHelper.GetCustomerByID(customerId);
                            Rental rental = new Rental
                            {
                                IdDB = renralId,
                                CarId = carId,
                                CarString = $"{car.Brand} {car.Model} {car.RegisterPlate}",
                                CustomerId = customerId,
                                CustomerString = $"{customer.Name} {customer.Surname}",
                                RentalStart = DateTime.Parse(rentalStart).ToString("d"),
                                RentalEnd = DateTime.Parse(rentalEnd).ToString("d"),
                                RentalReturn = (rentalReturn != "")? DateTime.Parse(rentalReturn).ToString("d") : "",
                                IsDone = isDone,
                                Done = isDone ? "Yes" : "No",
                                IsReturned = isReturned,
                                Returned = isReturned ? "Yes" : "No",
                                IsDamage = isDamage,
                                IsSelected = false
                            };
                            rental.updateDates();
                            rental.TotalCost = rental.computeCost();
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
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "INSERT INTO Rentals ([Car ID], [Customer ID], [Rental Start], [Rental End], [Is Done], [Is Returned], " +
                        "[Is Damage], [Date Return]) VALUES (@CarId, @CustomerId, @RentalStart, @RentalEnd, @IsDone, @IsReturned, @IsDamage, @RentalReturn)";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerId", rental.CustomerId);
                    command.Parameters.AddWithValue("@CarId", rental.CarId);
                    command.Parameters.AddWithValue("@RentalStart", rental.RentalStart);
                    command.Parameters.AddWithValue("@RentalEnd", rental.RentalEnd);
                    command.Parameters.AddWithValue("@IsDone", rental.IsDone);
                    command.Parameters.AddWithValue("@IsReturned", rental.IsReturned);
                    command.Parameters.AddWithValue("@IsDamage", rental.IsDamage);
                    command.Parameters.AddWithValue("@RentalReturn", string.IsNullOrEmpty(rental.RentalReturn) ? (object)DBNull.Value : rental.RentalReturn);

                    connection.Open();
                    command.ExecuteNonQuery();

                    ValuesHelper.ChangeAvailability(rental.IsReturned, rental.CarId);

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
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    List<Rental> selectedRentals = new List<Rental>();

                    connection.Open();

                    foreach (Rental rental in Rentals)
                    {
                        if (rental.IsSelected)
                        {
                            string query = "DELETE FROM Rentals WHERE [ID] = @RentalID";
                            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                            command.Parameters.AddWithValue("@RentalID", rental.IdDB);
                            command.ExecuteNonQuery();
                            if (!rental.IsReturned)
                            {
                                ValuesHelper.ChangeAvailability(true, rental.CarId);
                            }
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

        private bool UpdateRentalDB(Rental rental, int id)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "UPDATE Rentals SET [Customer ID] = @CustomerId, [Car ID] = @CarId, [Rental Start] = @RentalStart, " +
                                   "[Rental End] = @RentalEnd, [Is Done] = @IsDone, [Is Returned] = @IsReturned, " +
                                   "[Is Damage] = @IsDamage, [Date Return] = @RentalReturn WHERE ID = @id";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@CustomerId", rental.CustomerId);
                    command.Parameters.AddWithValue("@CarId", rental.CarId);
                    command.Parameters.AddWithValue("@RentalStart", rental.RentalStart);
                    command.Parameters.AddWithValue("@RentalEnd", rental.RentalEnd);
                    command.Parameters.AddWithValue("@IsDone", rental.IsDone);
                    command.Parameters.AddWithValue("@IsReturned", rental.IsReturned);
                    command.Parameters.AddWithValue("@IsDamage", rental.IsDamage);
                    command.Parameters.AddWithValue("@RentalReturn", string.IsNullOrEmpty(rental.RentalReturn) ? (object)DBNull.Value : rental.RentalReturn);
                    command.Parameters.AddWithValue("@id", id);

                    connection.Open();
                    command.ExecuteNonQuery();

                    ValuesHelper.ChangeAvailability(rental.IsReturned, rental.CarId);

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
