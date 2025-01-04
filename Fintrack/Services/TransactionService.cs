using Fintrack.Abstraction;
using Fintrack.Model;
using Fintrack.Services.Interface;

namespace Fintrack.Services
{
    public class TransactionService : TransactionBase, ITransaction
    {
        private List<Transaction> _transactions;

        public TransactionService()
        {
            _transactions = LoadTransactions();
        }

        public List<Transaction> GetAllTransactions()
        {
            return _transactions.OrderByDescending(t => t.Date).ToList();
        }

        public List<Transaction> GetFilteredTransactions(DateTime? fromDate, DateTime? toDate, string type)
        {
            var filteredTransactions = _transactions.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(type))
                filteredTransactions = filteredTransactions.Where(t => t.Type == type);

            if (fromDate.HasValue)
                filteredTransactions = filteredTransactions.Where(t => t.Date >= fromDate.Value);

            if (toDate.HasValue)
                filteredTransactions = filteredTransactions.Where(t => t.Date <= toDate.Value);

            return filteredTransactions.OrderByDescending(t => t.Date).ToList();
        }

        public bool AddTransaction(Transaction transaction, decimal availableBalance)
        {
            if (transaction.Type == "Expense" && transaction.Amount > availableBalance)
                return false;

            _transactions.Add(transaction);
            SaveTransactions(_transactions);
            return true;
        }

        public bool UpdateTransaction(Transaction oldTransaction, Transaction newTransaction)
        {
            var existingTransaction = _transactions.FirstOrDefault(t =>
                t.Description == oldTransaction.Description &&
                t.Date == oldTransaction.Date &&
                t.Amount == oldTransaction.Amount &&
                t.Type == oldTransaction.Type);

            if (existingTransaction == null)
                return false;

            _transactions.Remove(existingTransaction);
            _transactions.Add(newTransaction);
            SaveTransactions(_transactions);
            return true;
        }

        public bool DeleteTransaction(Transaction transaction)
        {
            var result = _transactions.Remove(transaction);
            if (result)
                SaveTransactions(_transactions);
            return result;
        }

        public (decimal Income, decimal Expense, decimal Debt, decimal Balance) GetFinancialSummary()
        {
            var income = _transactions.Where(t => t.Type == "Income").Sum(t => t.Amount);
            var expense = _transactions.Where(t => t.Type == "Expense").Sum(t => t.Amount);
            var debt = _transactions.Where(t => t.Type == "Debt").Sum(t => t.Amount);
            var balance = income + debt - expense;

            return (income, expense, debt, balance);
        }
    }
}