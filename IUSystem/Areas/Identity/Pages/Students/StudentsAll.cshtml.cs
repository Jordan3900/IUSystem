using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IUSystem.Areas.Identity.Pages.Students
{
    [Authorize(Roles = "Admin")]
    public class StudentsAllModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsAllModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public List<Student> Students { get; set; }

        public IActionResult OnGet()
        {
            this.Students = this.dbContext.Students.ToList();

            return Page();
        }
    }
}