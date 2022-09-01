using System.Text.Json.Serialization;

namespace ContractTZ1.Models
{
    public class ContractStage
    {

        public int id { get; set; }
        public string nameStage { get; set; }
        public DateTime startDate { get; set; } 
        public DateTime stopDate { get; set; }
        public int contractId { get; set; }
        [JsonIgnore]
        public Contract contract { get; set; }


    }
}
