using Emergency_Department_Simulator_DTO;
using System.Collections.ObjectModel;

namespace Emergency_Department_Simulator_DAL
{
    public interface IPatientData
    {
        ObservableCollection<Patient> LoadPatients();
        bool SavePatients(ObservableCollection<Patient> patients);
    }
}
