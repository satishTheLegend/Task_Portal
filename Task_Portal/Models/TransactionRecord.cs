using System.ComponentModel.DataAnnotations;

namespace Task_Portal.Models
{
    public class TransactionRecord
    {
        [Key]
        public int TransactionId { get; set; }
        public DateTime? Date { get; set; }
        public string? CustCode { get; set; }
        public long? DebitAmt { get; set; }
        public long? CreditAmt { get; set; }
    }
}
