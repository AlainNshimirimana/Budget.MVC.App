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
        public IActionResult Index(BudgetViewModel? model)
        {
            var transactions = FilterTransactions(model);
            var categories = _budgetRepository.GetCategories();

            var viewModel = new BudgetViewModel
            {
                Transactions = transactions,
                InsertTransaction = new InsertTransactionViewModel { Categories = categories },
                Categories = new CategoryViewModel {  Categories = categories},
                FilterParameters = new FilterParametersViewModel { Categories = categories }
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
            // update transaction
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

        [HttpPost]
        public IActionResult InsertCategory(BudgetViewModel bvm)
        {
            // add and update transaction
            if (bvm.InsertCategory.Id > 0)
            {
                _budgetRepository.UpdateCategory(bvm.InsertCategory.Name, bvm.InsertCategory.Id);
            }
            else
            {
                _budgetRepository.AddCategory(bvm.InsertCategory.Name);
            }
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            _budgetRepository.DeleteCategory(id);

            return RedirectToAction("Index");
        }

        //filter transactions
        private List<Transaction> FilterTransactions(BudgetViewModel? model)
        {
            var transactions = _budgetRepository.GetTransactions();

            if (model.FilterParameters == null)
                transactions = transactions.ToList();

            else if ((model.FilterParameters.CategoryId != 0 && model.FilterParameters.StartDate == null))
                transactions = transactions
                    .Where(x => x.CategoryId == model.FilterParameters.CategoryId)
                    .ToList();

            else if ((model.FilterParameters.CategoryId == 0 && model.FilterParameters.StartDate != null))
                transactions = transactions
                    .Where(x =>
                    DateTime.Parse(x.Date) >= DateTime.Parse(model.FilterParameters.StartDate) &&
                    DateTime.Parse(x.Date) <= DateTime.Parse(model.FilterParameters.EndDate))
                    .ToList();

            else if ((model.FilterParameters.CategoryId != 0 && model.FilterParameters.StartDate != null))
                transactions = transactions
                         .Where(x =>
                         DateTime.Parse(x.Date) >= DateTime.Parse(model.FilterParameters.StartDate) &&
                         DateTime.Parse(x.Date) <= DateTime.Parse(model.FilterParameters.EndDate) &&
                         x.CategoryId == model.FilterParameters.CategoryId)
                         .ToList();

            return transactions;
        }

        [AcceptVerbs("GET", "POST")]
        public JsonResult IsUnique([Bind(Prefix = "InsertCategory.Name")] string name)
        {
            var categories = _budgetRepository.GetCategories();

            if (categories.Any(x => x.Name == name))
                return Json("Category already exists");

            return Json(true);

        }


    }
}