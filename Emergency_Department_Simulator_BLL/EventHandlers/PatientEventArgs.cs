using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL.EventHandlers
{
    /// <summary>
    /// Abstract eventargs class that the other eventargs inherits from
    /// </summary>
    public abstract class PatientEventArgs : EventArgs
    {
        public Patient Patient { get; }

        public PatientEventArgs(Patient patient)
        {
            Patient = patient;
        }
    }
}
