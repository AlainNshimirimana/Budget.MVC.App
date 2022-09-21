using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Budget.MVC.App.ViewModels
{
    public class InsertCategoryViewModel
    {
        public int Id { get; set; }
        [Required]
        [Remote("IsUnique", "Home")]
        public string Name { get; set; }
    }
}
