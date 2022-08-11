using Budget.MVC.App.Models;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Data;

namespace Budget.MVC.App.Repositories
{
    public interface IBudgetRepository
    {
        List<Transaction> GetTransactions();
    }
    public class BudgetRepository:IBudgetRepository
    {
        private readonly IConfiguration _configuration;
        public BudgetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // Connect to the database and get all transactions
        public List<Transaction> GetTransactions()
        {
            using (IDbConnection connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                var query =
                    @"SELECT t.Amount, t.CategoryId, t.[Date], t.Id, t.TransactionType, t.Name, c.Name AS Category
                      FROM Transactions AS t
                      LEFT JOIN Category AS c
                      ON t.CategoryId = c.Id;";

                var allTransactions = connection.Query<Transaction>(query);

                return allTransactions.ToList();
            }
        }
    }       
}
