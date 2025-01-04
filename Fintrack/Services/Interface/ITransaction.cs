using Fintrack.Model;

namespace Fintrack.Services.Interface
{
    public interface ITransaction
    {
        List<Transaction> GetAllTransactions();
        List<Transaction> GetFilteredTransactions(DateTime? fromDate, DateTime? toDate, string type);
        bool AddTransaction(Transaction transaction, decimal availableBalance);
        bool UpdateTransaction(Transaction oldTransaction, Transaction newTransaction);
        bool DeleteTransaction(Transaction transaction);
        (decimal Income, decimal Expense, decimal Debt, decimal Balance) GetFinancialSummary();

        Transaction GetHighestTransaction();
        Transaction GetLowestTransaction();

    }
}