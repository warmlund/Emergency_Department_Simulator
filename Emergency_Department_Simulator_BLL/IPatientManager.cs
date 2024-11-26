using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL
{
    internal interface IPatientManager
    {
        Task <bool> AddPatient(string name, DateOnly date);
        void UpdatePatientStatus();
        void LoadPatients();
        void SavePatients();
        int GetRegisteredPatients();
        int GetDischargedPatients();
        int GetTreatedPatients();
        void UpdateStatusBoard();
    }
}
