﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Budget.MVC.App.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
