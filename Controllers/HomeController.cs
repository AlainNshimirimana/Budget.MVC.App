using Budget.MVC.App.Models;
using Microsoft.AspNetCore.Mvc;
using Budget.MVC.App.Repositories;
using Budget.MVC.App.ViewModels;
using System.Diagnostics;

namespace Budget.MVC.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBudgetRepository _budgetRepository;

        public HomeController(IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
        }
        public IActionResult Index()
        {
            var transactions = _budgetRepository.GetTransactions();
            var categories = _budgetRepository.GetCategories();

            var viewModel = new BudgetViewModel
            {
                Transactions = transactions,
                InsertTransaction = new InsertTransactionViewModel { Categories = categories },
                Categories = new CategoryViewModel {  Categories = categories}
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult InsertTransaction(BudgetViewModel bvm)
        {
            var transaction = new Transaction
            {
                Id = bvm.InsertTransaction.Id,
                Name = bvm.InsertTransaction.Name,
                Amount =  bvm.InsertTransaction.Amount,
                Date = bvm.InsertTransaction.Date,
                TransactionType = bvm.InsertTransaction.TransactionType,
                CategoryId = bvm.InsertTransaction.CategoryId,
            };
            if (transaction.Id > 0)
            {
                _budgetRepository.UpdateTransaction(transaction);
            }
            else
            {
                _budgetRepository.AddTransaction(transaction);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteTransaction(int id)
        {
            _budgetRepository.DeleteTransaction(id);

            return RedirectToAction("Index");
        }


    }
}