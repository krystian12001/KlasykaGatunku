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
using System.Data;

namespace KlasykaGatunku.MVVM.ViewModel
{
    public class Repairment : ObservableObject
    {
        public int IdDB { get; set; }
        public int CarId { get; set; }
        public string CarString { get; set; }
        public string DamageDescription { get; set; }
        public int FixCost { get; set; }
        public string Fixed { get; set; }
        public string DamageDate { get; set; }
        public DateTime DamageDateDateTime { get; set; }
        private bool isFixed { get; set; }
        public bool IsFixed
        {
            get { return isFixed; }
            set
            {
                if (isFixed != value)
                {
                    isFixed = value;
                    OnPropertyChanged();
                }
            }
        }
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

        public Repairment()
        {
            IdDB = -1;
            CarId = 1;
            CarString = $"{ValuesHelper.GetCarByID(CarId).Brand} {ValuesHelper.GetCarByID(CarId).Model} {ValuesHelper.GetCarByID(CarId).RegisterPlate}";
            DamageDescription = "Seatbelt control not working properly";
            FixCost = 500;
            Fixed = "No";
            DamageDate = "1/1/2023";
            DamageDateDateTime = DateTime.Parse(DamageDate);
            IsFixed = false;
            IsSelected = false;
        }

        public Repairment(int idDB, int carId, string damageDescription, int fixCost, string damageDate, bool isFixed)
        {
            if (idDB == null)
            {
                IdDB = -1;
            }
            else
            {
                IdDB = idDB;
            }
            CarId = carId;
            CarString = $"{ValuesHelper.GetCarByID(CarId).Brand} {ValuesHelper.GetCarByID(CarId).Model} {ValuesHelper.GetCarByID(CarId).RegisterPlate}";
            DamageDescription = damageDescription;
            FixCost = fixCost;
            Fixed = isFixed ? "Yes" : "No";
            DamageDate = damageDate;
            DamageDateDateTime = DateTime.Parse(damageDate);
            IsFixed = isFixed;
            IsSelected = false;
        }
    }

    public class RepairmentsViewModel : ObservableObject
    {
        private ObservableCollection<Repairment> repairments;

        public ObservableCollection<Repairment> Repairments
        {
            get { return repairments; }
            set
            {
                repairments = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Repairment> filteredRepairments;

        public ObservableCollection<Repairment> FilteredRepairments
        {
            get { return filteredRepairments; }
            set
            {
                filteredRepairments = value;
                OnPropertyChanged(nameof(FilteredRepairments));
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

        public RepairmentsViewModel()
        {
            LoadRepairmentsDB();
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

        private ICommand _finishRepairmentCommand;
        public ICommand FinishRepairmentCommand
        {
            get
            {
                return _finishRepairmentCommand ?? (_finishRepairmentCommand = new RelayCommand(FinishRepairment, _ => true));
            }
        }

        public void Search(object parameter)
        {
            LoadRepairmentsDB();
            if (string.IsNullOrWhiteSpace(FilterText))
            {
                FilteredRepairments = new ObservableCollection<Repairment>(Repairments);
                return;
            }
            
            string searchText = FilterText.ToLower();
            FilteredRepairments = new ObservableCollection<Repairment>(Repairments.Where(repairment =>
                (ValuesHelper.GetCarByID(repairment.CarId)).Brand.ToLower().Contains(searchText) ||
                (ValuesHelper.GetCarByID(repairment.CarId)).Model.ToLower().Contains(searchText) ||
                (ValuesHelper.GetCarByID(repairment.CarId)).RegisterPlate.ToLower().Contains(searchText) ||
                repairment.DamageDescription.ToLower().Contains(searchText) ||
                repairment.FixCost.ToString().ToLower().Contains(searchText)));

            Repairments.Clear();
            foreach (Repairment repairment in FilteredRepairments)
            {
                Repairments.Add(repairment);
            }
            OnPropertyChanged(nameof(Repairments));
        }

        public void Remove(object parameter)
        {
            RemoveRepairments();
        }

        public void Add(object parameter)
        {
            AddRepairmentWindow addRepairmentWindow = new AddRepairmentWindow();
            if (addRepairmentWindow.ShowDialog() == true)
            {
                Repairment newRepairment = addRepairmentWindow.Repairment;
                if (newRepairment != null)
                {
                    InsertRepairment(newRepairment);
                }
            }
        }

        public void SelectAll(object parameter)
        {
            bool allSelected = true;
            foreach (Repairment repairment in Repairments)
            {
                if (!repairment.IsSelected)
                {
                    allSelected = false;
                    break;
                }
            }

            if (allSelected)
            {
                foreach (Repairment repairment in Repairments)
                {
                    repairment.IsSelected = false;
                }
            }
            else
            {
                foreach (Repairment repairment in Repairments)
                {
                    repairment.IsSelected = true;
                }
            }

            OnPropertyChanged(nameof(Repairments));
        }

        public void Edit(object parameter)
        {

            Repairment repairmentToEdit = new Repairment();
            int id = 0;

            int selectedCount = 0;

            foreach (Repairment repairment in Repairments)
            {
                if (repairment.IsSelected)
                {
                    selectedCount++;
                    id = repairment.IdDB;
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
                messageBoxNoneSelected.Message = "Select a repairment to modify";
                bool? result = messageBoxNoneSelected.ShowDialog();
            }
            else if (selectedCount == 1)
            {
                EditRepairmentWindow editRepairmentWindow = new EditRepairmentWindow();
                if (editRepairmentWindow.ShowDialog() == true)
                {
                    repairmentToEdit = editRepairmentWindow.Repairment;
                    if (UpdateRepairmentDB(repairmentToEdit, id))
                    {
                        Repairments.Clear();
                        LoadRepairmentsDB();
                        CustomMessageBoxOk messageBoxSucces = new CustomMessageBoxOk();
                        messageBoxSucces.Message = "Modified succesfully!";
                        bool? result = messageBoxSucces.ShowDialog();
                    }
                }
            }
            else
            {
                CustomMessageBoxOk messageBoxTooMany = new CustomMessageBoxOk();
                messageBoxTooMany.Message = "Selected to many repairments! You can only modify one repairment at once";
                bool? result = messageBoxTooMany.ShowDialog();
            }
        }

        private void FinishRepairment(object parameter)
        {
            if (parameter is Repairment repairment)
            {
                repairment.IsFixed = true;
                repairment.Fixed = "Yes";
                if (UpdateRepairmentDB(repairment, repairment.IdDB))
                {
                    List<Repairment> storedRepairments = new List<Repairment>(Repairments);
                    Repairments.Clear();
                    foreach (Repairment storedRepairment in storedRepairments)
                    {
                        Repairments.Add(storedRepairment);
                    }
                }
                else
                {
                    repairment.IsFixed = false;
                    repairment.Fixed = "No";
                }
            }
        }

        //======//

        public void InsertRepairment(Repairment repairment)
        {
            if (InsertRepairmentDB(repairment))
            {
                Repairments.Clear();
                LoadRepairmentsDB();
            }
        }

        public void RemoveRepairments()
        {
            bool anySelected = false;

            foreach (Repairment repairment in Repairments)
            {
                anySelected = repairment.IsSelected;
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
                    if (RemoveSelectedRepairments())
                    {
                        List<Repairment> storedRepairments = new List<Repairment>(Repairments);
                        Repairments.Clear();
                        foreach (Repairment repairment in storedRepairments)
                        {
                            Repairments.Add(repairment);
                        }
                        OnPropertyChanged(nameof(Repairments));
                    }
                }
            }
            else
            {
                CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                messageBox.Message = "Select a repairment to delete";

                bool? result = messageBox.ShowDialog();
            }
        }

        //======//

        public void LoadRepairmentsDB()
        {
            Repairments = new ObservableCollection<Repairment>();
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "SELECT * FROM Damage";
                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);

                    connection.Open();

                    using (System.Data.OleDb.OleDbDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idDB = Convert.ToInt32(reader["ID"].ToString());
                            int carId = Convert.ToInt32(reader["Car ID"].ToString());
                            string damageDescription = reader["Damage Description"].ToString();
                            int fixCost = Convert.ToInt32(reader["Fix Cost"].ToString());
                            bool isFixed = Convert.ToBoolean(reader["IsFixed"].ToString());
                            string damageDate = reader["Damage Date"].ToString();

                            Repairment repairment = new Repairment
                            {
                                IdDB = idDB,
                                CarId = carId,
                                CarString = $"{ValuesHelper.GetCarByID(carId).Brand} {ValuesHelper.GetCarByID(carId).Model} {ValuesHelper.GetCarByID(carId).RegisterPlate}",
                                DamageDescription = damageDescription,
                                Fixed = isFixed ? "Yes" : "No",
                                FixCost = fixCost,
                                IsFixed = isFixed,
                                DamageDate = DateTime.Parse(damageDate).ToString("d"),
                                DamageDateDateTime = DateTime.Parse(damageDate),
                                IsSelected = false // Set initial value to false
                            };

                            Repairments.Add(repairment);
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

        private bool InsertRepairmentDB(Repairment repairment)
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

        private bool RemoveSelectedRepairments()
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    List<Repairment> selectedRepairments = new List<Repairment>();

                    connection.Open();

                    foreach (Repairment repairment in Repairments)
                    {
                        if (repairment.IsSelected)
                        {
                            string query = "DELETE FROM Damage WHERE [ID] = @IdDB";
                            System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                            command.Parameters.AddWithValue("@IdDB", repairment.IdDB);
                            command.ExecuteNonQuery();

                            if (!repairment.IsFixed)
                            {
                                ValuesHelper.ChangeAvailability(true, repairment.CarId);
                            }

                            selectedRepairments.Add(repairment);
                        }
                    }

                    foreach (Repairment selectedRepairment in selectedRepairments)
                    {
                        Repairments.Remove(selectedRepairment);
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

        private bool UpdateRepairmentDB(Repairment repairment, int Id)
        {
            try
            {
                using (System.Data.OleDb.OleDbConnection connection = new System.Data.OleDb.OleDbConnection(ValuesHelper.connectionString))
                {
                    string query = "UPDATE Damage SET [Car ID] = @CarId, [Damage Description] = @DamageDescription, " +
                                   "[Fix Cost] = @FixCost, [IsFixed] = @IsFixed, [Damage Date] = @DamageDate WHERE [ID] = @Id";

                    System.Data.OleDb.OleDbCommand command = new System.Data.OleDb.OleDbCommand(query, connection);
                    command.Parameters.AddWithValue("@CarId", repairment.CarId);
                    command.Parameters.AddWithValue("@DamageDescription", repairment.DamageDescription);
                    command.Parameters.AddWithValue("@FixCost", repairment.FixCost);
                    command.Parameters.AddWithValue("@IsFixed", repairment.IsFixed);
                    command.Parameters.AddWithValue("@DamageDate", repairment.DamageDate);
                    command.Parameters.AddWithValue("@Id", Id);

                    connection.Open();
                    command.ExecuteNonQuery();

                    ValuesHelper.ChangeAvailability(repairment.IsFixed, repairment.CarId);

                    return true;
                }
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                System.Windows.MessageBox.Show("Error updating repairment: " + ex.Message);
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
