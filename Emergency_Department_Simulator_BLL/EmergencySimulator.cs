using Emergency_Department_Simulator_BLL.EventHandlers;
using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL
{
    public class EmergencySimulator
    {
        private Random random;

        public event EventHandler<NurseUpdateEventArgs> NurseUpdate;
        public event EventHandler<DoctorUpdateEventArgs> DoctorUpdate;

        public EmergencySimulator()
        {
            random = new Random();
        }

        public async Task SimulateEmergencyActivity(Patient patient)
        {
            while (patient.Status != StatusType.Discharged)
            {   
                if (random.Next(0, 2) == 0)
                {
                    await Task.Run(() => OnSimulateDoctorUpdate(patient));
                }

                else
                {
                   await Task.Run(() =>OnSimulateNurseUpdate(patient));
                }
            }
        }

        private void OnSimulateNurseUpdate(Patient patient)
        {
            Thread.Sleep(random.Next(1000,3000));

            if(patient.Status != StatusType.Treated || patient.Status!=StatusType.Discharged)
                NurseUpdate?.Invoke(patient, new NurseUpdateEventArgs(patient));
        }

        private void OnSimulateDoctorUpdate(Patient patient)
        {
            Thread.Sleep(random.Next(1000, 3000));

            if(patient.Status == StatusType.Registered)
            {
                patient.Status = StatusType.Diagnosed;
                DoctorUpdate?.Invoke(patient, new DoctorUpdateEventArgs(patient));
            }

            else if (patient.Status == StatusType.Diagnosed)
            {
                patient.Status = StatusType.Treated;
                DoctorUpdate?.Invoke(patient, new DoctorUpdateEventArgs(patient));
            }

            else if (patient.Status == StatusType.Treated)
            {
                patient.Status = StatusType.Discharged;
                DoctorUpdate?.Invoke(patient, new DoctorUpdateEventArgs(patient));
            }
                
        }
    }
}
