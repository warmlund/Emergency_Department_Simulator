namespace Emergency_Department_Simulator_BLL
{
    internal interface IPatientManager
    {
        Task<bool> AddPatient(string name, DateTime date);
        bool LoadPatients();
        bool SavePatients();
        int GetRegisteredPatients();
        int GetDischargedPatients();
        int GetTreatedPatients();
    }
}
