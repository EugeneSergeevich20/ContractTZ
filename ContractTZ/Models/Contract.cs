namespace ContractTZ1.Models
{
    public class Contract
    {

        public int id { get; set; }
        public string contractCode { get; set; }
        public string contractName { get; set; }
        public string customer { get; set; }
        public List<ContractStage> contractStages { get; set; }

        public Contract()
        {
            contractStages = new List<ContractStage>();
        }

    }
}
