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

        public bool AddPatient(string name, DateOnly date)
        {
            if (IsPatientRegistered(name, date))
                return false;

            else
            {
                string id = CreatePatientId();
                _patientStorage.Add(new Patient { Name = name, DateOfBirth = date, PatientId = id, Status = StatusType.Registered });
                return true;
            }
        }

        public void UpdatePatientStatus()
        {
            throw new NotImplementedException();
        }
        public void UpdateStatusBoard()
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

        public string CreatePatientId()
        {
            int id = 1;
            List<int> ids = _patientStorage.Select(x => int.Parse(x.PatientId.Substring(2))).ToList();

            while (ids.Contains(id))
                id++;
            
            return "ER"+id.ToString();
        }

        public int GetRegisteredPatients() => _patientStorage.Where(p => p.Status == StatusType.Registered).Count();

        public int GetDischargedPatients() => _patientStorage.Where(p => p.Status == StatusType.Discharged).Count();

        public int GetTreatedPatients() => _patientStorage.Where(p => p.Status == StatusType.Treated).Count();
        public bool IsPatientRegistered(string name, DateOnly date) => _patientStorage.Any(p => p.Name == name && p.DateOfBirth == date);

    }
}
