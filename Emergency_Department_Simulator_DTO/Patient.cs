namespace Emergency_Department_Simulator_DTO
{
    public class Patient
    {
        public string PatientId { get; set; }
        public string Name { get; set; }
        public DateOnly DateOfBirth { get; set; }

        public StatusType Status { get; set; }
    }
}
