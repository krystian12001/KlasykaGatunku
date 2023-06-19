using KlasykaGatunku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasykaGatunku.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public CarsViewModel CarsVm { get; set; }

        public ClientsViewModel ClientsVm { get; set; }

        public RentalsViewModel RentalsVm { get; set; }

        public RepairmentsViewModel RepairmentsVm { get; set; }

        public ReportsViewModel ReportsVm { get; set; }

        public RelayCommand CarsViewCommand { get; set; }

        public RelayCommand ClientsViewCommand { get; set; }

        public RelayCommand RentalsViewCommand { get; set; }

        public RelayCommand RepairmentsViewCommand { get; set; }

        public RelayCommand ReportsViewCommand { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            CarsVm = new CarsViewModel();

            ClientsVm = new ClientsViewModel();

            RentalsVm = new RentalsViewModel();

            RepairmentsVm = new RepairmentsViewModel();

            ReportsVm = new ReportsViewModel(this);

            CurrentView = CarsVm;

            CarsViewCommand = new RelayCommand(o =>
            {
                CurrentView = CarsVm;
            });

            ClientsViewCommand = new RelayCommand(o =>
            {
                CurrentView = ClientsVm;
            });

            RentalsViewCommand = new RelayCommand(o =>
            {
                RentalsVm.LoadRentalsDB();
                CurrentView = RentalsVm;
            });

            RepairmentsViewCommand = new RelayCommand(o =>
            {
                RepairmentsVm.LoadRepairmentsDB();
                CurrentView = RepairmentsVm;
            });

            ReportsViewCommand = new RelayCommand(o =>
            {
                ReportsVm.fillData();
                CurrentView = ReportsVm;
            });
        }
    }
}
