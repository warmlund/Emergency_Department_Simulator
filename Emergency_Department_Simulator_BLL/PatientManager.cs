using Emergency_Department_Simulator_BLL.EventHandlers;
using Emergency_Department_Simulator_DTO;
using System.Collections.ObjectModel;
using System.Windows;

namespace Emergency_Department_Simulator_BLL
{
    public class PatientManager : IPatientManager
    {
        private ObservableCollection<Patient> _patientStorage;
        private ObservableCollection<StatusMessage> _statusList;

        public ObservableCollection<Patient> PatientStorage { get { return _patientStorage; } }
        public ObservableCollection<StatusMessage> StatusList { get { return _statusList; } }

        public PatientManager()
        {
            _patientStorage = new ObservableCollection<Patient>();
            _statusList = new ObservableCollection<StatusMessage>();
        }

        public async Task<bool> AddPatient(string name, DateTime date)
        {
            if (IsPatientRegistered(name, date))
                return false;

            else
            {
                string id = CreatePatientId();
                Patient patient = new Patient { Name = name, DateOfBirth = date, PatientId = id, Status = StatusType.Registered };
                _patientStorage.Add(patient);
                _statusList.Add(new StatusMessage { Message = $"REGISTERED: {patient.PatientId} {patient.Name}" });

                EmergencySimulator emergencySimulator = new EmergencySimulator();
                emergencySimulator.NurseUpdate += OnNurseUpdate;
                emergencySimulator.DoctorUpdate += OnDoctorUpdate;

                try
                {
                    await emergencySimulator.SimulateEmergencyActivity(patient);
                }
                finally
                {
                    emergencySimulator.NurseUpdate -= OnNurseUpdate;
                    emergencySimulator.DoctorUpdate -= OnDoctorUpdate;
                }

                return true;
            }
        }

        public string CreatePatientId()
        {
            int id = 1;
            List<int> ids = _patientStorage.Select(x => int.Parse(x.PatientId.Substring(2))).ToList();

            while (ids.Contains(id))
                id++;

            return "ER" + id.ToString();
        }

        public int GetRegisteredPatients() => _patientStorage.Count();

        public int GetDischargedPatients() => _patientStorage.Where(p => p.Status == StatusType.Discharged).Count();

        public int GetTreatedPatients() => _patientStorage.Where(p => p.Status == StatusType.Treated).Count();

        public bool IsPatientRegistered(string name, DateTime date)
        {
            if (_patientStorage.Count > 0)
                return _patientStorage.Any(p => p.Name == name && p.DateOfBirth == date);
            return false;
        }

        private void OnNurseUpdate(object sender, NurseUpdateEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _statusList.Add(new StatusMessage { Message = e.Message });
            });
        }

        private void OnDoctorUpdate(object sender, DoctorUpdateEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _statusList.Add(new StatusMessage { Message = e.Message });

                Patient updatedPatient = sender as Patient;

                if (updatedPatient != null)
                {
                    int patientIndex = _patientStorage.IndexOf(_patientStorage.Where(p=>p.PatientId == updatedPatient.PatientId).FirstOrDefault());
                    if (patientIndex != -1)
                    {
                        _patientStorage[patientIndex] = updatedPatient;
                    }

                }

                CheckDischargedStatus();
            });
        }

        private void CheckDischargedStatus()
        {
            if (_patientStorage.All(p => p.Status == StatusType.Discharged))
                _statusList.Add(new StatusMessage { Message = "STATUS: All patients are discharged" });
        }


    }
}
