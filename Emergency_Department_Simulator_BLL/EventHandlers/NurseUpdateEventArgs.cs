using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL.EventHandlers
{
    /// <summary>
    /// Eventargs for nurse updates
    /// </summary>
    public class NurseUpdateEventArgs : PatientEventArgs
    {
        public string Message { get; }

        public NurseUpdateEventArgs(Patient patient) : base(patient)
        {
            Message = $"STATUS: {patient.PatientId} {patient.Name} Nurse updated status for patient";
        }
    }
}
