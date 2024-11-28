using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL.EventHandlers
{
    /// <summary>
    /// Eventargs for doctor updates
    /// Message update depends on the patients status type
    /// </summary>
    public class DoctorUpdateEventArgs : PatientEventArgs
    {
        public string Message { get; }

        public DoctorUpdateEventArgs(Patient patient) : base(patient)
        {
            switch (patient.Status)
            {
                case StatusType.Diagnosed:
                    Message = $"DIAGNOSED: {patient.PatientId} {patient.Name} is diagnosed by doctor";
                    break;

                case StatusType.Treated:
                    Message = $"TREATED: {patient.PatientId} {patient.Name} is treated by doctor";
                    break;

                case StatusType.Discharged:
                    Message = $"DISCHARGED: {patient.PatientId} {patient.Name} is discharged by doctor";
                    break;
            }
        }
    }
}
