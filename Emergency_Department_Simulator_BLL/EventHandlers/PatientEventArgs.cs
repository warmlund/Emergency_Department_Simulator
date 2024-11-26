using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL.EventHandlers
{
    public class PatientEventArgs : EventArgs
    {
        public Patient Patient { get; }

        public PatientEventArgs(Patient patient)
        {
            Patient = patient;
        }
    }
}
