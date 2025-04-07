namespace FormWise.WebApi.Domain.Entities
{
    public class Reimbursement : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        public string ReceiptFilePath { get; set; }
    }

}
