using Emergency_Department_Simulator_BLL.EventHandlers;
using Emergency_Department_Simulator_DTO;
using System.Collections.ObjectModel;
using System.Windows;

namespace Emergency_Department_Simulator_BLL
{
    public class PatientManager : IPatientManager
    {
        private ObservableCollection<Patient> _patientStorage; //Collection of patients
        private ObservableCollection<StatusMessage> _statusList; //Collection of status messages

        public ObservableCollection<Patient> PatientStorage { get { return _patientStorage; } } 
        public ObservableCollection<StatusMessage> StatusList { get { return _statusList; } }

        public PatientManager()
        {
            _patientStorage = new ObservableCollection<Patient>();
            _statusList = new ObservableCollection<StatusMessage>();
        }

        /// <summary>
        /// async task for adding patient
        /// </summary>
        /// <param name="name">patient name</param>
        /// <param name="date">patient date of birth</param>
        /// <returns></returns>
        public async Task<bool> AddPatient(string name, DateTime date)
        {
            if (IsPatientRegistered(name, date)) //checks if the patient is registered
                return false;

            else
            {
                string id = CreatePatientId(); //creates id 
                Patient patient = new Patient { Name = name, DateOfBirth = date, PatientId = id, Status = StatusType.Registered }; //create patient
                _patientStorage.Add(patient); //add patient to storage
                _statusList.Add(new StatusMessage { Message = $"REGISTERED: {patient.PatientId} {patient.Name}" }); //adds status update for registered patient

                EmergencySimulator emergencySimulator = new EmergencySimulator(); //creates a new simulator
                emergencySimulator.NurseUpdate += OnNurseUpdate; //subscribes to nurse event in the simulator
                emergencySimulator.DoctorUpdate += OnDoctorUpdate; //subscribes to doctor events in the simulator

                try
                {
                    await emergencySimulator.SimulateEmergencyActivity(patient); //simulates activities
                }
                finally
                {
                    //when the simulation is over for the patient unsubscribing to the events
                    emergencySimulator.NurseUpdate -= OnNurseUpdate;
                    emergencySimulator.DoctorUpdate -= OnDoctorUpdate;
                }

                return true;
            }
        }

        /// <summary>
        /// Method for creating unique id for a new patient
        /// </summary>
        /// <returns></returns>
        public string CreatePatientId()
        {
            int id = 1; //counter
            List<int> ids = _patientStorage.Select(x => int.Parse(x.PatientId.Substring(2))).ToList(); //gets all ids from storage withouth the prefix ER

            while (ids.Contains(id)) //while loop that increases the id with 1 if it is already in use
                id++;

            return "ER" + id.ToString(); //Returns the unique id with the ER prefix
        }

        /// <summary>
        /// Returns all registered patients
        /// </summary>
        /// <returns>all registered patients</returns>
        public int GetRegisteredPatients() => _patientStorage.Count();

        /// <summary>
        /// Gets all patients that have status discharged
        /// </summary>
        /// <returns>number of discharged patients</returns>
        public int GetDischargedPatients() => _patientStorage.Where(p => p.Status == StatusType.Discharged).Count();


        /// <summary>
        /// Gets all treated patients
        /// </summary>
        /// <returns>number of treated patients</returns>
        public int GetTreatedPatients() => _patientStorage.Where(p => p.Status == StatusType.Treated).Count();

        /// <summary>
        /// Checks if a patient with the same name and date of birth
        /// is already registered
        /// </summary>
        /// <param name="name">patient name</param>
        /// <param name="date">patient date of birth</param>
        /// <returns></returns>
        public bool IsPatientRegistered(string name, DateTime date)
        {
            if (_patientStorage.Count > 0) //checks if the storage is not empty
                return _patientStorage.Any(p => p.Name == name && p.DateOfBirth == date); //checks if any item in the storage has the same name and DoB
            return false;
        }

        /// <summary>
        /// Method that subscribes to the event in the simulator class
        /// </summary>
        /// <param name="sender">the patient</param>
        /// <param name="e">the event args with status message</param>
        private void OnNurseUpdate(object sender, NurseUpdateEventArgs e)
        {
            //Due to the viewmodels is bound to the observablecollection of the statuslist
            //A dispatcher is used for thread safe updates 
            Application.Current.Dispatcher.Invoke(() =>
            {
                _statusList.Add(new StatusMessage { Message = e.Message });
            });
        }

        /// <summary>
        /// Mehod that subscribes to the doctor event of the simulator class
        /// </summary>
        /// <param name="sender">the patient</param>
        /// <param name="e">the event args with status message</param>
        private void OnDoctorUpdate(object sender, DoctorUpdateEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                _statusList.Add(new StatusMessage { Message = e.Message });

                Patient updatedPatient = sender as Patient; //this patient is a copy of the patient in the storage, but the patient in the storage hasn't the updated status type

                //replacing the patient in the storage with the patient from the event args with the updated stauts
                if (updatedPatient != null)
                {
                    int patientIndex = _patientStorage.IndexOf(_patientStorage.Where(p=>p.PatientId == updatedPatient.PatientId).FirstOrDefault());
                    if (patientIndex != -1)
                    {
                        _patientStorage[patientIndex] = updatedPatient;
                    }

                }

                CheckDischargedStatus(); //Check if all patients have been discharged
            });
        }

        /// <summary>
        /// Checks if all the patients in the storage have been discharged
        /// If true, a status message is created
        /// </summary>
        private void CheckDischargedStatus()
        {
            if (_patientStorage.All(p => p.Status == StatusType.Discharged))
                _statusList.Add(new StatusMessage { Message = "STATUS: All patients are discharged" });
        }


    }
}
