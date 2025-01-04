using System.IO;
using System.Collections.Generic;
using Fintrack.Model;

namespace Fintrack.Abstraction
{
    public abstract class TransactionBase
    {
        protected static readonly string FilePath = Path.Combine(FileSystem.AppDataDirectory, "transactions.csv");

        protected List<Transaction> LoadTransactions()
        {
            if (!File.Exists(FilePath))
                return new List<Transaction>();

            var transactions = new List<Transaction>();
            var lines = File.ReadAllLines(FilePath);

            foreach (var line in lines)
            {
                var parts = line.Split(',');
                if (parts.Length == 4)
                {
                    transactions.Add(new Transaction
                    {
                        Description = parts[0],
                        Date = DateTime.Parse(parts[1]),
                        Amount = decimal.Parse(parts[2]),
                        Type = parts[3]
                    });
                }
            }

            return transactions;
        }

        protected void SaveTransactions(List<Transaction> transactions)
        {
            var lines = transactions.Select(t => $"{t.Description},{t.Date:yyyy-MM-dd},{t.Amount},{t.Type}");
            File.WriteAllLines(FilePath, lines);
        }
    }
}
