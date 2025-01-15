using System;
using System.Collections.Generic;
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
        decimal GetPendingDebtTotal();
        decimal GetClearedDebtTotal();
        List<Transaction> GetPendingDebts();
        Transaction GetHighestTransaction();
        Transaction GetLowestTransaction();
        bool TryClearDebt(Transaction debt, decimal availableBalance, out string errorMessage);
    }
}
