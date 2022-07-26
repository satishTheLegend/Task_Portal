using System.ComponentModel.DataAnnotations;

namespace Task_Portal.Models
{
    public class CustomerRecord
    {
        [Key]
        public int Id { get; set; }
        public string? CustCode { get; set; }
        public string? CustName { get; set; }
        public long? Balance { get; set; }
    }
}
