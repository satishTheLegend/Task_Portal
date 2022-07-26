namespace Task_Portal.Models
{
    public class TransactionRecordViewModel
    {
        public int Id { get; set; }
        public string? CustCode { get; set; }
        public string? CustName { get; set; }
        public long? Balance { get; set; }

        public List<TransactionRecord>? TransactionRecords { get; set; }

        public bool? IsTotal { get; set; }
        public string? TotalAmount { get; set; } = "NA";
    }
}
