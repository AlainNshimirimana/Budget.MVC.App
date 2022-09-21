using Budget.MVC.App.Models;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Data;
using System.Xml.Linq;

namespace Budget.MVC.App.Repositories
{
    public interface IBudgetRepository
    {
        List<Transaction> GetTransactions();
        void AddTransaction(Transaction transaction);
        void UpdateTransaction(Transaction transaction);
        void DeleteTransaction(int id);
        List<Category> GetCategories();
        void AddCategory(string category);
        void UpdateCategory(string category, int id);
        void DeleteCategory(int id);
    }
    public class BudgetRepository:IBudgetRepository
    {
        private readonly IConfiguration _configuration;
        public BudgetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void AddCategory(string category)
        {
            using (IDbConnection connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                var query = @"INSERT INTO Category(Name) 
                            VALUES(@Name)";
                connection.Execute(query, new {Name = category});
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            using (IDbConnection connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                var query = @"INSERT INTO Transactions(Name, Date, Amount, TransactionType, CategoryId) 
                            VALUES(@Name, @Date, @Amount, @TransactionType, @CategoryId)";
                connection.Execute(query, transaction);
            }
        }

        public void DeleteCategory(int id)
        {
            using (IDbConnection connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                var query =
                    @"DELETE FROM Category WHERE Id = @id";

                connection.Execute(query, new { id });
            }
        }

        public void DeleteTransaction(int id)
        {
            using (IDbConnection connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                var query =
                    @"DELETE FROM Transactions WHERE Id = @id";

                connection.Execute(query, new { id });
            }
        }

        public List<Category> GetCategories()
        {
            using (IDbConnection connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                var query =
                    @"SELECT * FROM Category";

                var categories = connection.Query<Category>(query);

                return categories.ToList();
            }
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

        public void UpdateCategory(string category, int id)
        {
            using (IDbConnection connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                var sql =
                    "UPDATE Category SET Name = @Name WHERE Id = @Id";

                connection.Execute(sql, new { Name = category, Id = id });
            }
        }

        public void UpdateTransaction(Transaction transaction)
        {
            using (IDbConnection connection = new SqliteConnection(_configuration.GetConnectionString("ConnectionString")))
            {
                var query = @"UPDATE Transactions
                              SET Name = @Name, Date = @Date, Amount = @Amount, TransactionType = @TransactionType, CategoryId = @CategoryId
                              WHERE Id = @Id";
                connection.Execute(query, transaction);
            }
        }
    }       
}
