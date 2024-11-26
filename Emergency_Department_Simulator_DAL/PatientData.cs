using Emergency_Department_Simulator_DTO;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace Emergency_Department_Simulator_DAL
{
    public class PatientData : IPatientData
    {
        private static readonly string projectRoot = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.Parent.FullName;
        private static readonly string patientStoragePath = Path.Combine(projectRoot, "Sample_Data", "patient_data.json");

        public ObservableCollection<Patient> LoadPatients()
        {
            try
            {
                if (File.Exists(patientStoragePath)) //Checks if file exists
                {
                    string jsonPatients = File.ReadAllText(patientStoragePath); //Reads the json file
                    return JsonConvert.DeserializeObject<ObservableCollection<Patient>>(jsonPatients); //returns the deserialized json file as patients
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return null;
            }

        }

        public bool SavePatients(ObservableCollection<Patient> patients)
        {
            try
            {
                // Ensure the directory exists
                Directory.CreateDirectory(Path.GetDirectoryName(patientStoragePath));

                // Serialize patients to JSON format
                string jsonPatients = JsonConvert.SerializeObject(patients, Formatting.Indented);

                // Write JSON data to the file
                File.WriteAllText(patientStoragePath, jsonPatients);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
