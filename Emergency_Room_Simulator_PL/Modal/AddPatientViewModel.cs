using Emergency_Department_Simulator_BLL;
using Emergency_Department_Simulator_PL.Commands;

namespace Emergency_Department_Simulator_PL.Modal
{
    public class AddPatientViewModel : NotifyPropertyChanged
    {
        private string _patientName;
        private DateOnly _patientDateOfBirth;
        private bool _dialogResult;
        private PatientManager _patientManager;

        public string PatientName { get { return _patientName; } set { if (_patientName != value) { _patientName = value; OnPropertyChanged(nameof(PatientName)); AddPatient.RaiseCanExecuteChanged(); } } }
        public DateOnly PatientDateOfBirth { get { return _patientDateOfBirth; } set { if (_patientDateOfBirth != value) { _patientDateOfBirth = value; OnPropertyChanged(nameof(PatientDateOfBirth)); } } }
        public bool DialogResult { get { return _dialogResult; } set { if (_dialogResult != value) { _dialogResult = value; OnPropertyChanged(nameof(DialogResult)); } } }
        public Action Close { get; set; }

        public Command AddPatient { get; private set; }
        public Command CancelAddPatient { get; private set; }

        public AddPatientViewModel(PatientManager patientManager)
        {
            _patientManager = patientManager;
            AddPatient = new Command(AddNewPatient, CanAddPatient);
            CancelAddPatient = new Command(CancelNewPatient, CanCancelAddPatient);
        }

        private bool CanAddPatient()
        {
            if (PatientName.Length > 0 && !_patientManager.IsPatientRegistered(PatientName, PatientDateOfBirth))
                return true;
            return false;
        }

        private bool CanCancelAddPatient() => true;

        private void AddNewPatient()
        {
            DialogResult = true;
            Close?.Invoke();
        }

        private void CancelNewPatient()
        {
            DialogResult = false;
            Close?.Invoke();
        }
    }
}
