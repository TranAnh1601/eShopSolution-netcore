﻿using eShopSolution.Data.Entities;
using eShopSolution.ViewModels.System.Languages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eShopSolution.AdminApp.Models
{
    public class NavigationViewModel
    {

        // public List<SelectListItem> Languages { get; set; }
        public List<LanguageVm> Languages { get; set; }

        public string CurrentLanguageId { get; set; }

        public string ReturnUrl { set; get; }
    }
}
