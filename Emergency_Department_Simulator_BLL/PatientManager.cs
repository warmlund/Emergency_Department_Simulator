using Emergency_Department_Simulator_DTO;
using System.Collections.ObjectModel;

namespace Emergency_Department_Simulator_BLL
{
    public class PatientManager : IPatientManager
    {
        private ObservableCollection<Patient> _patientStorage;
        private List<Status> _statusList;

        public ObservableCollection<Patient> PatientStorage { get { return _patientStorage; } }
        public PatientManager()
        {
            _patientStorage = new ObservableCollection<Patient>();
            _statusList = new List<Status>();
        }

        public void AddPatient()
        {
            throw new NotImplementedException();
        }

        public void UpdatePatient()
        {
            throw new NotImplementedException();
        }

        public void LoadPatients()
        {
            throw new NotImplementedException();
        }

        public void SavePatients()
        {
            throw new NotImplementedException();
        }

        public void CreatePatientId()
        {
            throw new NotImplementedException();
        }

        public List<Patient> GetPatients()
        {
            throw new NotImplementedException();
        }

        public int GetRegisteredPatients() => _patientStorage.Where(p => p.Status == StatusType.Registered).Count();

        public int GetDischargedPatients() => _patientStorage.Where(p => p.Status == StatusType.Discharged).Count();

        public int GetTreatedPatients() => _patientStorage.Where(p => p.Status == StatusType.Treated).Count();
    }
}
