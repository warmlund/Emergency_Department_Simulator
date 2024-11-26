using Emergency_Department_Simulator_DTO;

namespace Emergency_Department_Simulator_BLL
{
    internal interface IPatientManager
    {
        void AddPatient();
        void UpdatePatient();
        void LoadPatients();
        void SavePatients();
        void CreatePatientId();
        int GetRegisteredPatients();
        int GetDischargedPatients();
        int GetTreatedPatients();
        List<Patient> GetPatients();
    }
}
