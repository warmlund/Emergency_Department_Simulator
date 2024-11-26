using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL.EventHandlers
{
    public class DoctorUpdateEventArgs : PatientEventArgs
    {
        public string Message { get; }

        public DoctorUpdateEventArgs(Patient patient) : base(patient)
        {
            switch (patient.Status)
            {
                case StatusType.Diagnosed:
                    Message = $"STATUS: {patient.PatientId} {patient.Name} is diagnosed by doctor";
                    break;

                case StatusType.Treated:
                    Message = $"STATUS: {patient.PatientId} {patient.Name} is treated by doctor";
                    break;
            }
        }
    }
}
