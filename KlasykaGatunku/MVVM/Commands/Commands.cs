using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using KlasykaGatunku.Core;

namespace KlasykaGatunku.MVVM.Commands
{
    public class CommandsMenager : RelayCommand
    {
        public RelayCommand AddIconClickCommand { get; }
        public RelayCommand TrashIconClickCommand { get; }
        public RelayCommand SelectAllIconClickCommand { get; }

        public CommandsMenager() : base(null)
        {
            AddIconClickCommand = new RelayCommand(AddIconClick);
            TrashIconClickCommand = new RelayCommand(TrashIconClick);
            SelectAllIconClickCommand = new RelayCommand(SelectAllIconClick);
        }

        private void AddIconClick(object parameter)
        {
            MessageBox.Show("Add button clicked");
        }

        private void TrashIconClick(object parameter)
        {
            MessageBox.Show("Trash button clicked");
        }

        private void SelectAllIconClick(object parameter)
        {
            MessageBox.Show("Select All button clicked");
        }
    }
}
