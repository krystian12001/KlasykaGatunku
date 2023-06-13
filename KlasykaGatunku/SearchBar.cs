using KlasykaGatunku.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlasykaGatunku
{
    class SearchBar
    {
        public RelayCommand SearchIconClickCommand { get; set; }

        public SearchBar()
        {
            SearchIconClickCommand = new RelayCommand(o =>
                {
                    //CurrentView = ReportsVm;
                });
        }
    }
}
