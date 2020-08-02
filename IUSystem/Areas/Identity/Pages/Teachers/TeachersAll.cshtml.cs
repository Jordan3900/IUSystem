using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IUSystem.Areas.Identity.Pages.Teachers
{
    public class TeachersAllModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public TeachersAllModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IList<Teacher> Teachers { get; set; }

        public IActionResult OnGet()
        {
            this.Teachers = this.dbContext.Teachers.ToList();

            return Page();
        }
    }
}