using Newtonsoft.Json;

namespace Emergency_Department_Simulator_DTO
{
    public class Patient
    {
        [JsonProperty("Id")]
        public string PatientId { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("DateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        [JsonProperty("Status")]
        public StatusType Status { get; set; }
    }
}
