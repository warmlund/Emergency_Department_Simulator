using Emergency_Department_Simulator_BLL.EventHandlers;
using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL
{
    public class EmergencySimulator
    {
        private Random random;
        private readonly object _locker = new object();
        //setting event handlers for nurse and doctor updates
        public event EventHandler<NurseUpdateEventArgs> NurseUpdate;
        public event EventHandler<DoctorUpdateEventArgs> DoctorUpdate;

        public EmergencySimulator()
        {
            random = new Random();
        }

        public async Task SimulateEmergencyActivity(Patient patient)
        {
            while (patient.Status != StatusType.Discharged) //Simulates while the patient is not discharged
            {
                if (random.Next(0, 3) == 0 && patient.Status != StatusType.Treated) //1/3 chanse for simulating nurse updates
                {
                    await Task.Run(() => OnSimulateNurseUpdate(patient));
                }

                else
                {
                    await Task.Run(() => OnSimulateDoctorUpdate(patient));
                }
            }
        }

        /// <summary>
        /// Simulates the nurse updates only happens if the patient is not treated
        /// </summary>
        /// <param name="patient"></param>
        private void OnSimulateNurseUpdate(Patient patient)
        {
            Thread.Sleep(random.Next(1000, 3000));

            if (patient.Status != StatusType.Treated)
            {
                lock (_locker)
                {
                    NurseUpdate?.Invoke(patient, new NurseUpdateEventArgs(patient)); //Thread safe invoke of the event
                }
            }
        }

            /// <summary>
            /// Simulates different doctor updates depending on the patients status
            /// </summary>
            /// <param name="patient">the current patient</param>
            private void OnSimulateDoctorUpdate(Patient patient)
            {
                Thread.Sleep(random.Next(1000, 3000));

                if (patient.Status == StatusType.Registered)
                {
                    patient.Status = StatusType.Diagnosed;
                    lock (_locker)
                    {
                        DoctorUpdate?.Invoke(patient, new DoctorUpdateEventArgs(patient)); //Thread safe invoke of the event
                    }
                }
                else if (patient.Status == StatusType.Diagnosed)
                {
                    patient.Status = StatusType.Treated;
                    lock (_locker)
                    {
                        DoctorUpdate?.Invoke(patient, new DoctorUpdateEventArgs(patient));
                    }
                }
                else if (patient.Status == StatusType.Treated)
                {
                    patient.Status = StatusType.Discharged;
                    lock (_locker)
                    {
                        DoctorUpdate?.Invoke(patient, new DoctorUpdateEventArgs(patient));
                    }
                }
            }
        }
    }
