using Budget.MVC.App.Models;
using System;
namespace Budget.MVC.App.ViewModels
{
    public class BudgetViewModel
    {
        public List<Transaction>? Transactions { get; set; }
        public InsertTransactionViewModel InsertTransaction { get; set; }
        public CategoryViewModel Categories { get; set; }
        public InsertCategoryViewModel InsertCategory { get; set; }
        public FilterParametersViewModel? FilterParameters { get; set; }
    }
}
