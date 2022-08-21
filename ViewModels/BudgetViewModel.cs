using Budget.MVC.App.Models;
using System;
namespace Budget.MVC.App.ViewModels
{
    public class BudgetViewModel
    {
        public List<Transaction>? Transactions { get; set; }
        public InsertTransactionViewModel InsertTransaction { get; set; }
    }
}
