using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Emergency_Department_Simulator_PL.Commands;

namespace Emergency_Department_Simulator_PL
{
    public class ViewModel : NotifyPropertyChanged
    {
        #region instance variables
        private int _registeredPatients;
        private int _treatedPatients;
        private int _dischargedPatients;
        #endregion
    }
}
