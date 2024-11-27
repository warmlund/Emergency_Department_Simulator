using Emergency_Department_Simulator_BLL;
using Emergency_Department_Simulator_DTO;
using Emergency_Department_Simulator_PL.Commands;
using Emergency_Department_Simulator_PL.Modal;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Emergency_Department_Simulator_PL
{
    public class ViewModel : NotifyPropertyChanged
    {
        #region instance variables
        private int _registeredPatients;
        private int _treatedPatients;
        private int _dischargedPatients;
        private PatientManager _patientManager;
        private string _searchTerm;
        #endregion

        #region properties
        public int RegisteredPatients { get { return _patientManager.GetRegisteredPatients(); } set { OnPropertyChanged(nameof(RegisteredPatients)); } }
        public int TreatedPatients { get { return _treatedPatients; } set { OnPropertyChanged(nameof(TreatedPatients)); } }
        public int DischargedPatients { get { return _dischargedPatients; } set { OnPropertyChanged(nameof(DischargedPatients)); } }
        public string SearchTerm { get { return _searchTerm; } set { if (_searchTerm != value) { _searchTerm = value; OnPropertyChanged(nameof(SearchTerm)); ApplyFilter(); } } } //Property search term bound to the textbox in the view
        public ICollectionView FilteredPatientList { get; } //An ICollectionView for enabling filtering of the datagridview
        public ObservableCollection<Patient> Patients
        {
            get { return _patientManager.PatientStorage; }
            set { if (_patientManager.PatientStorage != value) { OnPropertyChanged(nameof(Patients)); } }
        }

        public List<string> StatusBoard
        {
            get { return _patientManager.StatusList; }
            set { if (_patientManager.StatusList != value) { OnPropertyChanged(nameof(StatusBoard)); } }
        }
        #endregion

        #region Command
        public AsyncCommand AddPatient { get; private set; }
        #endregion

        public ViewModel(PatientManager patientManager)
        {
            _patientManager = patientManager;
            AddPatient = new AsyncCommand(AddNewPatient, CanAddNewPatient);
            FilteredPatientList = CollectionViewSource.GetDefaultView(Patients);
            FilteredPatientList.Filter = FilterPatients;
        }

        private bool CanAddNewPatient()
        {
            return true;
        }

        private async Task AddNewPatient()
        {
            AddPatientViewModel addPatientViewModel = new(_patientManager);
            AddPatientWindow addPatientWindow = new() { DataContext = addPatientViewModel };

            bool? result = addPatientWindow.ShowDialog();

            if (result == true)
            {
                await _patientManager.AddPatient(addPatientViewModel.PatientName, addPatientViewModel.PatientDateOfBirth);
            }

            else
            {
                return;
            }
        }

        /// <summary>
        /// A method that filters the datagridview based on name or id
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool FilterPatients(object obj)
        {
            if (obj is not Patient patient) return false;
            return string.IsNullOrEmpty(SearchTerm)
                   || patient.Name.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase)
                   || patient.PatientId.Contains(SearchTerm, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// A method that refreshes the filter every time the search term is changed
        /// </summary>
        private void ApplyFilter()
        {
            FilteredPatientList.Refresh();
        }

    }
}
