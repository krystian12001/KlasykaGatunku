using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using KlasykaGatunku.Core;
using KlasykaGatunku.WindowsApp;
using KlasykaGatunku.Utils;
using System.Text.RegularExpressions;
using System.Windows;

namespace KlasykaGatunku.MVVM.ViewModel
{
    public class DataPoint
    {
        public string Label { get; set; }
        public double Value { get; set; }
    }

    public class ReportsViewModel : ObservableObject
    {
        private readonly MainViewModel _mainViewModel;

        private ObservableCollection<DataPoint> carsDataPoints { get; set; }

        public ObservableCollection<DataPoint> CarsDataPoints
        {
            get { return carsDataPoints; }
            set
            {
                carsDataPoints = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DataPoint> repairmentsDataPoints { get; set; }

        public ObservableCollection<DataPoint> RepairmentsDataPoints
        {
            get { return repairmentsDataPoints; }
            set
            {
                repairmentsDataPoints = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<DataPoint> clientsDataPoints { get; set; }

        public ObservableCollection<DataPoint> ClientsDataPoints
        {
            get { return clientsDataPoints; }
            set
            {
                clientsDataPoints = value;
                OnPropertyChanged();
            }
        }

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

        public DateTime startingDate = DateTime.MinValue;

        public DateTime endingDate = DateTime.MaxValue;

        private string startingDateString { get; set; }

        public string StartingDateString
        {
            get { return startingDateString; }
            set
            {
                startingDateString = value;
                OnPropertyChanged();
            }
        }

        private string endingDateString { get; set; }

        public string EndingDateString
        {
            get { return endingDateString; }
            set
            {
                endingDateString = value;
                OnPropertyChanged();
            }
        }

        public ReportsViewModel(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;

            fillData();
        }

        public void fillData()
        {
            StartingDateString = startingDate.ToString("d");

            EndingDateString = endingDate.ToString("d");

            Rentals = _mainViewModel.RentalsVm.Rentals;

            Repairments = _mainViewModel.RepairmentsVm.Repairments;

            ObservableCollection<DataPoint> CarsDataPointsTemp = new ObservableCollection<DataPoint>();

            ObservableCollection<DataPoint> RepairmentsDataPointsTemp = new ObservableCollection<DataPoint>();

            ObservableCollection<DataPoint> ClientsDataPointsTemp = new ObservableCollection<DataPoint>();


            foreach (Rental rental in Rentals)
            {
                if (rental.RentalStartDateTime >= startingDate && rental.RentalEndDateTime <= endingDate)
                {
                    int carId = rental.CarId;

                    DataPoint existingDataPoint = CarsDataPointsTemp.FirstOrDefault(dp => dp.Label == $"{ValuesHelper.GetCarByID(carId).Brand} {ValuesHelper.GetCarByID(carId).Model} {ValuesHelper.GetCarByID(carId).RegisterPlate} ");

                    if (existingDataPoint != null)
                    {
                        existingDataPoint.Value += 1;

                    }
                    else
                    {
                        DataPoint newDataPoint = new DataPoint { Label = $"{ValuesHelper.GetCarByID(carId).Brand} {ValuesHelper.GetCarByID(carId).Model} {ValuesHelper.GetCarByID(carId).RegisterPlate} ", Value = 1 };
                        CarsDataPointsTemp.Add(newDataPoint);
                    }
                }
            }

            foreach (Repairment repairment in Repairments)
            {
                if (repairment.DamageDateDateTime >= startingDate && repairment.DamageDateDateTime <= endingDate)
                {
                    int carId = repairment.CarId;

                    DataPoint existingDataPoint = RepairmentsDataPointsTemp.FirstOrDefault(dp => dp.Label == $"{ValuesHelper.GetCarByID(carId).Brand} {ValuesHelper.GetCarByID(carId).Model} {ValuesHelper.GetCarByID(carId).RegisterPlate} ");

                    if (existingDataPoint != null)
                    {
                        existingDataPoint.Value += 1;
                    }
                    else
                    {
                        DataPoint newDataPoint = new DataPoint { Label = $"{ValuesHelper.GetCarByID(carId).Brand} {ValuesHelper.GetCarByID(carId).Model} {ValuesHelper.GetCarByID(carId).RegisterPlate} ", Value = 1 };
                        RepairmentsDataPointsTemp.Add(newDataPoint);
                    }
                }
            }

            foreach (Rental rental in Rentals)
            {
                if (rental.RentalStartDateTime >= startingDate && rental.RentalEndDateTime <= endingDate)
                {
                    int customerId = rental.CustomerId;

                    DataPoint existingDataPoint = ClientsDataPointsTemp.FirstOrDefault(dp => dp.Label == $"{ValuesHelper.GetCustomerByID(customerId).Name} {ValuesHelper.GetCustomerByID(customerId).Surname} ");

                    if (existingDataPoint != null)
                    {
                        existingDataPoint.Value += rental.TotalCost;
                    }
                    else
                    {
                        DataPoint newDataPoint = new DataPoint { Label = $"{ValuesHelper.GetCustomerByID(customerId).Name} {ValuesHelper.GetCustomerByID(customerId).Surname} ", Value = rental.TotalCost };
                        ClientsDataPointsTemp.Add(newDataPoint);
                    }
                }
            }

            CarsDataPoints = CarsDataPointsTemp;

            RepairmentsDataPoints = RepairmentsDataPointsTemp;

            ClientsDataPoints = ClientsDataPointsTemp;
        }

        private string startDateText;

        public string StartDateText
        {
            get { return startDateText; }
            set
            {
                startDateText = value;
                OnPropertyChanged();
            }
        }

        private string endDateText;

        public string EndDateText
        {
            get { return endDateText; }
            set
            {
                endDateText = value;
                OnPropertyChanged();
            }
        }

        private ICommand _setStartClickCommand;
        public ICommand SetStartClickCommand
        {
            get
            {
                return _setStartClickCommand ?? (_setStartClickCommand = new RelayCommand(SetStart, (parameter) => true));
            }
        }

        private ICommand _setEndClickCommand;
        public ICommand SetEndClickCommand
        {
            get
            {
                return _setEndClickCommand ?? (_setEndClickCommand = new RelayCommand(SetEnd, (parameter) => true));
            }
        }

        private void SetStart(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(StartDateText))
            {
                if (checkDateInput(StartDateText))
                {
                    if (IsValidDateFormat(StartDateText))
                    {
                        if (DateTime.Parse(StartDateText) > endingDate)
                        {
                            CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                            messageBox.Message = "Rental starting date cannot be bigger than ending date";

                            bool? result = messageBox.ShowDialog();
                        }
                        else
                        {
                            startingDate = DateTime.Parse(StartDateText);
                            StartingDateString = startingDate.ToString("d");
                            fillData();
                        }
                    }
                    else
                    {
                        CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                        messageBox.Message = "Invalid date format!";

                        bool? result = messageBox.ShowDialog();
                    }
                }
                else
                {
                    CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                    messageBox.Message = "Invalid month and/or day!";

                    bool? result = messageBox.ShowDialog();
                }
            }
            else
            {
                CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                messageBox.Message = "Insert value";

                bool? result = messageBox.ShowDialog();
            }

        }

        private void SetEnd(object parameter)
        {
            if (!string.IsNullOrWhiteSpace(EndDateText))
            {
                if (checkDateInput(EndDateText))
                {
                    if (IsValidDateFormat(EndDateText))
                    {
                        if (DateTime.Parse(EndDateText) < startingDate)
                        {
                            CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                            messageBox.Message = "Rental starting date cannot be bigger than ending date";

                            bool? result = messageBox.ShowDialog();
                        }
                        else
                        {
                            endingDate = DateTime.Parse(EndDateText);
                            EndingDateString = endingDate.ToString("d");
                            fillData();
                        }
                    }
                    else
                    {
                        CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                        messageBox.Message = "Invalid date format!";

                        bool? result = messageBox.ShowDialog();
                    }
                }
                else
                {
                    CustomMessageBoxOk messageBox = new CustomMessageBoxOk();
                    messageBox.Message = "Insert value";

                    bool? result = messageBox.ShowDialog();
                }
            }
        }


        public bool IsValidDateFormat(string input)
        {
            string pattern = @"^[0-9]{1,2}/[0-9]{1,2}/\d{4}$";
            return Regex.IsMatch(input, pattern);
        }

        public bool checkDateInput(string input)
        {
            string[] parts = input.Split('/');
            if (parts.Length == 3 && int.TryParse(parts[0], out int month) && int.TryParse(parts[1], out int day))
            {
                return month <= 12 && day <= 31;
            }
            else
            {
                return false;
            }
        }
    }
}
