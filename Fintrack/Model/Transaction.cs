namespace Fintrack.Model
{
    public class Transaction
    {
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Type { get; set; }
        public bool IsDebtCleared { get; set; } = false;
        public DateTime? DebtClearedDate { get; set; }
        public string Note { get; set; }  
        public string Tag { get; set; }
    }
}
