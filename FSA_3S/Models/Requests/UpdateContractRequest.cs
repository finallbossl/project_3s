namespace FSA_3S.Models.Requests
{
    public class UpdateContractRequest
    {
        public int ContractType { get; set; }
        public int ContractStatus { get; set; }
        public int StatusPayment { get; set; }
        public List<int> ClauseIds { get; set; } = new();
    }
}
