using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Areas.Identity.Pages.Account.Manage;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IUSystem.Areas.Identity.Pages.Students
{
    [Authorize(Roles = "Admin")]
    public class EditStudentModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public EditStudentModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [BindProperty]
        public Student Student { get; set; }

        [BindProperty]
        public List<Subject> Subjects { get; set; }

        public IActionResult OnGet(string id)
        {
            this.Student = this.dbContext.Students.FirstOrDefault(x => x.Id == id);
            this.Subjects = this.Student.Subjects.ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(List<Subject> subjects)
        {
            foreach (var subject in subjects)
            {
                var sub = await this.dbContext.Subjects.FindAsync(subject.Id);
                sub.Grade = subject.Grade;
                sub.Name = subject.Name;

                await this.dbContext.SaveChangesAsync();
            }

            return RedirectToPage("./StudentsAll");
        }
    }
}