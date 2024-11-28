namespace Emergency_Department_Simulator_BLL
{
    /// <summary>
    /// Interface for patientmanger
    /// </summary>
    internal interface IPatientManager
    {
        Task<bool> AddPatient(string name, DateTime date);
        int GetRegisteredPatients();
        int GetDischargedPatients();
        int GetTreatedPatients();
    }
}
