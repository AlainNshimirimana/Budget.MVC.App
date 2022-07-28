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

            var viewModel = new BudgetViewModel
            {
                Transactions = transactions
            };
            return View(viewModel);
        }
    }
}