using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IUSystem.Data;
using IUSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IUSystem.Areas.Identity.Pages.Lectures
{
    [Authorize(Roles = "Admin")]
    public class LecturesAllModel : PageModel
    {
        private readonly ApplicationDbContext dbContext;

        public LecturesAllModel(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.Lectures = this.dbContext.Lectures.ToList();
        }

        public IList<Lecture> Lectures { get; set; }

        public IActionResult OnGet()
        {
            return Page();
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