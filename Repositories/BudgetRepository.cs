﻿using Budget.MVC.App.Models;
using Microsoft.Data.Sqlite;
using Dapper;
using System.Data;

namespace Budget.MVC.App.Repositories
{
    public interface IBudgetRepository
    {
        List<Transaction> GetTransactions();
        List<Category> GetCategories();
        void AddTransaction(Transaction transaction);
    }
    public class BudgetRepository:IBudgetRepository
    {
        private readonly IConfiguration _configuration;
        public BudgetRepository(IConfiguration configuration)
        {
            _configuration = configuration;
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
    }       
}
