using Emergency_Department_Simulator_BLL;
using Emergency_Department_Simulator_PL.Commands;

namespace Emergency_Department_Simulator_PL.Modal
{
    /// <summary>
    /// viewmodel of the modal window for adding patients
    /// </summary>
    public class AddPatientViewModel : NotifyPropertyChanged
    {
        private string _patientName;
        private DateTime _patientDateOfBirth;
        private bool _dialogResult;
        private PatientManager _patientManager;

        public string PatientName { get { return _patientName; } set { if (_patientName != value) { _patientName = value; OnPropertyChanged(nameof(PatientName)); AddPatient.RaiseCanExecuteChanged(); } } }
        public DateTime PatientDateOfBirth { get { return _patientDateOfBirth; } set { if (_patientDateOfBirth != value) { _patientDateOfBirth = value; OnPropertyChanged(nameof(PatientDateOfBirth)); } } }
        public bool DialogResult { get { return _dialogResult; } set { if (_dialogResult != value) { _dialogResult = value; OnPropertyChanged(nameof(DialogResult)); } } }
        public Action Close { get; set; }

        public Command AddPatient { get; private set; }
        public Command CancelAddPatient { get; private set; }

        /// <summary>
        /// Constructor of the view model
        /// </summary>
        /// <param name="patientManager">the patientmanager from the bll layer</param>
        /// <exception cref="ArgumentNullException">returns and exception if the patientmanager is null</exception>
        public AddPatientViewModel(PatientManager patientManager)
        {
            _patientManager = patientManager ?? throw new ArgumentNullException(nameof(patientManager)); //checks if the patientmanager is null
            AddPatient = new Command(AddNewPatient, CanAddPatient);
            CancelAddPatient = new Command(CancelNewPatient, CanCancelAddPatient);
            PatientName = string.Empty;
            PatientDateOfBirth = DateTime.Today;
        }

        /// <summary>
        /// Boolean for checking if the patient can be added
        /// </summary>
        /// <returns>true if the patient name is longer than 0 and if the patient is not registered</returns>
        private bool CanAddPatient()
        {
            if (PatientName.Length > 0 && !_patientManager.IsPatientRegistered(PatientName, PatientDateOfBirth))
                return true;
            return false;
        }

        /// <summary>
        /// Boolean that controls if the adding patient modal can be cancelled
        /// </summary>
        /// <returns>true</returns>
        private bool CanCancelAddPatient() => true;

        /// <summary>
        /// sets the dialog result to true and invokes the close action
        /// </summary>
        private void AddNewPatient()
        {
            DialogResult = true;
            Close?.Invoke();
        }

        /// <summary>
        /// sets the dialog result to false and invokes the close action
        /// </summary>
        private void CancelNewPatient()
        {
            DialogResult = false;
            Close?.Invoke();
        }
    }
}
