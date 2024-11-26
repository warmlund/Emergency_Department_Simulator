using Emergency_Department_Simulator_BLL.EventHandlers;
using Emergency_Department_Simulator_DTO;
using Emergency_Department_Simulator_DAL;
using System.Collections.ObjectModel;

namespace Emergency_Department_Simulator_BLL
{
    public class PatientManager : IPatientManager
    {
        private ObservableCollection<Patient> _patientStorage;
        private List<string> _statusList;
        private PatientData _patientData;

        public ObservableCollection<Patient> PatientStorage { get { return _patientStorage; } }
        public List<string> StatusList { get { return _statusList; } }

        public PatientManager()
        {
            _patientStorage = new ObservableCollection<Patient>();
            _statusList = new List<string>();
            _patientData = new PatientData();
        }

        public async Task<bool> AddPatient(string name, DateOnly date)
        {
            if (IsPatientRegistered(name, date))
                return false;

            else
            {
                string id = CreatePatientId();
                Patient patient = new Patient { Name = name, DateOfBirth = date, PatientId = id, Status = StatusType.Registered };
                _patientStorage.Add(patient);

                EmergencySimulator emergencySimulator = new EmergencySimulator();
                await emergencySimulator.SimulateEmergencyActivity(patient);
                emergencySimulator.NurseUpdate += OnNurseUpdate;
                emergencySimulator.DoctorUpdate += OnDoctorUpdate;

                return true;
            }
        }

        public bool LoadPatients()
        {
            _patientStorage = _patientData.LoadPatients();

            if(_patientStorage == null)
                return false;
            return true;
        }

        public bool SavePatients() => _patientData.SavePatients(_patientStorage);

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

        private void OnNurseUpdate(object sender, NurseUpdateEventArgs e)
        {
            _statusList.Add(e.Message);
        }

        private void OnDoctorUpdate(Object sender, DoctorUpdateEventArgs e)
        {
            StatusList.Add(e.Message);
            CheckDischargedStatus();
        }

        private void CheckDischargedStatus()
        {
            if (_patientStorage.All(p => p.Status == StatusType.Discharged))
                StatusList.Add("STATUS: All patients are discharged");
        }
    }
}
