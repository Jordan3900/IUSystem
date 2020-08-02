using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IUSystem.Areas.Identity.Pages.Subjects
{
    public class SubjectsAddModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public SubjectsAddModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.Students = this.dbContext.Students.OrderBy(x => x.FirstName).ToList();
        }

        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        [Range(2, 6, ErrorMessage = "Age must be between 25 and 60")]
        public double Grade { get; set; }

        [BindProperty]
        [Display(Name = "Student")]
        public string StudentId { get; set; }


        public ICollection<Student> Students { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult >OnPostAsync()
        {
            var student = await this.dbContext.Students.FindAsync(this.StudentId);

            var subject = new Subject
            {
                Id = Guid.NewGuid().ToString(),
                Name = this.Name,
                Grade = this.Grade,
                Student = student
            };

            this.dbContext.Subjects.Add(subject);
            await this.dbContext.SaveChangesAsync();

            return RedirectToPage("./All");
        }

        public async Task<IActionResult> OnGetDeleteAsync(string id)
        {
            var lecture = await this.dbContext.Lectures.FindAsync(id);

            if (lecture != null)
            {
                this.dbContext.Lectures.Remove(lecture);
                await this.dbContext.SaveChangesAsync();
            }

            return Page();
        }
    }
}
